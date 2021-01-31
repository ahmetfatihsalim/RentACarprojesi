using RentACar5.Modelss.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar5.Modelss.Concrete
{
    public class Musteri : Kullanici, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Musteri()
        {
            KiraladigiAraclar = new List<Araba>();
            Islemlerim = new List<IslemTakip>();
        }

        public List<Araba> KiraladigiAraclar { get; set; }
        public List<IslemTakip> Islemlerim { get; set; }
    }
}
