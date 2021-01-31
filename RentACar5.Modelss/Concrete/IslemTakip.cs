using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentACar5.Modelss.Concrete
{
    public class IslemTakip : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public IslemTakip()
        {
            Musteri = new Musteri();
            Sirket = new Sirket();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IslemTakipID { get; set; }


        [Required(ErrorMessage = "Kira Baslangici girilmedi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KiraBaslangic { get; set; }

        [Required(ErrorMessage = "Kira Baslangici girilmedi.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KiraBitis { get; set; }

        [Required(ErrorMessage = "Tutar girilmedi.")]
        public double Tutar { get; set; }


        [Required(ErrorMessage = "Musterisini girmediniz.")]
        public int MusteriID { get; set; }

        [Required(ErrorMessage = "Sirketini girmediniz.")]
        public int SirketID { get; set; }

        public Musteri Musteri { get; set; }
        public Sirket Sirket { get; set; }
    }
}
