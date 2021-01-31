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
    public class ArabaBusiness : IDisposable
    {
        public ArabaBusiness()
        {
            //
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
        public bool InsertAraba(Araba entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new ArabaRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:ArabaBusiness::InsertAraba::Error occured.", ex);
            }
        }
        public bool UpdateAraba(Araba entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new ArabaRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:ArabaBusiness::UpdateAraba::Error occured.", ex);
            }
        }
        public bool DeleteArabaById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new ArabaRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:ArabaBusiness::DeleteAraba::Error occured.", ex);
            }
        }
        public Araba SelectArabaById(int ArabaId)
        {
            try
            {
                Araba responseEntitiy;
                using (var repo = new ArabaRepository())
                {
                    responseEntitiy = repo.SelectedById(ArabaId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Araba doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:ArabaBusiness::SelectArabaById::Error occured.", ex);
            }
        }
        public List<Araba> SelectAllAraba()
        {
            var responseEntities = new List<Araba>();

            try
            {
                using (var repo = new ArabaRepository())
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
                throw new Exception("BusinessLogic:ArabaBusiness::SelectAllAraba::Error occured.", ex);
            }
        }
    }
}
