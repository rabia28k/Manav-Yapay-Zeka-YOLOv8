using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Runtime.Versioning;
using System.Speech.Synthesis; // SES İÇİN EKLENDİ

namespace ManavAsistaniApp
{
    [SupportedOSPlatform("windows")]
    class Program
    {
        // Etiket sırası 
        static readonly string[] Labels = { "patlican", "muz", "patates", "havuc", "salatalik", "Curuk_Muz" };

        // SES İÇİN DEĞİŞKENLER
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        static string lastSpeech = "";

        static void Main(string[] args)
        {
            string modelPath = "best.onnx";
            using var session = new InferenceSession(modelPath);

            // --- TÜRKÇE SES YAPILANDIRMASI ---
            synth.Rate = 1;
            var turkishVoice = synth.GetInstalledVoices()
                .FirstOrDefault(v => v.VoiceInfo.Culture.Name.Contains("TR") || v.VoiceInfo.Name.Contains("Turkish"));

            if (turkishVoice != null)
            {
                synth.SelectVoice(turkishVoice.VoiceInfo.Name);
                Console.WriteLine($"✅ Ses Türkçe ayarlandi: {turkishVoice.VoiceInfo.Name}");
            }
            // ---------------------------------

            // Telefon IP adresin
            string videoUrl = "http://10.245.4.143:8080/video";
            using var capture = new VideoCapture(videoUrl);

            // Performans için buffer'ı 1 yaptım
            capture.Set(VideoCaptureProperties.BufferSize, 1);

            using var window = new Window("Manav Asistani - Hizli ve Akici");
            using var frame = new Mat();

            Console.WriteLine("🚀 Sistem hizlandirildi! ESC ile cikabilirsiniz.");
            while (true)
            {
                // PERFORMANS: Eski kareleri hizlica atla 
                for (int i = 0; i < 5; i++) capture.Grab();

                if (!capture.Retrieve(frame) || frame.Empty()) continue;

                // BOYUT: Ekrani rahatlatmak icin goruntuyu %50 kucult
                Cv2.Resize(frame, frame, new Size(frame.Width / 2, frame.Height / 2));

                // Analiz icin 640x640 yap
                using var resized = new Mat();
                Cv2.Resize(frame, resized, new Size(640, 640));
                var input = PrepareInput(resized);

                // Modeli calistir
                var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor("images", input) };
                using var results = session.Run(inputs);


                var output = results.First().AsEnumerable<float>().ToArray();

                // Cizim ve sayim (currentFrameCounts her karede sifirlanir)
                ParseAndDraw(frame, output);

                window.ShowImage(frame);
                if (Cv2.WaitKey(1) == 27) break;
            }
        }

        static void Announce(Dictionary<string, int> counts)
        {
            if (counts.Count == 0) return;

            string text = string.Join(", ", counts.Select(x => $"{x.Value} tane {x.Key}"));

            if (text != lastSpeech && synth.State != SynthesizerState.Speaking)
            {
                lastSpeech = text;
                synth.SpeakAsync(text); // SpeakAsync: Görüntü akışını dondurmaz
            }
        }

        static DenseTensor<float> PrepareInput(Mat img)
        {
            var tensor = new DenseTensor<float>(new[] { 1, 3, 640, 640 });
            for (int y = 0; y < 640; y++)
            {
                for (int x = 0; x < 640; x++)
                {
                    var color = img.At<Vec3b>(y, x);
                    tensor[0, 0, y, x] = color.Item2 / 255f; // R
                    tensor[0, 1, y, x] = color.Item1 / 255f; // G
                    tensor[0, 2, y, x] = color.Item0 / 255f; // B
                }
            }
            return tensor;
        }

        static void ParseAndDraw(Mat frame, float[] output)
        {
            int rows = 8400;
            var currentFrameCounts = new Dictionary<string, int>();
            var detections = new List<Detection>();

            for (int i = 0; i < rows; i++)
            {
                float maxScore = 0;
                int classId = -1;
                for (int c = 0; c < Labels.Length; c++)
                {
                    float score = output[(4 + c) * rows + i];
                    if (score > maxScore) { maxScore = score; classId = c; }
                }

                if (maxScore > 0.30f)
                {
                    float xCenter = output[0 * rows + i] * frame.Width / 640;
                    float yCenter = output[1 * rows + i] * frame.Height / 640;
                    float width = output[2 * rows + i] * frame.Width / 640;
                    float height = output[3 * rows + i] * frame.Height / 640;

                    detections.Add(new Detection
                    {
                        Rect = new Rect((int)(xCenter - width / 2), (int)(yCenter - height / 2), (int)width, (int)height),
                        Score = maxScore,
                        ClassId = classId
                    });
                }
            }

            var finalDetections = new List<Detection>();
            foreach (var det in detections.OrderByDescending(d => d.Score))
            {
                if (finalDetections.Any(f => IntersectionOverUnion(f.Rect, det.Rect) > 0.4)) continue;
                finalDetections.Add(det);
            }

            foreach (var det in finalDetections)
            {
                string label = Labels[det.ClassId];
                if (!currentFrameCounts.ContainsKey(label)) currentFrameCounts[label] = 0;
                currentFrameCounts[label]++;

                int confidence = (int)(det.Score * 100);
                Cv2.Rectangle(frame, det.Rect, Scalar.Lime, 2);
                Cv2.PutText(frame, $"{label.ToUpper()} %{confidence}", new Point(det.Rect.X, det.Rect.Y - 5),
                            HersheyFonts.HersheySimplex, 0.5, Scalar.White, 1);
            }

            // SESLENDİRME ÇAĞRISI
            Announce(currentFrameCounts);

            string summary = string.Join(" | ", currentFrameCounts.Select(x => $"{x.Value} {x.Key}"));
            Cv2.Rectangle(frame, new Rect(0, 0, frame.Width, 40), Scalar.Black, -1);
            Cv2.PutText(frame, string.IsNullOrEmpty(summary) ? "Meyve Bekleniyor..." : $"Ekranda: {summary}",
                        new Point(10, 25), HersheyFonts.HersheySimplex, 0.6, Scalar.Yellow, 2);
        }

        static double IntersectionOverUnion(Rect rect1, Rect rect2)
        {
            int intersectionArea = (rect1 & rect2).Width * (rect1 & rect2).Height;
            int unionArea = (rect1.Width * rect1.Height) + (rect2.Width * rect2.Height) - intersectionArea;
            if (unionArea <= 0) return 0;
            return (double)intersectionArea / unionArea;
        }

        class Detection
        {
            public Rect Rect { get; set; }
            public float Score { get; set; }
            public int ClassId { get; set; }
        }
    }
}