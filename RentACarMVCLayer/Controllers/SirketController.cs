using RenkACar5.Business;
using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sirket = RentACar5.Modelss.Concrete.Sirket;

namespace RentACarMVCLayer.Controllers
{
    public class SirketController : Controller
    {
        // GET: Sirket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(SelectSirketByID(id));
        }

        // GET: Sirket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sirket/Create
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
                if (InsertSirket(int.Parse(collection["SirketPuan"]), collection["SirketAd"], collection["SirketAdres"], int.Parse(collection["Yoneticisi"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        // GET: Sirket/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectSirketByID(id));
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
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
                if (UpdateSirket(id, int.Parse(collection["SirketPuan"]), collection["SirketAd"], collection["SirketAdres"], int.Parse(collection["Yoneticisi"])))
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Sirket/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteSirket(id))
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
                IList<Sirket> Sirket = ListAllSirket().ToList();
                return View(Sirket);
            }
            catch //(Exception ex)
            {
                /*LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");*/
                return View();
            }
        }

        #region metodlar

        private bool InsertSirket(int sirketpuan, string sirketad, string sirketadres, int yoneticisi)
        {
            try
            {
                using (var SirketBussines = new SirketBusiness())
                {
                    return SirketBussines.InsertSirket(new Sirket()
                    {
                        SirketPuan = sirketpuan,
                        SirketAd = sirketad,
                        SirketAdres = sirketadres,
                        Yoneticisi = yoneticisi
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }
        private bool UpdateSirket(int id, int sirketpuan, string sirketad, string sirketadres, int yoneticisi)
        {
            try
            {
                using (var SirketBussines = new SirketBusiness())
                {
                    return SirketBussines.UpdateSirket(new Sirket()
                    {
                        SirketID = id,
                        SirketPuan = sirketpuan,
                        SirketAd = sirketad,
                        SirketAdres = sirketadres,
                        Yoneticisi = yoneticisi
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }
        private bool DeleteSirket(int ID)
        {
            try
            {
                using (var SirketBussines = new SirketBusiness())
                {
                    return SirketBussines.DeleteSirketById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }
        private List<Sirket> ListAllSirket()
        {
            try
            {
                using (var SirketBussines = new SirketBusiness())
                {
                    List<Sirket> Sirket = SirketBussines.SelectAllSirket();
                    return Sirket;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }
        private Sirket SelectSirketByID(int ID)
        {
            try
            {
                using (var SirketBussines = new SirketBusiness())
                {
                    return SirketBussines.SelectSirketById(ID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }

        #endregion
    }
}