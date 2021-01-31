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
    public class YoneticiBusiness : IDisposable
    {
        public YoneticiBusiness()
        {
            //Auto
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public bool InsertYonetici(Yonetici entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new YoneticiRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:YoneticiBusiness::InsertYonetici::Error occured.", ex);
            }
        }
        public bool UpdateYonetici(Yonetici entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new YoneticiRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:YoneticiBusiness::UpdateYonetici::Error occured.", ex);
            }
        }
        public bool DeleteYoneticiById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new YoneticiRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:YoneticiBusiness::DeleteYonetici::Error occured.", ex);
            }
        }
        public Yonetici SelectYoneticiById(int YoneticiId)
        {
            try
            {
                Yonetici responseEntitiy;
                using (var repo = new YoneticiRepository())
                {
                    responseEntitiy = repo.SelectedById(YoneticiId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Yonetici doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:YoneticiBusiness::SelectYoneticiById::Error occured.", ex);
            }
        }

        public List<Yonetici> SelectAllYonetici()
        {
            var responseEntities = new List<Yonetici>();

            try
            {
                using (var repo = new YoneticiRepository())
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
                throw new Exception("BusinessLogic:YoneticiBusiness::SelectAllYonetici::Error occured.", ex);
            }
        }
    }
}
