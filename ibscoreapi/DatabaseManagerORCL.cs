using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;


namespace IbsCoreapi
{
    public class DatabaseManagerORCL : IDisposable
    {
        private String conString;
        private OracleConnection _cn;
        private DataTable _dt;
        private OracleCommand _cmd;
        private OracleDataReader _reader;
        private Dictionary<string, object> ParamList;

        private static OracleConnection _con;
        private static string connectionString;

        public DatabaseManagerORCL()
        {
            connectionString = "";
            conString = connectionString;
            ParamList = new Dictionary<string, object>();
        }
        public static OracleConnection OpenCon
        {
            get
            {
                if (_con == null)
                {
                    _con = new OracleConnection(connectionString);
                    _con.Open();
                    return _con;
                }
                else if (_con.State != ConnectionState.Open)
                {
                    _con.Open();
                    return _con;
                }
                else
                {
                    return _con;
                }
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cn != null)
                {
                    _cn.Dispose();
                    _cn = null;
                }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if (_cmd != null)
                {
                    _cmd.Dispose();
                    _cmd = null;
                }
                if (_reader != null)
                {
                    _reader.Dispose();
                    _reader = null;
                }
            }
        }

        ~DatabaseManagerORCL()
        {
            Dispose(false);
        }

        private static readonly Lazy<DatabaseManagerORCL> VLazy = new Lazy<DatabaseManagerORCL>(() => new DatabaseManagerORCL());

        public static DatabaseManagerORCL Instance
        {
            get { return VLazy.Value; }
        }

        public int ClearParameters()
        {
            if (ParamList != null)
            {
                if (ParamList.Count > 0)
                    ParamList.Clear();
            }
            if (_cmd != null)
            {
                if (_cmd.Parameters.Count > 0)
                    _cmd.Parameters.Clear();
            }
            return 1;
        }

        public void AddParameteres(string param, object value)
        {
            ParamList.Add(param, value);
        }

        //public int ExecuteNonQuery(string strSql)
        //{
        //    try
        //    {
        //        int intResult;
        //        _cmd = new OracleCommand(strSql, OpenCon);

        //        _cmd.CommandType = CommandType.Text;

        //        intResult = _cmd.ExecuteNonQuery();

        //        return intResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (new Exception(ex.Message));

        //    }
        //    finally
        //    {

        //    }
        //}
        public int ExecuteNonQuery(string strSql)
        {
            try
            {
                int intResult;
                using (_cn = new OracleConnection(conString))
                {
                    _cn.Open();
                    using (_cmd = new OracleCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;
                        //foreach (KeyValuePair<string, object> dc in ParamList)
                        //{
                        //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        //}

                        intResult = _cmd.ExecuteNonQuery();
                    }
                }
                return intResult;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));

            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }
        public int ExecuteNonQuery(Common peram)
        {
            try
            {
                conString = @"DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + peram.Host + ")(PORT=" + peram.Port + "))(CONNECT_DATA=(SERVICE_NAME=" + peram.ServiceName + ")));PASSWORD=" + peram.Password + ";USER ID=" + peram.User + ";pooling=False;";
                int intResult;
                using (_cn = new OracleConnection(conString))
                {
                    _cn.Open();
                    using (_cmd = new OracleCommand(peram.Query, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;

                        intResult = _cmd.ExecuteNonQuery();
                    }
                }
                return intResult;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));

            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }


      


        public DataTable ExecuteQuery(string strSql)
        {
            try
            {
                using (_cn = new OracleConnection(conString))
                {
                    _cn.Open();


                    using (_cmd = new OracleCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;
                        //foreach (KeyValuePair<string, object> dc in ParamList)
                        //{
                        //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        //}

                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable();
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch { }
                        }
                    }
                }

                return _dt;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }
        public DataTable ExecuteQuery(Common peram)
        {
            try
            {
                conString = @"DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + peram.Host + ")(PORT=" + peram.Port + "))(CONNECT_DATA=(SERVICE_NAME=" + peram.ServiceName + ")));PASSWORD=" + peram.Password + ";USER ID=" + peram.User + ";pooling=False;";
                using (_cn = new OracleConnection(conString))
                {
                    _cn.Open();


                    using (_cmd = new OracleCommand(peram.Query, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;
                        //foreach (KeyValuePair<string, object> dc in ParamList)
                        //{
                        //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        //}

                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable("NewDatatable");
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch  { }
                        }
                    }
                }

                return _dt;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }

        public DataTable ExecuteQuery(commp peram)
        {
            try
            {
                conString = @"DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + peram.Host + ")(PORT=" + peram.Port + "))(CONNECT_DATA=(SERVICE_NAME=" + peram.ServiceName + ")));PASSWORD=" + peram.Password + ";USER ID=" + peram.User + ";pooling=False;";
                using (_cn = new OracleConnection(conString))
                {
                    _cn.Open();


                    using (_cmd = new OracleCommand(peram.Query, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;
                        //foreach (KeyValuePair<string, object> dc in ParamList)
                        //{
                        //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        //}

                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable();
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch { }
                        }
                    }
                }

                return _dt;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }


        

        internal OracleConnection GetDbConnection()
        {
            OracleConnection myConnection = new OracleConnection(connectionString);
            return myConnection;
        }

        public static DataTable StaticDT(string strSql)
        {
            try
            {
                DataTable dt = new DataTable();
                using (_con = new OracleConnection(connectionString))
                {
                    _con.Open();


                    using (OracleCommand cmds = new OracleCommand(strSql, _con))
                    {
                        cmds.CommandType = CommandType.Text;
                        //foreach (KeyValuePair<string, object> dc in ParamList)
                        //{
                        //    _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        //}
                        using (OracleDataReader reader = cmds.ExecuteReader())
                        {
                            dt = new DataTable();
                            try
                            {
                                dt.Load(reader);
                            }
                            catch { }
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                _con.Dispose();
                _con.Close();
                OracleConnection.ClearPool(_con);
            }
        }
    }
}