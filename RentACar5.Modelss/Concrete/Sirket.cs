using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentACar5.Modelss.Concrete
{
    public class Sirket : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Sirket()
        {
            DepodakiAraclar = new List<Araba>();
            Kayıtlar = new List<IslemTakip>();
            Calisanlar = new List<Calisan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SirketID { get; set; }
        public int SirketPuan { get; set; }

        [Required(ErrorMessage = "Ad girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string SirketAd { get; set; }

        [Required(ErrorMessage = "Adres girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string SirketAdres { get; set; }


        //[Required(ErrorMessage = "Yoneticisini girmediniz.")]
        public int Yoneticisi { get; set; }


        public List<Araba> DepodakiAraclar { get; set; }
        public List<IslemTakip> Kayıtlar { get; set; }
        public List<Calisan> Calisanlar { get; set; }
    }
}
