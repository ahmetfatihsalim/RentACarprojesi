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
    public class ArabaRepository : IRepository<Araba>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public ArabaRepository()
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
                query.Append("FROM [dbo].[tbl_Arabalar] ");
                query.Append("WHERE ");
                query.Append("[ArabaID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Arabalar] can't be null. ");

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
                                "Deleting Error for entity [tbl_Arabalar] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("ArabalarRepository:Delete:Error occured.", ex);
            }
        }
        public bool Insert(Araba entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Arabalar] ");
                query.Append("( [Marka], [Model], [ArabaTipi], [ArabaninKmsi], [Aciklama], [Ehliyet], [GunlukKmSiniri], [GunlukKiraFiyati], [KiraliMi], [KiraBaslangic], [KiraBitis], [MusteriID], [SirketID] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @Marka, @Model, @ArabaTipi, @ArabaninKmsi, @Aciklama, @Ehliyet, @GunlukKmSiniri, @GunlukKiraFiyati, @KiraliMi, @KiraBaslangic, @KiraBitis, @MusteriID, @SirketID ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Arabalar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@Marka", CsType.String, ParameterDirection.Input, entity.Marka);
                        DBHelper.AddParameter(dbCommand, "@Model", CsType.String, ParameterDirection.Input, entity.Model);
                        DBHelper.AddParameter(dbCommand, "@ArabaTipi", CsType.String, ParameterDirection.Input, entity.ArabaTipi);
                        DBHelper.AddParameter(dbCommand, "@ArabaninKmsi", CsType.Int, ParameterDirection.Input, entity.ArabaninKmsi);
                        DBHelper.AddParameter(dbCommand, "@Aciklama", CsType.String, ParameterDirection.Input, entity.Aciklama);
                        DBHelper.AddParameter(dbCommand, "@Ehliyet", CsType.Byte, ParameterDirection.Input, entity.Ehliyet);
                        DBHelper.AddParameter(dbCommand, "@GunlukKmSiniri", CsType.Int, ParameterDirection.Input, entity.GunlukKmSiniri);
                        DBHelper.AddParameter(dbCommand, "@GunlukKiraFiyati", CsType.Int, ParameterDirection.Input, entity.GunlukKiraFiyati);
                        DBHelper.AddParameter(dbCommand, "@KiraliMi", CsType.Boolean, ParameterDirection.Input, entity.KiraliMi);
                        DBHelper.AddParameter(dbCommand, "@KiraBaslangic", CsType.DateTime, ParameterDirection.Input, entity.KiraBaslangic);
                        DBHelper.AddParameter(dbCommand, "@KiraBitis", CsType.DateTime, ParameterDirection.Input, entity.KiraBitis);
                        DBHelper.AddParameter(dbCommand, "@MusteriID", CsType.Int, ParameterDirection.Input, entity.MusteriID);
                        DBHelper.AddParameter(dbCommand, "@SirketID", CsType.Int, ParameterDirection.Input, entity.SirketID);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Arabalar] reported the Database ErrorCode: " + _errorCode);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("ArabaRepository:Insert:Error occured.", ex);
            }
        }
        public IList<Araba> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Araba> arabalar = new List<Araba>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[ArabaID], [Marka], [Model], [ArabaTipi], [ArabaninKmsi], [Aciklama], [Ehliyet], [GunlukKmSiniri], [GunlukKiraFiyati], [KiraliMi], [KiraBaslangic], [KiraBitis], [MusteriID], [SirketID] ");
                query.Append("FROM [dbo].[tbl_Arabalar] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Arabalar] can't be null. ");

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
                                    var entity = new Araba();
                                    entity.ArabaID = reader.GetInt32(0);
                                    entity.Marka = reader.GetString(1);
                                    entity.Model = reader.GetString(2);
                                    entity.ArabaTipi = reader.GetString(3);
                                    entity.ArabaninKmsi = reader.GetInt32(4);
                                    entity.Aciklama = reader.GetString(5);
                                    entity.Ehliyet = reader.GetByte(6);
                                    entity.GunlukKmSiniri = reader.GetInt32(7);
                                    entity.GunlukKiraFiyati = reader.GetInt32(8);
                                    entity.KiraliMi = reader.GetBoolean(9);
                                    entity.KiraBaslangic = reader.GetDateTime(10);
                                    entity.KiraBitis = reader.GetDateTime(11);
                                    entity.MusteriID = reader.GetInt32(12);
                                    entity.SirketID = reader.GetInt32(13);
                                    arabalar.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Arabalar] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return arabalar;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("ArabaRepository:SelectAll:Error occured.", ex);
            }
        }
        public Araba SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Araba araba = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[ArabaID], [Marka], [Model], [ArabaTipi], [ArabaninKmsi], [Aciklama], [Ehliyet], [GunlukKmSiniri], [GunlukKiraFiyati], [KiraliMi], [KiraBaslangic], [KiraBitis], [MusteriID], [SirketID] ");
                query.Append("FROM [dbo].[tbl_Arabalar] ");
                query.Append("WHERE ");
                query.Append("[ArabaID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Arabalar] can't be null. ");

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
                                    var entity = new Araba();
                                    entity.ArabaID = reader.GetInt32(0);
                                    entity.Marka = reader.GetString(1);
                                    entity.Model = reader.GetString(2);
                                    entity.ArabaTipi = reader.GetString(3);
                                    entity.ArabaninKmsi = reader.GetInt32(4);
                                    entity.Aciklama = reader.GetString(5);
                                    entity.Ehliyet = reader.GetByte(6);
                                    entity.GunlukKmSiniri = reader.GetInt32(7);
                                    entity.GunlukKiraFiyati = reader.GetInt32(8);
                                    entity.KiraliMi = reader.GetBoolean(9);
                                    entity.KiraBaslangic = reader.GetDateTime(10);
                                    entity.KiraBitis = reader.GetDateTime(11);
                                    entity.MusteriID = reader.GetInt32(12);
                                    entity.SirketID = reader.GetInt32(13);
                                    araba = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Arabalar] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return araba;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("ArabaRepository::SelectById:Error occured.", ex);
            }
        }
        public bool Update(Araba entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Arabalar] ");
                query.Append(" SET [Marka] = @Marka, [Model] = @Model, [ArabaTipi] = @ArabaTipi, [ArabaninKmsi] = @ArabaninKmsi, [Aciklama] = @Aciklama, [Ehliyet] = @Ehliyet, [GunlukKmSiniri] = @GunlukKmSiniri, [GunlukKiraFiyati] = @GunlukKiraFiyati, [KiraliMi] = @KiraliMi, [KiraBaslangic] = @KiraBaslangic, [KiraBitis] = @KiraBitis, [MusteriID] = @MusteriID, [SirketID] = @SirketID, ");
                query.Append(" WHERE ");
                query.Append(" [ArabaID] = @ArabaID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Arabalar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@ArabaID", CsType.Int, ParameterDirection.Input, entity.ArabaID);
                        DBHelper.AddParameter(dbCommand, "@Marka", CsType.String, ParameterDirection.Input, entity.Marka);
                        DBHelper.AddParameter(dbCommand, "@Model", CsType.String, ParameterDirection.Input, entity.Model);
                        DBHelper.AddParameter(dbCommand, "@ArabaTipi", CsType.String, ParameterDirection.Input, entity.ArabaTipi);
                        DBHelper.AddParameter(dbCommand, "@ArabaninKmsi", CsType.Int, ParameterDirection.Input, entity.ArabaninKmsi);
                        DBHelper.AddParameter(dbCommand, "@Aciklama", CsType.String, ParameterDirection.Input, entity.Aciklama);
                        DBHelper.AddParameter(dbCommand, "@Ehliyet", CsType.Byte, ParameterDirection.Input, entity.Ehliyet);
                        DBHelper.AddParameter(dbCommand, "@GunlukKmSiniri", CsType.Int, ParameterDirection.Input, entity.GunlukKmSiniri);
                        DBHelper.AddParameter(dbCommand, "@GunlukKiraFiyati", CsType.Int, ParameterDirection.Input, entity.GunlukKiraFiyati);
                        DBHelper.AddParameter(dbCommand, "@KiraliMi", CsType.Boolean, ParameterDirection.Input, entity.KiraliMi);
                        DBHelper.AddParameter(dbCommand, "@KiraBaslangic", CsType.DateTime, ParameterDirection.Input, entity.KiraBaslangic);
                        DBHelper.AddParameter(dbCommand, "@KiraBitis", CsType.DateTime, ParameterDirection.Input, entity.KiraBitis);
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
                            throw new Exception("Updating Error for entity [tbl_Arabalar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("ArabaRepository::Update:Error occured.", ex);
            }
        }
    }
}
