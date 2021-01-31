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
    public class IslemTakipRepository : IRepository<IslemTakip>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public IslemTakipRepository()
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
                query.Append("FROM [dbo].[tbl_IslemTakip] ");
                query.Append("WHERE ");
                query.Append("[IslemTakipID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_IslemTakip] can't be null. ");

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
                                "Deleting Error for entity [tbl_IslemTakip] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IslemTakipRepository::Delete:Error occured.", ex);
            }
        }
        public bool Insert(IslemTakip entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_IslemTakip] ");
                query.Append("( [KiraBaslangic], [KiraBitis], [Tutar], [MusteriID], [SirketID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @KiraBaslangic, @KiraBitis, @Tutar, @MusteriID, @SirketID ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_IslemTakip] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@KiraBaslangic", CsType.DateTime, ParameterDirection.Input, entity.KiraBaslangic);
                        DBHelper.AddParameter(dbCommand, "@KiraBitis", CsType.DateTime, ParameterDirection.Input, entity.KiraBitis);
                        DBHelper.AddParameter(dbCommand, "@Tutar", CsType.Double, ParameterDirection.Input, entity.Tutar);
                        DBHelper.AddParameter(dbCommand, "@MusteriID", CsType.Int, ParameterDirection.Input, entity.MusteriID);
                        DBHelper.AddParameter(dbCommand, "@SirketID", CsType.Int, ParameterDirection.Input, entity.SirketID);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_IslemTakip] reported the Database ErrorCode: " + _errorCode);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IslemTakipRepository::Insert:Error occured.", ex);
            }
        }
        public IList<IslemTakip> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<IslemTakip> islemler = new List<IslemTakip>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[IslemTakipID], [KiraBaslangic], [KiraBitis], [Tutar], [MusteriID], [SirketID] ");
                query.Append("FROM [dbo].[tbl_IslemTakip] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_IslemTakip] can't be null. ");

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
                                    var entity = new IslemTakip();
                                    entity.IslemTakipID = reader.GetInt32(0);
                                    entity.KiraBaslangic = reader.GetDateTime(1);
                                    entity.KiraBitis = reader.GetDateTime(2);
                                    entity.Tutar = reader.GetInt64(3);
                                    entity.MusteriID = reader.GetInt32(4);
                                    entity.SirketID = reader.GetInt32(5);
                                    islemler.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_IslemTakip] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return islemler;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IslemTakipRepository::SelectAll:Error occured.", ex);
            }
        }
        public IslemTakip SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IslemTakip islemler = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[IslemTakipID], [KiraBaslangic], [KiraBitis], [Tutar], [MusteriID], [SirketID] ");
                query.Append("FROM [dbo].[tbl_IslemTakip] ");
                query.Append("WHERE ");
                query.Append("[IslemTakipID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_IslemTakip] can't be null. ");

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
                                    var entity = new IslemTakip();
                                    entity.IslemTakipID = reader.GetInt32(0);
                                    entity.KiraBaslangic = reader.GetDateTime(1);
                                    entity.KiraBitis = reader.GetDateTime(2);
                                    entity.Tutar = reader.GetInt64(3);
                                    entity.MusteriID = reader.GetInt32(4);
                                    entity.SirketID = reader.GetInt32(5);
                                    islemler = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_IslemTakip] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return islemler;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IslemTakipRepository::SelectById:Error occured.", ex);
            }
        }
        public bool Update(IslemTakip entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_IslemTakip] ");
                query.Append(" SET [KiraBaslangic] = @KiraBaslangic, [KiraBitis] = @KiraBitis, [Tutar] = @Tutar, [MusteriID] = @MusteriID, [SirketID] = @SirketID ");
                query.Append(" WHERE ");
                query.Append(" [IslemTakipID] = @IslemTakipID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_IslemTakip] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@IslemTakipID", CsType.Int, ParameterDirection.Input, entity.IslemTakipID);
                        DBHelper.AddParameter(dbCommand, "@KiraBaslangic", CsType.DateTime, ParameterDirection.Input, entity.KiraBaslangic);
                        DBHelper.AddParameter(dbCommand, "@KiraBitis", CsType.DateTime, ParameterDirection.Input, entity.KiraBitis);
                        DBHelper.AddParameter(dbCommand, "@Tutar", CsType.Double, ParameterDirection.Input, entity.Tutar);
                        DBHelper.AddParameter(dbCommand, "@MusteriID", CsType.Int, ParameterDirection.Input, entity.MusteriID);
                        DBHelper.AddParameter(dbCommand, "@SirketID", CsType.Int, ParameterDirection.Input, entity.SirketID);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_IslemTakip] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("IslemTakipRepository::Update:Error occured.", ex);
            }
        }
    }
}
