using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.DataAccess.Concrete;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenkACar5.Business
{
    public class CalisanBusiness : IDisposable
    {
        public CalisanBusiness()
        {
            //
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
        public bool InsertCalisan(Calisan entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CalisanRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CalisanBusiness::InsertCalisan::Error occured.", ex);
            }
        }
        public bool UpdateCalisan(Calisan entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CalisanRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CalisanBusiness::UpdateCalisan::Error occured.", ex);
            }
        }
        public bool DeleteCalisanById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CalisanRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CalisanBusiness::DeleteCalisan::Error occured.", ex);
            }
        }
        public Calisan SelectCalisanById(int CalisanId)
        {
            try
            {
                Calisan responseEntitiy;
                using (var repo = new CalisanRepository())
                {
                    responseEntitiy = repo.SelectedById(CalisanId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Calisan doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CalisanBusiness::SelectCalisanById::Error occured.", ex);
            }
        }

        public List<Calisan> SelectAllCalisan()
        {
            var responseEntities = new List<Calisan>();

            try
            {
                using (var repo = new CalisanRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CalisanBusiness::SelectAllCalisan::Error occured.", ex);
            }
        }
    }
}
