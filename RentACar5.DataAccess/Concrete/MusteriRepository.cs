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
    public class MusteriRepository : IRepository<Musteri>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public MusteriRepository()
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
                query.Append("FROM [dbo].[tbl_Kullanicilar] ");
                query.Append("WHERE ");
                query.Append("[KullaniciID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Kullanicilar] can't be null. ");

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
                                "Deleting Error for entity [tbl_Kullanicilar] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::Insert:Error occured.", ex);
            }
        }
        public bool Insert(Musteri entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Kullanicilar] ");
                query.Append("( [Ad], [Soyad], [Yas], [Parola], [KimlikNo], [Maas], [Ehliyet] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @Ad, @Soyad, @Yas, @Parola, @KimlikNo, @Maas, @Ehliyet ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Kullanicilar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@Ad", CsType.String, ParameterDirection.Input, entity.Ad);
                        DBHelper.AddParameter(dbCommand, "@Soyad", CsType.String, ParameterDirection.Input, entity.Soyad);
                        DBHelper.AddParameter(dbCommand, "@Yas", CsType.Int, ParameterDirection.Input, entity.Yas);
                        DBHelper.AddParameter(dbCommand, "@Parola", CsType.String, ParameterDirection.Input, entity.Parola);
                        DBHelper.AddParameter(dbCommand, "@KimlikNo", CsType.Double, ParameterDirection.Input, entity.KimlikNo);
                        DBHelper.AddParameter(dbCommand, "@Maas", CsType.Int, ParameterDirection.Input, entity.Maas);
                        DBHelper.AddParameter(dbCommand, "@Ehliyet", CsType.Byte, ParameterDirection.Input, entity.Ehliyet);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Kullanicilar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Musteri> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Musteri> musteriler = new List<Musteri>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[KullaniciID], [Ad], [Soyad], [Yas], [Parola], [KimlikNo], [Maas], [Ehliyet] ");
                query.Append("FROM [dbo].[tbl_Kullanicilar] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Kullanicilar] can't be null. ");

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
                                    var entity = new Musteri();
                                    entity.KullaniciID = reader.GetInt32(0);
                                    entity.Ad = reader.GetString(1);
                                    entity.Soyad = reader.GetString(2);
                                    entity.Yas = reader.GetInt32(3);
                                    entity.Parola = reader.GetString(4);
                                    entity.KimlikNo = reader.GetInt64(5);
                                    entity.Maas = reader.GetInt32(6);
                                    entity.Ehliyet = reader.GetByte(7);
                                    musteriler.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Kullanicilar] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return musteriler;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::SelectAll:Error occured.", ex);
            }
        }

        public Musteri SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Musteri musteri = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[KullaniciID], [Ad], [Soyad], [Yas], [Parola], [KimlikNo], [Maas], [Ehliyet] ");
                query.Append("FROM [dbo].[tbl_Kullanicilar] ");
                query.Append("WHERE ");
                query.Append("[KullaniciID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Kullanicilar] can't be null. ");

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
                                    var entity = new Musteri();
                                    entity.KullaniciID = reader.GetInt32(0);
                                    entity.Ad = reader.GetString(1);
                                    entity.Soyad = reader.GetString(2);
                                    entity.Yas = reader.GetInt32(3);
                                    entity.Parola = reader.GetString(4);
                                    entity.KimlikNo = reader.GetInt64(5);
                                    entity.Maas = reader.GetInt32(6);
                                    entity.Ehliyet = reader.GetByte(7);
                                    musteri = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Kullanicilar] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                musteri.KiraladigiAraclar = new ArabaRepository().SelectAll().Where(araba => araba.MusteriID.Equals(musteri.KullaniciID)).ToList();
                musteri.Islemlerim = new IslemTakipRepository().SelectAll().Where(islem => islem.MusteriID.Equals(musteri.KullaniciID)).ToList();

                return musteri;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Musteri entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Kullanicilar] ");
                query.Append(" SET [Ad] = @Ad, [Soyad] = @Soyad, [Yas] = @Yas, [Parola] = @Parola, [KimlikNo] = @KimlikNo, [Maas] = @Maas, [Ehliyet] = @Ehliyet ");
                query.Append(" WHERE ");
                query.Append(" [KullaniciID] = @KullaniciID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Kullanicilar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@KullaniciID", CsType.Int, ParameterDirection.Input, entity.KullaniciID);
                        DBHelper.AddParameter(dbCommand, "@Ad", CsType.String, ParameterDirection.Input, entity.Ad);
                        DBHelper.AddParameter(dbCommand, "@Soyad", CsType.String, ParameterDirection.Input, entity.Soyad);
                        DBHelper.AddParameter(dbCommand, "@Yas", CsType.Int, ParameterDirection.Input, entity.Yas);
                        DBHelper.AddParameter(dbCommand, "@Parola", CsType.String, ParameterDirection.Input, entity.Parola);
                        DBHelper.AddParameter(dbCommand, "@KimlikNo", CsType.Double, ParameterDirection.Input, entity.KimlikNo);
                        DBHelper.AddParameter(dbCommand, "@Maas", CsType.Int, ParameterDirection.Input, entity.Maas);
                        DBHelper.AddParameter(dbCommand, "@Ehliyet", CsType.Byte, ParameterDirection.Input, entity.Ehliyet);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Kullanicilar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::Update:Error occured.", ex);
            }
        }
    }
}
