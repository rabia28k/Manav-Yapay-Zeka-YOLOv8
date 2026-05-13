🍎 Manav Asistanı: YOLOv8 & C# Real-Time Nesne Algılama
Bu proje, görme engelli bireylerin market alışverişi sırasında reyonlardaki ürünleri bağımsız bir şekilde tanıyabilmesi ve miktarını öğrenebilmesi amacıyla geliştirilmiş, yapay zeka tabanlı bir asistan uygulamasıdır. Sistem, kameradan gelen görüntüleri gerçek zamanlı olarak analiz ederek meyve ve sebzeleri türlerine göre yüksek doğrulukla tanımlamakla kalmaz; aynı zamanda ekrandaki her bir ürünü tek tek sayarak toplam adet bilgisini (Örn: "3 adet elma ve 1 adet muz bulundu") anlık olarak hesaplar. Elde edilen bu veriler, kullanıcıya asenkron bir ses motoru aracılığıyla Türkçe olarak bildirilerek hem ürünün ne olduğu hem de kaç tane olduğu konusunda tam bir farkındalık sağlayan akıllı bir alışveriş deneyimi sunar.

🚀 Temel Özellikler
YOLOv8 Entegrasyonu: Google Colab üzerinde eğitilmiş özel nesne algılama modeli.

Gerçek Zamanlı İşleme: C# ve OpenCvSharp kullanılarak optimize edilmiş kamera akışı.

Düşük Gecikme (Latency): Buffer ve Frame Skipping teknikleriyle sıfıra yakın gecikme.

Erişilebilirlik: Asenkron (Async) ses motoru ile anlık sesli bilgilendirme.

🛠️ Kullanılan Teknolojiler
Yapay Zeka: YOLOv8 (Ultralytics), ONNX Runtime.

Görüntü İşleme: OpenCvSharp4.

Programlama Dili: C# (.NET 8.0/6.0).

Eğitim Ortamı: Google Colab (Python & PyTorch).

