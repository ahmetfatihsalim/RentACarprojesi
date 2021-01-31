using RentACar5.Modelss.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar5.Modelss.Concrete
{
    public class Yonetici : SirketKullanicisi, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Yonetici()
        {
            YoneticiMi = true;
            AltKademedeCalisanlar = new List<Calisan>();
            Sirket = new Sirket();
        }

        public List<Calisan> AltKademedeCalisanlar { get; set; }
    }
}
