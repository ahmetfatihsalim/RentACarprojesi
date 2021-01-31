using RentACar5.Modelss.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentACar5.Modelss.Concrete
{
    public class Calisan : SirketKullanicisi, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Calisan()
        {
            YoneticiMi = false;
            Sirket = new Sirket();
        }
        [Required(ErrorMessage = "Yoneticisini girmediniz.")]
        public int Yoneticisi { get; set; }
    }
}
