using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentACar5.Modelss.Abstract
{
    public class Kullanici
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KullaniciID { get; set; }

        [Required(ErrorMessage = "Ad girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad girmediniz.")]
        [StringLength(50, MinimumLength = 3)]
        public string Soyad { get; set; }

        //[Required(ErrorMessage = "Yas girmediniz.")]
        public int Yas { get; set; }

        [Required(ErrorMessage = "Parola girmediniz.")]
        [StringLength(10, MinimumLength = 5)]
        public string Parola { get; set; }

        [Required(ErrorMessage = "Kimlik No girmediniz.")]
        public double KimlikNo { get; set; }

        //[Required(ErrorMessage = "Maas girmediniz.")]
        public int Maas { get; set; }

        [Required(ErrorMessage = "Ehliyet girmediniz.")]
        public byte Ehliyet { get; set; }
    }
}
