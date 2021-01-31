using RenkACar5.Business;
using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Musteri = RentACar5.Modelss.Concrete.Musteri;

namespace RentACarMVCLayer.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(SelectMusteriByID(id));
        }

        // GET: Musteri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Musteri/Create
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
                if (InsertMusteri(collection["Ad"], collection["Soyad"], int.Parse(collection["Yas"]), collection["Parola"], double.Parse(collection["KimlikNo"]), int.Parse(collection["Maas"]), byte.Parse(collection["Ehliyet"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        // GET: Musteri/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectMusteriByID(id));
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
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
                if (UpdateMusteri(id, collection["Ad"], collection["Soyad"], int.Parse(collection["Yas"]), collection["Parola"], double.Parse(collection["KimlikNo"]), int.Parse(collection["Maas"]), byte.Parse(collection["Ehliyet"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Musteri/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteMusteri(id))
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
                IList<Musteri> Musteri = ListAllMusteri().ToList();
                return View(Musteri);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }

        #region metodlar

        private bool InsertMusteri(string ad, string soyad, int yas, string parola, double kimlikno, int maas, byte ehliyet)
        {
            try
            {
                using (var MusteriBussines = new MusteriBusiness())
                {
                    return MusteriBussines.InsertMusteri(new Musteri()
                    {
                        Ad = ad,
                        Soyad = soyad,
                        Yas = yas,
                        Parola = parola,
                        KimlikNo = kimlikno,
                        Maas = maas,
                        Ehliyet = ehliyet
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }
        private bool UpdateMusteri(int id, string ad, string soyad, int yas, string parola, double kimlikno, int maas, byte ehliyet)
        {
            try
            {
                using (var musteriBussines = new MusteriBusiness())
                {
                    return musteriBussines.UpdateMusteri(new Musteri()
                    {
                        KullaniciID = id,
                        Ad = ad,
                        Soyad = soyad,
                        Yas = yas,
                        Parola = parola,
                        KimlikNo = kimlikno,
                        Maas = maas,
                        Ehliyet = ehliyet
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }
        private bool DeleteMusteri(int ID)
        {
            try
            {
                using (var musteriBussines = new MusteriBusiness())
                {
                    return musteriBussines.DeleteMusteriById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }
        private List<Musteri> ListAllMusteri()
        {
            try
            {
                using (var musteriBussines = new MusteriBusiness())
                {
                    List<Musteri> musteri = musteriBussines.SelectAllMusteri();
                    return musteri;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }
        private Musteri SelectMusteriByID(int ID)
        {
            try
            {
                using (var musteriBussines = new MusteriBusiness())
                {
                    return musteriBussines.SelectMusteriById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Musteri doesn't exists.");
            }
        }

        #endregion
    }
}