🛒 Görme Engelliler İçin Akıllı Market Asistanı (Object Detection)
Bu proje, görme engelli bireylerin market alışverişi sırasında ürünleri tanımasını ve özellikle taze/bozuk ayrımını (örneğin çürük muz) yapabilmesini sağlayan bir yapay zeka asistanıdır.

🚀 Proje Özeti ve Kapsamı
Proje kapsamında 6 farklı ürün sınıfı (Patlıcan, Muz, Patates, Havuç, Salatalık ve Çürük Muz) üzerinde çalışılmıştır. Model, sadece nesne tespiti yapmakla kalmayıp, bulduğu nesneleri gruplandırarak kullanıcıya doğal bir dille (Örn: "Önünde 3 adet taze muz var") geri bildirim vermektedir.

🛠️ Teknik Altyapı ve Kullanılan Araçlar
Mimari: YOLOv8 (You Only Look Once) - Nano Model.

Dil: Python (OpenCV, PyTorch, Matplotlib).

Ortam: Google Colab (NVIDIA A100 GPU).

Veri Seti: Başlangıçta ~5.000 olan veri sayısı, uygulanan tekniklerle 56.042 etikete çıkarılmıştır.

🔍 Geliştirme Sürecinde Dikkat Ettiğim Kritik Noktalar
1. Veri Temizliği ve Sınıf Senkronizasyonu
Veri setlerini birleştirirken farklı kaynaklardan gelen etiketlerin (Class ID) birbiriyle çakıştığını fark ettim. Tüm etiket dosyalarını (.txt) tek tek tarayan bir script yazarak, sınıf numaralarını (0-5 arası) projemin ana listesine göre yeniden mühürledim. Bu adım, modelin yanlış öğrenmesini engelleyen en kritik müdahaleydi.

2. Veri Dengeleme ve Oversampling (Aşırı Örnekleme)
Veri setinde 24.000 patates resmine karşılık sadece 1.000 çürük muz vardı. Modelin "patates yanlısı" (bias) olmaması için:

Az olan sınıfları silmek yerine Oversampling tekniğiyle çoğalttım.

Bu sınıfları patatesin sayısal seviyesine yaklaştırarak modelin "adaletli" öğrenmesini sağladım.

3. Agresif Veri Artırma (Heavy Augmentation)
Görme engelli bir kullanıcının telefonu titretmesi, eğik tutması veya ışığın yetersiz olması gibi gerçek hayat senaryolarını simüle etmek için şu teknikleri uyguladım:

Mosaic & Mixup: Nesnelerin yarım veya kapalı olduğu durumları öğretmek için.

Perspective Transform: Telefonun farklı açılarda tutulma ihtimaline karşı.

Motion Blur & Noise: El titremesi ve düşük ışık koşullarına karşı dayanıklılık.

4. Hiperparametre Optimizasyonu ve Overfitting Engelleme
Modelin verileri ezberlemesini (overfitting) önlemek adına şu profesyonel denetimleri ekledim:

Early Stopping (Erken Durdurma): 15 tur boyunca iyileşme olmazsa eğitimi en iyi noktada kesmesini sağladım.

Dropout (0.1): Nöronların bir kısmını rastgele kapatarak modelin genelleme yeteneğini artırdım.

Warmup: Eğitimin başında modelin sarsılmaması için ilk 3 turda öğrenme hızını kademeli artırdım.

📊 Eğitim Sonuçları (Metrics)
100 epoch süren eğitim sonucunda elde edilen başarı metrikleri:

mAP@50: %96.9 (Genel Başarı)

mAP@50-95: %82.0 (Hassas Konumlandırma Başarısı)

En Başarılı Sınıf: Çürük Muz (%99.5 başarı ile en ayırt edici sınıf haline getirildi).
