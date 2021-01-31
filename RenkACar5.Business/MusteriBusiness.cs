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
    public class MusteriBusiness : IDisposable
    {
        public MusteriBusiness()
        {

        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public bool InsertMusteri(Musteri entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new MusteriRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:MusteriBusiness::InsertMusteri::Error occured.", ex);
            }
        }
        public bool UpdateMusteri(Musteri entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new MusteriRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:MusteriBusiness::UpdateMusteri::Error occured.", ex);
            }
        }
        public bool DeleteMusteriById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new MusteriRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:MusteriBusiness::DeleteMusteri::Error occured.", ex);
            }
        }
        public Musteri SelectMusteriById(int MusteriId)
        {
            try
            {
                Musteri responseEntitiy;
                using (var repo = new MusteriRepository())
                {
                    responseEntitiy = repo.SelectedById(MusteriId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Musteri doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:MusteriBusiness::SelectMusteriById::Error occured.", ex);
            }
        }

        public List<Musteri> SelectAllMusteri()
        {
            var responseEntities = new List<Musteri>();

            try
            {
                using (var repo = new MusteriRepository())
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
                throw new Exception("BusinessLogic:MusteriBusiness::SelectAllMusteri::Error occured.", ex);
            }
        }
    }
}
