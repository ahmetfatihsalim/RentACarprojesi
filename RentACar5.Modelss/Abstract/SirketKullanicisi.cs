using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar5.Modelss.Abstract
{
    public class SirketKullanicisi : Kullanici
    {
        public bool YoneticiMi { get; set; }
        public int SirketID { get; set; }
        public Sirket Sirket { get; set; }
    }
}
