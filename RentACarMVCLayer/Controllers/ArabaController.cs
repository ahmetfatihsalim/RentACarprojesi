using RenkACar5.Business;
using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Araba = RentACar5.Modelss.Concrete.Araba;

namespace RentACarMVCLayer.Controllers
{
    public class ArabaController : Controller
    {
        // GET: Araba
        public ActionResult Index()
        {
            return View();
        }

        // GET: Araba/Details/5
        public ActionResult Details(int id)
        {
            return View(SelectArabaByID(id));
        }

        // GET: Araba/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Araba/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (InsertAraba(collection["Marka"], collection["Model"], collection["ArabaTipi"], int.Parse(collection["ArabaninKmsi"]), collection["Aciklama"], byte.Parse(collection["Ehliyet"]), int.Parse(collection["GunlukKmSiniri"]), int.Parse(collection["GunlukKiraFiyati"]), bool.Parse(collection["KiraliMi"]), DateTime.Parse(collection["KiraBaslangic"]), DateTime.Parse(collection["KiraBitis"]), int.Parse(collection["MusteriID"]), int.Parse(collection["SirketID"])))
                    return RedirectToAction("ListAll");
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        // GET: Araba/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectArabaByID(id));
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Araba doesn't exists.");
            }
        }

        // POST: Araba/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateAraba(id, collection["Marka"], collection["Model"], collection["ArabaTipi"], int.Parse(collection["ArabaninKmsi"]), collection["Aciklama"], byte.Parse(collection["Ehliyet"]), int.Parse(collection["GunlukKmSiniri"]), int.Parse(collection["GunlukKiraFiyati"]), bool.Parse(collection["KiraliMi"]), DateTime.Parse(collection["KiraBaslangic"]), DateTime.Parse(collection["KiraBitis"]), int.Parse(collection["MusteriID"]), int.Parse(collection["SirketID"])))
                    return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Araba/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteAraba(id))
                    return RedirectToAction("ListAll");
                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Operation failed!", ex);
            }
        }

        public ActionResult ListAll()
        {
            try
            {
                IList<Araba> Araba = ListAllAraba().ToList();
                return View(Araba);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }

        #region metodlar

        private bool InsertAraba(string marka, string model, string arabatipi, int arabaninkmsi, string aciklama, byte ehliyet, int gunlukkmsiniri, int gunlukkirafiyati, bool kiralimi, DateTime kirabaslangic, DateTime kirabitis, int musteriid, int sirketid)
        {
            try
            {
                using (var ArabaBussines = new ArabaBusiness())
                {
                    return ArabaBussines.InsertAraba(new Araba()
                    {
                        Marka = marka,
                        Model = model,
                        ArabaTipi = arabatipi,
                        ArabaninKmsi = arabaninkmsi,
                        Aciklama = aciklama,
                        Ehliyet = ehliyet,
                        GunlukKmSiniri = gunlukkmsiniri,
                        GunlukKiraFiyati = gunlukkirafiyati,
                        KiraliMi = false,
                        KiraBaslangic = kirabaslangic,
                        KiraBitis = kirabitis,
                        MusteriID = musteriid,
                        SirketID = sirketid
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Araba doesn't exists.");
            }
        }
        private bool UpdateAraba(int id, string marka, string model, string arabatipi, int arabaninkmsi, string aciklama, byte ehliyet, int gunlukkmsiniri, int gunlukkirafiyati, bool kiralimi, DateTime kirabaslangic, DateTime kirabitis, int musteriid, int sirketid)
        {
            try
            {
                using (var ArabaBussines = new ArabaBusiness())
                {
                    return ArabaBussines.UpdateAraba(new Araba()
                    {
                        ArabaID = id,
                        Marka = marka,
                        Model = model,
                        ArabaTipi = arabatipi,
                        ArabaninKmsi = arabaninkmsi,
                        Aciklama = aciklama,
                        Ehliyet = ehliyet,
                        GunlukKmSiniri = gunlukkmsiniri,
                        GunlukKiraFiyati = gunlukkirafiyati,
                        KiraliMi = kiralimi,
                        KiraBaslangic = kirabaslangic,
                        KiraBitis = kirabitis,
                        MusteriID = musteriid,
                        SirketID = sirketid
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Araba doesn't exists.");
            }
        }
        private bool DeleteAraba(int ID)
        {
            try
            {
                using (var ArabaBussines = new ArabaBusiness())
                {
                    return ArabaBussines.DeleteArabaById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Araba doesn't exists.");
            }
        }
        private List<Araba> ListAllAraba()
        {
            try
            {
                using (var ArabaBussines = new ArabaBusiness())
                {
                    List<Araba> Araba = ArabaBussines.SelectAllAraba();
                    return Araba;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Araba doesn't exists.");
            }
        }
        private Araba SelectArabaByID(int ID)
        {
            try
            {
                using (var ArabaBussines = new ArabaBusiness())
                {
                    return ArabaBussines.SelectArabaById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Araba doesn't exists.");
            }
        }

        #endregion

    }
}