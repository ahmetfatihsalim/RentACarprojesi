using RenkACar5.Business;
using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calisan = RentACar5.Modelss.Concrete.Calisan;

namespace RentACarMVCLayer.Controllers
{
    public class CalisanController : Controller
    {
        // GET: Calisan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(SelectCalisanByID(id));
        }

        // GET: Calisan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calisan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (InsertCalisan(collection["Ad"], collection["Soyad"], int.Parse(collection["Yas"]), collection["Parola"], double.Parse(collection["KimlikNo"]), int.Parse(collection["Maas"]), byte.Parse(collection["Ehliyet"]), bool.Parse(collection["YoneticiMi"]), int.Parse(collection["SirketID"]), int.Parse(collection["Yoneticisi"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        // GET: Calisan/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectCalisanByID(id));
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateCalisan(id, collection["Ad"], collection["Soyad"], int.Parse(collection["Yas"]), collection["Parola"], double.Parse(collection["KimlikNo"]), int.Parse(collection["Maas"]), byte.Parse(collection["Ehliyet"]), bool.Parse(collection["YoneticiMi"]), int.Parse(collection["SirketID"]), int.Parse(collection["Yoneticisi"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Calisan/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteCalisan(id))
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
                IList<Calisan> Calisan = ListAllCalisan().ToList();
                return View(Calisan);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }

        #region metodlar

        private bool InsertCalisan(string ad, string soyad, int yas, string parola, double kimlikno, int maas, byte ehliyet, bool yoneticimi, int sirketid, int yoneticisi)
        {
            try
            {
                using (var CalisanBussines = new CalisanBusiness())
                {
                    return CalisanBussines.InsertCalisan(new Calisan()
                    {
                        Ad = ad,
                        Soyad = soyad,
                        Yas = yas,
                        Parola = parola,
                        KimlikNo = kimlikno,
                        Maas = maas,
                        Ehliyet = ehliyet,
                        YoneticiMi = yoneticimi,
                        SirketID = sirketid,
                        Yoneticisi = yoneticisi
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }
        private bool UpdateCalisan(int id, string ad, string soyad, int yas, string parola, double kimlikno, int maas, byte ehliyet, bool yoneticimi, int sirketid, int yoneticisi)
        {
            try
            {
                using (var CalisanBussines = new CalisanBusiness())
                {
                    return CalisanBussines.UpdateCalisan(new Calisan()
                    {
                        KullaniciID = id,
                        Ad = ad,
                        Soyad = soyad,
                        Yas = yas,
                        Parola = parola,
                        KimlikNo = kimlikno,
                        Maas = maas,
                        Ehliyet = ehliyet,
                        YoneticiMi = yoneticimi,
                        SirketID = sirketid,
                        Yoneticisi = yoneticisi
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }
        private bool DeleteCalisan(int ID)
        {
            try
            {
                using (var CalisanBussines = new CalisanBusiness())
                {
                    return CalisanBussines.DeleteCalisanById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }
        private List<Calisan> ListAllCalisan()
        {
            try
            {
                using (var CalisanBussines = new CalisanBusiness())
                {
                    List<Calisan> Calisan = CalisanBussines.SelectAllCalisan();
                    return Calisan;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }
        private Calisan SelectCalisanByID(int ID)
        {
            try
            {
                using (var CalisanBussines = new CalisanBusiness())
                {
                    return CalisanBussines.SelectCalisanById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Calisan doesn't exists.");
            }
        }

        #endregion
    }
}