==================================================================
YOLOV8 TABANLI GERÇEK ZAMANLI AKILLI MARKET ASİSTANI FINAL PROJESİ
==================================================================

PROJE GRUP ÜYESİ:
-----------------
Adı Soyadı: Rabia Korkmaz
Öğrenci No: 23040101035
Bölüm: Bilgisayar Mühendisliği

PROJE ÖZETİ:
------------
Bu proje, görme engelli bireylerin market ve manav alışverişlerinde reyonlardaki 
ürünleri (Patlıcan, Muz, Patates, Havuç, Salatalık ve Çürük Muz) canlı kamera 
akışı üzerinden asenkron sesli bildirimlerle tespit edebilmesini sağlayan 
C# .NET ve ONNX tabanlı bir masaüstü otomasyon sistemidir.

SİSTEM GEREKSİNİMLERİ VE KÜTÜPHANELER:
--------------------------------------
- IDE: Visual Studio / VS Code
- Framework: .NET
- Gerekli NuGet Paketleri:
  * Microsoft.ML.OnnxRuntime
  * OpenCvSharp4
  * OpenCvSharp4.Extensions
  * System.Speech (Windows TTS Motoru için)

PROJENİN ÇALIŞTIRILMA ADIMLARI:
-------------------------------
1. Mobil cihazınıza herhangi bir "IP Webcam" uygulaması kurun ve canlı akışı başlatın.
2. "1_Kaynak_Kodlar" klasöründeki C# projesini Visual Studio ile açın.
3. Kod içerisindeki IP adresi alanına, mobil cihazınızın sağladığı canlı yayın HTTP URL'sini girin.
4. "3_Model_ve_Ağırlıklar" klasöründeki "best.onnx" dosyasının yolunun C# kodunda doğru tanımlandığından emin olun.
5. Projeyi "Build" edin ve başlatın.
6. Sistem aktif olduğunda sol üstte yeşil durum LED'i yanacak, ekrandaki nesneler sesli olarak okunurken sağ üstteki "Sepet Detayı" paneline ürün miktarları yansıtılacaktır.


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


