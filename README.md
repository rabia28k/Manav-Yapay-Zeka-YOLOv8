# 🍎 Manav Yapay Zekası - YOLOv8 Meyve ve Sebze Tanıma

Bu proje, derin öğrenme yöntemleri kullanılarak manav ürünlerini (Patlıcan, Muz, Patates, Havuç, Salatalık) otomatik olarak tanımak için geliştirilmiştir.

## 📊 Proje Performansı
Modelimiz 50 epoch sonunda şu değerlere ulaşmıştır:
- **Genel Doğruluk (mAP50):** %95.8
- **İşlem Hızı:** Görüntü başına ~2.3ms

## 📂 Dosya İçerikleri
- **Manav_Yapi_Egitim.ipynb**: Google Colab üzerinden T4 GPU ile yapılan eğitim süreci.
- **best.pt**: Eğitilmiş en iyi model ağırlıkları.
- **data_config.yaml**: Sınıf isimleri ve veri seti yollarını içeren konfigürasyon.

## 🥒 Tanınan Sınıflar
1. Patlıcan
2. Muz
3. Patates
4. Havuç
5. Salatalık
