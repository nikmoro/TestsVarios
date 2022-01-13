using DevExpress17EmptyWebForms.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DevExpress17EmptyWebForms.BLL
{
    public abstract class BasicAccess
    {
        #region Enum's
        /// <summary>
        /// Acciones Básicas
        /// </summary>
        protected enum enumBasicCurrentAction
        {
            Nothing = 0,
            Add = 1,
            Update = 2,
            Delete = 3,
            Delete_Virtual = 4,
            Load = 5,
            Other = 99
        }

        /// <summary>
        /// Esquemas utilizados en la base de datos
        /// </summary>
        protected enum enumBasicDBSchema
        {
            conta = 1,
            pres = 2,
            mate = 3,
            dbo = 4
        }

        /// <summary>
        /// Resultados por tipos de comandos
        /// </summary>
        private enum dbResultCommandType
        {
            Datatable = 1,
            Scalar = 2,
            Execute = 3
        }
        #endregion

        #region Propiedades
        private enumBasicCurrentAction _currentAction = enumBasicCurrentAction.Nothing;
        private string _errorDescription = "";
        private Exception _errorException = null;
        private iepcdbEntities _context = null;
        private bool _boContextExterno = false;

        /// <summary>
        /// Objeto padre que hereda de BasicAccess y que se envia para usar su Context
        /// </summary>
        private BasicAccess _parent;

        private bool _activeProccess = false;

        protected enumBasicCurrentAction CurrectAction
        {
            get { return _currentAction; }
        }

        protected string cAddErrorPrefix
        {
            get { return "(Error al agregar objeto). "; }
        }

        protected string cUpdateErrorPrefix
        {
            get { return "(Error al actualizar objeto). "; }
        }

        protected string cDeleteErrorPrefix
        {
            get { return "(Error al eliminar objeto). "; }
        }

        protected string cDeleteVirtualErrorPrefix
        {
            get { return "(Error al eliminar* objeto). "; }
        }

        protected string cLoadErrorPrefix
        {
            get { return "(Error al cargar objeto). "; }
        }

        /// <summary>
        /// Obtiene la instancia de la Entidad (Si es nula se crea)
        /// </summary>
        protected iepcdbEntities Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new iepcdbEntities();
                    _boContextExterno = false;
                }
                return _context;
            }
        }

        /// <summary>
        /// Indica si se ha iniciado un proceso con BeginProcess
        /// </summary>
        public bool ActiveProccess
        {
            get { return _activeProccess; }
        }

        public string ErrorDescripcion
        {
            get { return _errorDescription; }
        }

        public Exception ErrorException
        {
            get { return _errorException; }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Indica el inicio de una acción (Nothing por defecto)
        /// </summary>
        protected void InitAction() => InitAction(enumBasicCurrentAction.Nothing);

        /// <summary>
        /// Indica el inicio de una acción
        /// </summary>
        /// <param name="action">Acción Básica</param>
        protected void InitAction(enumBasicCurrentAction action)
        {
            ClearError();
            _currentAction = action;
        }

        /// <summary>
        /// Establece el tiempo de espera para todas las operaciones de contexto.
        /// </summary>
        /// <param name="t">Tiempo en segundos</param>
        protected void SetContextTimeOut(int t) => Context.Database.CommandTimeout = t;

        /// <summary>
        /// Obtiene la instancia del objeto si ya está creada
        /// </summary>
        /// <param name="parent">Representa la clase Base</param>
        protected void SetBasicParent(BasicAccess parent)
        {
            if (_context == null)
            {
                if (parent != null)
                {
                    _parent = parent;
                    _context = parent.Context;
                    _boContextExterno = true;
                }
                else
                {
                    throw new Exception("No es posible asignar un objeto que aún no ha sido creado.");
                }
            }
            else
            {
                throw new Exception("No es posible reemplazar el objeto context porque el objeto actual ya se encuentra creado.");
            }
        }

        /// <summary>
        /// Libera memoria del uso del Context
        /// </summary>
        protected void DisposeBasicAccess()
        {
            try
            {
                if (_context != null)
                {
                    if (!_boContextExterno)
                    {
                        _context.Dispose();
                    }
                    _context = null;
                }
                _parent = null;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Indica al contexto que se abrá una conexión y se mantenga abierta hasta que se ejecute EndProcess
        /// </summary>
        protected void BeginProcess()
        {
            if (Context.Database.Connection.State == ConnectionState.Closed)
            {
                Context.Database.Connection.Open();
            }
            _activeProccess = true;
        }

        /// <summary>
        /// Indica al contexto que un proceso ha terminado y que cierre la conexión actual.
        /// </summary>
        protected void EndProcess()
        {
            if (Context.Database.Connection.State == ConnectionState.Open)
            {
                Context.Database.Connection.Close();
            }
            _activeProccess = false;
        }

        /// <summary>
        /// Genera un Id
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <param name="keyFieldName">Nombre del campo clave de la tabla</param>
        /// <returns>Id generado de tipo Objeto entero</returns>
        protected int GetNextId(string tableName, string keyFieldName)
        {
            return GetNextId(tableName, keyFieldName, enumBasicDBSchema.dbo);
        }

        /// <summary>
        /// Genera un Id en una tabla de un esquema específico
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <param name="keyFieldName">Nombre del campo clave de la tabla</param>
        /// <param name="schema">Esquema usado en la tabla (dbo por defecto)</param>
        /// <returns>Id generado de tipo Objeto entero</returns>
        protected int GetNextId(string tableName, string keyFieldName, enumBasicDBSchema schema)
        {
            object v = ExecuteScalar("SELECT (ISNULL(MAX(" + keyFieldName + "),0)+1) AS id FROM " + getCompleteTableName(schema, tableName));
            if (!Convert.IsDBNull(v) && v != null)
            {
                return Convert.ToInt32(v);
            }
            else
            {
                throw new Exception(ErrorDescripcion);
            }
        }

        /// <summary>
        /// Borra un registro
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <param name="keyFieldName">Nombre del campo clave de la tabla</param>
        /// <param name="keyFieldValue">Valor del campo clave</param>
        protected void DeleteEntity(string tableName, string keyFieldName, int keyFieldValue)
        {
            DeleteEntity(tableName, keyFieldName, keyFieldValue, enumBasicDBSchema.mate);
        }

        /// <summary>
        /// Borra un registro de un esquema específico
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <param name="keyFieldName">Nombre del campo clave de la tabla</param>
        /// <param name="keyFieldValue">Valor del campo clave</param>
        /// <param name="schema">Esquema usado en la tabla (mate por defecto)</param>
        protected void DeleteEntity(string tableName, string keyFieldName, int keyFieldValue, enumBasicDBSchema schema)
        {
            int rowsAffected = Execute("DELETE FROM " + getCompleteTableName(schema, tableName) + " WHERE " + keyFieldName + "=@id", CreateParameter("id", SqlDbType.Int, keyFieldValue));
            if (rowsAffected <= 0)
            {
                throw new Exception("El comando no devolvió ningún resultado. " + ErrorDescripcion);
            }
        }

        /// <summary>
        /// Borrado virtual
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <param name="keyFieldName">Nombre del campo clave de la tabla</param>
        /// <param name="keyFieldValue">Valor del campo clave</param>
        protected void DeleteEntity_Virtual(string tableName, string keyFieldName, int keyFieldValue)
        {
            DeleteEntity_Virtual(tableName, keyFieldName, keyFieldValue, enumBasicDBSchema.mate);
        }

        /// <summary>
        /// Borrado virtual de un esquema específico
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <param name="keyFieldName">Nombre del campo clave de la tabla</param>
        /// <param name="keyFieldValue">Valor del campo clave</param>
        /// <param name="schema">Esquema usado en la tabla (mate por defecto)</param>
        protected void DeleteEntity_Virtual(string tableName, string keyFieldName, int keyFieldValue, enumBasicDBSchema schema)
        {
            int rowsAffected = Execute("UPDATE " + getCompleteTableName(schema, tableName) + " SET Eliminado=1, Modificado=GETDATE() WHERE " + keyFieldName + "=@id", CreateParameter("id", SqlDbType.Int, keyFieldValue));
            if (rowsAffected <= 0)
            {
                throw new Exception("El comando no devolvió ningún resultado. " + ErrorDescripcion);
            }
        }

        protected bool ExistsRow(string tableName, string keyFieldName, string whereFieldName, SqlDbType wheredbType, object whereValue)
        {
            return ExistsRow(tableName, keyFieldName, whereFieldName, wheredbType, whereValue, null, null, null, enumBasicDBSchema.mate);
        }

        protected bool ExistsRow(string tableName, string keyFieldName, string whereFieldName, SqlDbType wheredbType, object whereValue, string whereFieldName2, SqlDbType? wheredbType2, object whereValue2, enumBasicDBSchema schema)
        {
            if (GetKeyFieldValue(tableName, keyFieldName, whereFieldName, wheredbType, whereValue, null, null, null, schema) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected int? GetKeyFieldValue(string tableName, string keyFieldName, string whereFieldName, SqlDbType wheredbType, object whereValue)
        {
            return GetKeyFieldValue(tableName, keyFieldName, whereFieldName, wheredbType, whereValue, null, null, null, enumBasicDBSchema.mate);
        }

        protected int? GetKeyFieldValue(string tableName, string keyFieldName, string whereFieldName, SqlDbType wheredbType, object whereValue, string whereFieldName2, SqlDbType? wheredbType2, object whereValue2, enumBasicDBSchema schema)
        {
            SqlParameter[] parameters;
            try
            {
                string swhere2 = "";

                if (whereFieldName2 != null)
                {
                    parameters = new SqlParameter[2];
                    swhere2 = " AND " + whereFieldName2 + "=@xfield2";
                    parameters[1] = CreateParameter("xfield2", (System.Data.SqlDbType)wheredbType2, whereValue2);
                }
                else
                {
                    parameters = new SqlParameter[1];
                }

                parameters[0] = CreateParameter("xfield", wheredbType, whereValue);

                object v = ExecuteScalar("SELECT " + keyFieldName + " as id FROM " + getCompleteTableName(schema, tableName) + " WHERE " + whereFieldName + "=@xfield" + swhere2, parameters);

                if (!Convert.IsDBNull(v) && v != null)
                {
                    return Convert.ToInt32(v);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                SetError(ex);
                return null;
            }
            finally
            {
                parameters = null;
            }
        }
        #endregion

        #region Execute
        /// <summary>
        /// Ejecuta un sentencia SQL
        /// </summary>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="parameters">Parámetros de la sentencia</param>
        /// <returns>Tipo entero</returns>
        protected int Execute(string sqlCommand, params SqlParameter[] parameters)
        {
            object v = ExecuteCommand(dbResultCommandType.Execute, sqlCommand, parameters);

            if (v != null)
            {
                return Convert.ToInt32(v);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Ejecuta un Store Procedure
        /// </summary>
        /// <param name="sqlCommand">Nombre del Store Procedure</param>
        /// <param name="parameters">Parámetros del Store Procedure</param>
        /// <returns>Tipo entero</returns>
        protected int ExecuteSp(string sqlCommand, params SqlParameter[] parameters)
        {
            object v = ExecuteSpCommand(dbResultCommandType.Execute, sqlCommand, parameters);

            if (v != null)
            {
                return Convert.ToInt32(v);  // Número de filas afectadas usando ExecuteNonQuery()
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Execute Insert
        /// <summary>
        ///  Este método solo se debe usar el campo llave de la tabla que es autoincrementable (IDENTITY(1,1))
        /// </summary>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="parameters">Parámetros de la sentencia</param>
        /// <returns>Devuelve el identificador (primary key) que fue creado. (de tipo Objeto entero)</returns>
        protected int ExecuteInsert(string sqlCommand, params SqlParameter[] parameters)
        {
            if (sqlCommand.Substring(sqlCommand.Length - 1, 1) != ";")
            {
                sqlCommand += ";";
            }

            sqlCommand += " SELECT SCOPE_IDENTITY();";

            object v = ExecuteScalar(sqlCommand, parameters);

            if (!Convert.IsDBNull(v) && v != null)
            {
                return Convert.ToInt32(v);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Execute Query (DaTable)
        /// <summary>
        /// Ejecuta consulta básica y recibe diferentes valores por parámetros
        /// </summary>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="pName">Nombre del parámetro</param>
        /// <param name="pType">Tipo del parámetro</param>
        /// <param name="pValue">Valor del parámetro</param>
        /// <returns>DataTable</returns>
        protected DataTable ExecuteBasicQuery(string sqlCommand, string pName, SqlDbType pType, object pValue)
        {
            return ExecuteBasicQuery(sqlCommand, CreateParameter(pName, pType, pValue));
        }

        /// <summary>
        /// Ejecuta consulta básica
        /// </summary>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="parameters">Arreglo de parámetros</param>
        /// <returns>DataTable</returns>
        protected DataTable ExecuteBasicQuery(string sqlCommand, params SqlParameter[] parameters)
        {
            var dt = ExecuteCommand(dbResultCommandType.Datatable, sqlCommand, parameters);
            if (dt != null)
            {
                return (DataTable)dt;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Execute Scalar

        /// <summary>
        /// Ejecuta una sentencia del tipo Scalar (que devuelve la primera columna de la primera fila en el conjunto)
        /// </summary>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="pName">Nombre del parámetro</param>
        /// <param name="pType">Tipo del parámetro</param>
        /// <param name="pValue">Valor del parámetro</param>
        /// <returns>Objeto</returns>
        protected object ExecuteScalar(string sqlCommand, string pName, SqlDbType pType, object pValue)
        {
            return ExecuteScalar(sqlCommand, CreateParameter(pName, pType, pValue));
        }

        /// <summary>
        /// Ejecuta una sentencia del tipo Scalar (que devuelve la primera columna de la primera fila en el conjunto)
        /// </summary>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="parameters">Arreglo de parámetros</param>
        /// <returns>Objeto</returns>
        protected object ExecuteScalar(string sqlCommand, params SqlParameter[] parameters)
        {
            return ExecuteCommand(dbResultCommandType.Scalar, sqlCommand, parameters);
        }
        #endregion

        #region Métodos privados
        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        /// <param name="type">Scalar, Datatable o Execute</param>
        /// <param name="sqlCommand">Sentencia SQL</param>
        /// <param name="parameters">Arreglo de parámetros</param>
        /// <returns>Resultado por tipo de comandos</returns>
        private object ExecuteCommand(dbResultCommandType type, string sqlCommand, params SqlParameter[] parameters)
        {
            bool boConnectionOpenedHere = false;
            DataTable dt;
            SqlCommand cmd = null;

            try
            {
                if (Context.Database.Connection.State == ConnectionState.Closed)
                {
                    Context.Database.Connection.Open();
                    boConnectionOpenedHere = true;
                }

                //cmd = ((System.Data.SqlClient.SqlConnection)((System.Data.EntityClient.EntityConnection)Context.Connection).StoreConnection).CreateCommand();

                cmd = (SqlCommand)Context.Database.Connection.CreateCommand();
                cmd.CommandText = sqlCommand;
                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }

                switch (type)
                {
                    case dbResultCommandType.Datatable:
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                    case dbResultCommandType.Scalar:
                        return cmd.ExecuteScalar();
                    case dbResultCommandType.Execute:
                        return cmd.ExecuteNonQuery();
                    default:
                        throw new Exception("Tipo incorrecto");
                }
            }
            catch (Exception ex)
            {
                SetError(ex);
                return null;
            }
            finally
            {
                if (boConnectionOpenedHere)
                {
                    if (Context.Database.Connection.State == ConnectionState.Open)
                    {
                        Context.Database.Connection.Close();
                    }
                }

                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// Método que ejecuta el comando
        /// </summary>
        /// <param name="type">Scalar, Datatable o Execute (Execute por defecto)</param>
        /// <param name="sqlCommand">Nombre del Store Procedure</param>
        /// <param name="parameters">Arreglo de parámetros</param>
        /// <returns>Resultado por tipo de comandos</returns>
        private object ExecuteSpCommand(dbResultCommandType type, string sqlCommand, params SqlParameter[] parameters)
        {
            bool boConnectionOpenedHere = false;
            DataTable dt;
            SqlCommand cmd = null;

            try
            {
                if (Context.Database.Connection.State == ConnectionState.Closed)
                {
                    Context.Database.Connection.Open();
                    boConnectionOpenedHere = true;
                }

                //cmd = ((System.Data.SqlClient.SqlConnection)((System.Data.EntityClient.EntityConnection)Context.Connection).StoreConnection).CreateCommand();

                cmd = (SqlCommand)Context.Database.Connection.CreateCommand();
                cmd.CommandText = sqlCommand;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }

                switch (type)
                {
                    case dbResultCommandType.Datatable:
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                    case dbResultCommandType.Scalar:
                        return cmd.ExecuteScalar();
                    case dbResultCommandType.Execute:
                        return cmd.ExecuteNonQuery();
                    default:
                        throw new Exception("Tipo incorrecto");
                }
            }
            catch (Exception ex)
            {
                SetError(ex);
                return null;
            }
            finally
            {
                if (boConnectionOpenedHere)
                {
                    if (Context.Database.Connection.State == ConnectionState.Open)
                    {
                        Context.Database.Connection.Close();
                    }
                }

                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene el nombre completo de una tabla
        /// </summary>
        /// <param name="schema">Esquema de la tabla</param>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <returns>esquema.NombreDeLaTabla</returns>
        private string getCompleteTableName(enumBasicDBSchema schema, string tableName)
        {
            string tschema = "";

            switch (schema)
            {
                case enumBasicDBSchema.conta:
                    tschema = "conta";
                    break;
                case enumBasicDBSchema.pres:
                    tschema = "pres";
                    break;
                case enumBasicDBSchema.mate:
                    tschema = "mate";
                    break;
                case enumBasicDBSchema.dbo:
                    tschema = "dbo";
                    break;
            }
            return tschema + "." + tableName;
        }

        protected void ClearError()
        {
            _errorDescription = "";
            _errorException = null;
        }

        protected void SetError(Exception exception)
        {
            SetError(exception, true);
        }

        /// <summary>
        /// Devuelve los errores por tipo de acción
        /// </summary>
        /// <param name="exception">Excepción</param>
        /// <param name="boClearCurrentAction">Limpiar la accción actual (true por defecto)</param>
        protected void SetError(Exception exception, bool boClearCurrentAction)
        {
            try
            {
                _errorException = exception;

                string prefix = "";

                switch (_currentAction)
                {
                    case enumBasicCurrentAction.Nothing:
                        break;
                    case enumBasicCurrentAction.Add:
                        prefix = cAddErrorPrefix;
                        break;
                    case enumBasicCurrentAction.Update:
                        prefix = cUpdateErrorPrefix;
                        break;
                    case enumBasicCurrentAction.Delete:
                        prefix = cDeleteErrorPrefix;
                        break;
                    case enumBasicCurrentAction.Delete_Virtual:
                        prefix = cDeleteVirtualErrorPrefix;
                        break;
                    case enumBasicCurrentAction.Load:
                        prefix = cLoadErrorPrefix;
                        break;
                    case enumBasicCurrentAction.Other:
                        break;
                    default:
                        break;
                }
                _errorDescription = prefix;

                if (exception != null)
                {
                    if (exception.Message != null)
                        _errorDescription += exception.Message;
                }
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.Message != null)
                        _errorDescription += " " + exception.InnerException.Message;
                }

                if (boClearCurrentAction)
                {
                    _currentAction = enumBasicCurrentAction.Nothing;
                }
            }
            catch (Exception ex)
            {
                _errorDescription = ex.Message;
            }
        }

        /// <summary>
        /// Método para crear parámetros
        /// </summary>
        /// <param name="name">Nombre del parámetro</param>
        /// <param name="sdbType">Tipo del parámetro</param>
        /// <param name="value">Valor del parámetro</param>
        /// <returns>Valor del parámetro</returns>
        protected SqlParameter CreateParameter(string name, SqlDbType sdbType, object value)
        {
            SqlParameter p = new SqlParameter(name, sdbType);
            p.Value = value;

            return p;
        }
        #endregion
    }
}