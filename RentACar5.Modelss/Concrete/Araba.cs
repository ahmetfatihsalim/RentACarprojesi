using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentACar5.Modelss.Concrete
{
    public class Araba : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Araba()
        {
            Musteri = new Musteri();
            Sirket = new Sirket();
            KiraliMi = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArabaID { get; set; }

        [Required(ErrorMessage = "Marka girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string Marka { get; set; }

        [Required(ErrorMessage = "Model girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Araba tipi girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string ArabaTipi { get; set; }

        //[Required(ErrorMessage = "Araba kilometresini girmediniz.")]
        public int ArabaninKmsi { get; set; }

        [Required(ErrorMessage = "Aciklama girmediniz.")]
        [StringLength(200, MinimumLength = 3)]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "Ehliyet girmediniz.")]
        public byte Ehliyet { get; set; }

        //[Required(ErrorMessage = "Kilometre siniri girmediniz.")]
        public int GunlukKmSiniri { get; set; }

        //[Required(ErrorMessage = "Kira Fiyati girmediniz.")]
        public int GunlukKiraFiyati { get; set; }

        [Required(ErrorMessage = "Kira Durumu girmediniz.")]
        public bool KiraliMi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KiraBaslangic { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KiraBitis { get; set; }


        //[Required(ErrorMessage = "Musterisini girmediniz.")]
        public int MusteriID { get; set; }

        //[Required(ErrorMessage = "Sirketini girmediniz.")]
        public int SirketID { get; set; }

        public Musteri Musteri { get; set; }
        public Sirket Sirket { get; set; }
    }
}
