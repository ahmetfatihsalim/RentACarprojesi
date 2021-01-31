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
    public class SirketBusiness : IDisposable
    {
        public SirketBusiness()
        {

        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public bool InsertSirket(Sirket entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::InsertSirket::Error occured.", ex);
            }
        }
        public bool UpdateSirket(Sirket entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::UpdateSirket::Error occured.", ex);
            }
        }
        public bool DeleteSirketById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::DeleteSirket::Error occured.", ex);
            }
        }
        public Sirket SelectSirketById(int SirketId)
        {
            try
            {
                Sirket responseEntitiy;
                using (var repo = new SirketRepository())
                {
                    responseEntitiy = repo.SelectedById(SirketId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Sirket doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::SelectSirketById::Error occured.", ex);
            }
        }

        public List<Sirket> SelectAllSirket()
        {
            var responseEntities = new List<Sirket>();

            try
            {
                using (var repo = new SirketRepository())
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
                throw new Exception("BusinessLogic:SirketBusiness::SelectAllSirket::Error occured.", ex);
            }
        }
    }
}
