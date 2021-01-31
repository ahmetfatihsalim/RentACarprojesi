using RentACar5.Commons.Concrete.Data;
using RentACar5.Commons.Concrete.Helpers;
using RentACar5.Commons.Concrete.Loggers;
using RentACar5.DataAccess.Abstract;
using RentACar5.Modelss.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar5.DataAccess.Concrete
{
    public class SirketRepository : IRepository<Sirket>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public SirketRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }

        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Sirketler] ");
                query.Append("WHERE ");
                query.Append("[SirketID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirketler] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_Sirketler] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository:Delete:Error occured.", ex);
            }
        }
        public bool Insert(Sirket entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Sirketler] ");
                query.Append("( [SirketPuan], [SirketAd], [SirketAdres], [Yoneticisi] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @SirketPuan, @SirketAd, @SirketAdres, @Yoneticisi ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Sirketler] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@SirketPuan", CsType.Int, ParameterDirection.Input, entity.SirketPuan);
                        DBHelper.AddParameter(dbCommand, "@SirketAd", CsType.String, ParameterDirection.Input, entity.SirketAd);
                        DBHelper.AddParameter(dbCommand, "@SirketAdres", CsType.String, ParameterDirection.Input, entity.SirketAdres);
                        DBHelper.AddParameter(dbCommand, "@Yoneticisi", CsType.Int, ParameterDirection.Input, entity.Yoneticisi);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Sirketler] reported the Database ErrorCode: " + _errorCode);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::Insert:Error occured.", ex);
            }
        }
        public IList<Sirket> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Sirket> sirketler = new List<Sirket>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[SirketID], [SirketPuan], [SirketAd], [SirketAdres], [Yoneticisi] ");
                query.Append("FROM [dbo].[tbl_Sirketler] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirketler] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.SirketID = reader.GetInt32(0);
                                    entity.SirketPuan = reader.GetInt32(1);
                                    entity.SirketAd = reader.GetString(2);
                                    entity.SirketAdres = reader.GetString(3);
                                    entity.Yoneticisi = reader.GetInt32(4);
                                    sirketler.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Sirketler] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return sirketler;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::SelectAll:Error occured.", ex);
            }
        }
        public Sirket SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Sirket sirket = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[SirketID], [SirketPuan], [SirketAd], [SirketAdres], [Yoneticisi] ");
                query.Append("FROM [dbo].[tbl_Sirketler] ");
                query.Append("WHERE ");
                query.Append("[SirketID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirketler] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.SirketID = reader.GetInt32(0);
                                    entity.SirketPuan = reader.GetInt32(1);
                                    entity.SirketAd = reader.GetString(2);
                                    entity.SirketAdres = reader.GetString(3);
                                    entity.Yoneticisi = reader.GetInt32(4);
                                    sirket = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Sirketler] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                sirket.DepodakiAraclar = new ArabaRepository().SelectAll().Where(araba => araba.SirketID.Equals(sirket.SirketID)).ToList();
                sirket.Kayıtlar = new IslemTakipRepository().SelectAll().Where(islem => islem.SirketID.Equals(sirket.SirketID)).ToList();
                sirket.Calisanlar = new CalisanRepository().SelectAll().Where(calisan => calisan.SirketID.Equals(sirket.SirketID)).ToList();

                return sirket;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::SelectById:Error occured.", ex);
            }
        }
        public bool Update(Sirket entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Sirketler] ");
                query.Append(" SET [SirketPuan] = @SirketPuan, [SirketAd] = @SirketAd, [SirketAdres] = @SirketAdres, [Yoneticisi] = @Yoneticisi ");
                query.Append(" WHERE ");
                query.Append(" [SirketID] = @SirketID ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Sirketler] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@SirketID", CsType.Int, ParameterDirection.Input, entity.SirketID);
                        DBHelper.AddParameter(dbCommand, "@SirketPuan", CsType.Int, ParameterDirection.Input, entity.SirketPuan);
                        DBHelper.AddParameter(dbCommand, "@SirketAd", CsType.String, ParameterDirection.Input, entity.SirketAd);
                        DBHelper.AddParameter(dbCommand, "@SirketAdres", CsType.String, ParameterDirection.Input, entity.SirketAdres);
                        DBHelper.AddParameter(dbCommand, "@Yoneticisi", CsType.Int, ParameterDirection.Input, entity.Yoneticisi);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Sirketler] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::Update:Error occured.", ex);
            }
        }
    }
}
