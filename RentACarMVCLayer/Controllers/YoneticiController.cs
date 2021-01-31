using RenkACar5.Business;
using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonetici = RentACar5.Modelss.Concrete.Yonetici;

namespace RentACarMVCLayer.Controllers
{
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(SelectYoneticiByID(id));
        }

        // GET: Yonetici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/Create
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
                if (InsertYonetici(collection["Ad"], collection["Soyad"], int.Parse(collection["Yas"]), collection["Parola"], double.Parse(collection["KimlikNo"]), int.Parse(collection["Maas"]), byte.Parse(collection["Ehliyet"]), bool.Parse(collection["YoneticiMi"]), int.Parse(collection["SirketID"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        // GET: Yonetici/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectYoneticiByID(id));
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
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
                if (UpdateYonetici(id, collection["Ad"], collection["Soyad"], int.Parse(collection["Yas"]), collection["Parola"], double.Parse(collection["KimlikNo"]), int.Parse(collection["Maas"]), byte.Parse(collection["Ehliyet"]), bool.Parse(collection["YoneticiMi"]), int.Parse(collection["SirketID"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Yonetici/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteYonetici(id))
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
                IList<Yonetici> Yonetici = ListAllYonetici().ToList();
                return View(Yonetici);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
            }
        }

        #region metodlar

        private bool InsertYonetici(string ad, string soyad, int yas, string parola, double kimlikno, int maas, byte ehliyet, bool yoneticimi, int sirketid)
        {
            try
            {
                using (var YoneticiBussines = new YoneticiBusiness())
                {
                    return YoneticiBussines.InsertYonetici(new Yonetici()
                    {
                        Ad = ad,
                        Soyad = soyad,
                        Yas = yas,
                        Parola = parola,
                        KimlikNo = kimlikno,
                        Maas = maas,
                        Ehliyet = ehliyet,
                        YoneticiMi = yoneticimi,
                        SirketID = sirketid
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
            }
        }
        private bool UpdateYonetici(int id, string ad, string soyad, int yas, string parola, double kimlikno, int maas, byte ehliyet, bool yoneticimi, int sirketid)
        {
            try
            {
                using (var YoneticiBussines = new YoneticiBusiness())
                {
                    return YoneticiBussines.UpdateYonetici(new Yonetici()
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
                        SirketID = sirketid
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
            }
        }
        private bool DeleteYonetici(int ID)
        {
            try
            {
                using (var YoneticiBussines = new YoneticiBusiness())
                {
                    return YoneticiBussines.DeleteYoneticiById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
            }
        }
        private List<Yonetici> ListAllYonetici()
        {
            try
            {
                using (var YoneticiBussines = new YoneticiBusiness())
                {
                    List<Yonetici> Yonetici = YoneticiBussines.SelectAllYonetici();
                    return Yonetici;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
            }
        }
        private Yonetici SelectYoneticiByID(int ID)
        {
            try
            {
                using (var YoneticiBussines = new YoneticiBusiness())
                {
                    return YoneticiBussines.SelectYoneticiById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Yonetici doesn't exists.");
            }
        }

        #endregion
    }
}