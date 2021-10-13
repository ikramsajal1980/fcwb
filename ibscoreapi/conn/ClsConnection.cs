using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
//using Microsoft.Extensions.Hosting.Internal;


public class ClsConnection
{
    public DataSet Ds1;
    public OracleDataAdapter dadCats;
    public OracleConnection con;
    public OracleDataReader dr;
    public DataTable dataTable;
    public string connectionString;



    private OracleCommand cmd;
    private OracleDataReader reader;






    public string conString;

    public DataTable _dt;


    public MySqlConnection Mycon;
    public MySqlDataAdapter MySqldadCats;
    public MySqlConnection MySqlcon;
    public MySqlDataReader MySqldr;
    public MySqlConnection MySqlconOledb;


    //        Dim oODBCConnection As Odbc.OdbcConnection
    //Dim sConnString As String = "Dsn=myDsn;" & _
    //                            "Uid=myUsername;" & _
    //                            "Pwd=myPassword"
    //oODBCConnection = New Odbc.OdbcConnection(sConnString)
    //oODBCConnection.Open()

    public ClsConnection()
    {

    }

    public ClsConnection(string sql, string usr, string pass, string dataBase, int Lng)
    {
        string OracleService = "pran";
        //if (dataBase.Substring(0, 3) == "172")
        //{
        //    OracleService = "exp";
        //}



        MemoryManagement.FlushMemory();
        connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=" + dataBase + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + OracleService + ")));Pooling=false;Persist Security Info=false;User ID=" + usr + ";Password=" + pass + ";Connection Lifetime=5;";
        OracleConnection con = new OracleConnection(connectionString);

    }

    public ClsConnection(string usr, string pass, string dataBase)
    {
        string OracleService = "pran";
        //if (dataBase.Substring(0, 3) == "172")
        //{
        //    OracleService = "exp";
        //}
        // MemoryManagement.FlushMemory();
        connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=" + dataBase + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + OracleService + ")));Pooling=false;Persist Security Info=false;User ID=" + usr + ";Password=" + pass + ";Connection Lifetime=5;";
        OracleConnection con = new OracleConnection(connectionString);


    }

    //public ClsConnection(string sql, string usr, string pass, string dataBase)
    //{
    //    connectionString = "Data Source=" + dataBase + ";Pooling=false;Persist Security Info=True;User ID=" + usr + ";Password=" + pass + ";Unicode=True";
    //    OracleConnection con = new OracleConnection(connectionString);


    //}

    public ClsConnection(string sql, string usr, string pass, string dataBase)
    {
        string OracleService = "pran";
        //if (dataBase.Substring(0, 3) == "172")
        //{
        //    OracleService = "exp";
        //}

        // MemoryManagement.FlushMemory();
        connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=" + dataBase + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + OracleService + ")));Pooling=false;Persist Security Info=false;User ID=" + usr + ";Password=" + pass + ";Connection Lifetime=5;";
        OracleConnection con = new OracleConnection(connectionString);


    }

    public ClsConnection(string[] sql, string usr, string pass, string dataBase)
    {
        string OracleService = "pran";
        //if (dataBase.Substring(0, 3) == "172")
        //{
        //    OracleService = "exp";
        //}

        // MemoryManagement.FlushMemory();
        connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=" + dataBase + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + OracleService + ")));Pooling=false;Persist Security Info=false;User ID=" + usr + ";Password=" + pass + ";Connection Lifetime=5;";
        OracleConnection con = new OracleConnection(connectionString);


    }
    public ClsConnection(string usr, string pass, string dataBase, int olebd)
    {

        string OracleService = "pran";
        //if (dataBase.Substring(0, 3) == "172")
        //{
        //    OracleService = "exp";
        //}
        //MemoryManagement.FlushMemory();
        connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=" + dataBase + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + OracleService + ")));Pooling=false;Persist Security Info=false;User ID=" + usr + ";Password=" + pass + ";Connection Lifetime=5;";
        //  OleDbConnection conOledb = new OleDbConnection(connectionString);


    }

    public ClsConnection(string sql, string usr, string pass, string dataBase, int mySql, string mySqlServer)
    {


        //connectionString = "server=" + mySqlServer + "; user id=" + usr + "; password=" + pass + "; database=" + dataBase + "; pooling=true;";
        //MySqlConnection conOledb = new MySqlConnection(connectionString);
        if (dataBase == "mydb_uae")
        {
            connectionString = "Server=db-mysql-sgp1-ae-do-user-3244464-0.b.db.ondigitalocean.com; Port=25060; Database=prguae_fms_uae; Uid=mydb_uae_ibs; Pwd=W3lc0m3@mdB;";
            
        }
        else if (dataBase == "ksaprgfm_db")
        {
        
            connectionString = "Server=db-mysql-sgp1-ae-do-user-3244464-0.b.db.ondigitalocean.com; Port=25060; Database=mydb_ksa; Uid=mydb_ksa_ibs; Pwd=W3lc0m3@mdB;";
        }
        else if (dataBase == "omsihirf_oms")
        {

            connectionString = "Server=db-mysql-sgp1-ae-do-user-3244464-0.b.db.ondigitalocean.com; Port=25060; Database=omsihirf_oms; Uid=mydb_oms_ibs; Pwd=W3lc0m3@mdB;";
        }
        else if (dataBase == "omsihirf_omm")
        {
            connectionString = "Server=db-mysql-sgp1-ae-do-user-3244464-0.b.db.ondigitalocean.com; Port=25060; Database=omsihirf_omm; Uid=mydb_omm_ibs; Pwd=W3lc0m3@mdB;";
        }
        else if (dataBase == "mydb_kwt")
        {

            connectionString = "Server=db-mysql-sgp1-ae-do-user-3244464-0.b.db.ondigitalocean.com; Port=25060; Database=mydb_kwt; Uid=mydb_kwt_web; Pwd=W3lc0m3@mdB;";
        }
        else if (dataBase == "myprg_uae_kaizala")
        {

            connectionString = "Server=db-mysql-opdata-do-user-3909331-0.a.db.ondigitalocean.com; Port=25060; Database=myprg_uae_kaizala; Uid=uae_kaizala; Pwd=J@!?tp^4UN;";
        }


        else if (dataBase == "mysihirf_mss")
        {
            connectionString = "Server=db-mysql-sgp1-my-do-user-3244464-0.db.ondigitalocean.com; Port=25060; Database=mysihirf_mss; Uid=mysihirf_ibs; Pwd=PRGmis@321;Pooling=false;";
            //connectionString = "Server=db-mysql-opdata-do-user-3909331-0.a.db.ondigitalocean.com; Port=25060; Database=myprg_uae_kaizala; Uid=uae_kaizala; Pwd=J@!?tp^4UN;";
        }
        else
        {
            connectionString = "server=" + mySqlServer + "; user id=" + usr + "; password=" + pass + "; database=" + dataBase + "; pooling=true;";
            MySqlConnection conOledb = new MySqlConnection(connectionString);
        }




        ///  connectionString = "Server=db-mysql-opdata-do-user-3909331-0.a.db.ondigitalocean.com; Port=25060; Database=" + dataBase + "; Uid=" + usr + "; Pwd=" + pass + ";";

    }




    public DataSet GetDataset(string sql)
    {
        try
        {
            con = new OracleConnection(connectionString);
            OracleDataAdapter dadCats = new OracleDataAdapter(sql, con);
            Ds1 = new DataSet();
            dadCats.Fill(Ds1);
            return Ds1;
        }
        catch
        {
            throw;
        }
    }


    public DataSet GetDatasetMySql(string sql)
    {
        try
        {
            MySqlcon = new MySqlConnection(connectionString);
            MySqlDataAdapter dadCats = new MySqlDataAdapter(sql, MySqlcon);
            Ds1 = new DataSet();
            dadCats.Fill(Ds1);
            return Ds1;
        }
        catch
        {
            throw;
        }



    }

  



//public DataSet GetDatasetOlebd(string sql)
//{

//    try
//    {
//        conOledb = new OleDbConnection(connectionString);
//        Ds1 = new DataSet();
//        OleDbCommand cmdPerson = new OleDbCommand
//        (sql, conOledb);
//        OleDbDataAdapter daPerson = new OleDbDataAdapter(cmdPerson);
//        conOledb.Open();
//        daPerson.Fill(Ds1);
//        conOledb.Close();
//        return Ds1;
//    }
//    catch
//    {
//        throw;
//    }

//}


public OracleDataAdapter GetDataAdpt(string sql)
    {
        con = new OracleConnection(connectionString);
        dadCats = new OracleDataAdapter(sql, con);
        return dadCats;

    }


    public byte[] GetCompressByte(string sql)
    {
        try
        {
            con = new OracleConnection(connectionString);
            OracleDataAdapter dadCats = new OracleDataAdapter(sql, con);
            Ds1 = new DataSet();
            dadCats.Fill(Ds1);
            byte[] data = null;
            MemoryStream memStream = new MemoryStream();
            GZipStream zipStream = new GZipStream(memStream, CompressionMode.Compress);
            Ds1.WriteXml(zipStream, XmlWriteMode.WriteSchema);
            zipStream.Close();
            data = memStream.ToArray();
            memStream.Close();
            return data;
        }
        catch
        {
            return null;
        }
    }

    //public byte[] GetCompressByteOledb(string sql)
    //{
    //    try
    //    {
    //        conOledb = new OleDbConnection(connectionString);
    //        Ds1 = new DataSet();
    //        OleDbCommand cmdPerson = new OleDbCommand
    //        (sql, conOledb);
    //        OleDbDataAdapter daPerson = new OleDbDataAdapter(cmdPerson);
    //        conOledb.Open();
    //        daPerson.Fill(Ds1);
    //        byte[] data = null;
    //        MemoryStream memStream = new MemoryStream();
    //        GZipStream zipStream = new GZipStream(memStream, CompressionMode.Compress);
    //        Ds1.WriteXml(zipStream, XmlWriteMode.WriteSchema);
    //        zipStream.Close();
    //        data = memStream.ToArray();
    //        memStream.Close();
    //        return data;
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    public DataSet deCompressByte(byte[] byteName)
    {
        MemoryStream memStream = new MemoryStream(byteName);
        GZipStream unzipStream = new GZipStream(memStream, CompressionMode.Decompress);
        DataSet ds = new DataSet();
        ds.ReadXml(unzipStream, XmlReadMode.ReadSchema);
        unzipStream.Close();
        memStream.Close();
        return ds;
    }



    public DataSet UpdateDataset(string sql, DataSet Ds1)
    {
        con = new OracleConnection(connectionString);
        OracleDataAdapter dadCats = new OracleDataAdapter(sql, con);
        DataSet Ds11 = new DataSet();
        dadCats.Fill(Ds11);


        DataTable dt = Ds1.Tables[0];
        for (int i = 0; i < Ds1.Tables[0].Rows.Count; i++)
        {
            DataRow TbStukRow = Ds11.Tables[0].NewRow();
            for (int j = 0; j < Ds1.Tables[0].Columns.Count; j++)
            {
                if (Ds1.Tables[0].Rows[i][j].ToString() == "")
                {

                }

                else if (Ds1.Tables[0].Rows[i][j] is Single || Ds1.Tables[0].Rows[i][j] is Double || Ds1.Tables[0].Rows[i][j] is Decimal)
                {
                    TbStukRow[j] = decimal.Parse(Ds1.Tables[0].Rows[i][j].ToString());
                }
                else if (Ds1.Tables[0].Rows[i][j] is DateTime)
                {

                    TbStukRow[j] = DateTime.Parse(Ds1.Tables[0].Rows[i][j].ToString());

                }
                else if (Ds1.Tables[0].Rows[i][j] is string)
                {
                    TbStukRow[j] = Ds1.Tables[0].Rows[i][j].ToString();
                }
            }
            Ds11.Tables[0].Rows.Add(TbStukRow);
        }

        OracleCommandBuilder cmd = new OracleCommandBuilder(dadCats);
        dadCats.Update(Ds11);
        return Ds11;
    }

    public DataSet UpdateDataset(string sql, DataSet Ds1, string pk)
    {
        con = new OracleConnection(connectionString);
        OracleDataAdapter dadCats = new OracleDataAdapter(sql, con);
        DataSet Ds11 = new DataSet();
        dadCats.Fill(Ds11);

        Ds11.Tables[0].PrimaryKey = new DataColumn[] { Ds11.Tables[0].Columns[pk] };



        DataTable dt = Ds1.Tables[0];
        for (int i = 0; i < Ds1.Tables[0].Rows.Count; i++)
        {

            DataRow FindMyRow = Ds11.Tables[0].Rows.Find(Ds1.Tables[0].Rows[i][pk]);

            if (FindMyRow == null)
            {
                DataRow TbStukRow = Ds11.Tables[0].NewRow();
                for (int j = 0; j < Ds1.Tables[0].Columns.Count; j++)
                {
                    if (Ds1.Tables[0].Rows[i][j].ToString() == "")
                    {

                    }

                    else if (Ds1.Tables[0].Rows[i][j] is Single || Ds1.Tables[0].Rows[i][j] is Double || Ds1.Tables[0].Rows[i][j] is Decimal)
                    {
                        TbStukRow[j] = decimal.Parse(Ds1.Tables[0].Rows[i][j].ToString());
                    }
                    else if (Ds1.Tables[0].Rows[i][j] is DateTime)
                    {

                        TbStukRow[j] = DateTime.Parse(Ds1.Tables[0].Rows[i][j].ToString());

                    }
                    else if (Ds1.Tables[0].Rows[i][j] is string)
                    {
                        TbStukRow[j] = Ds1.Tables[0].Rows[i][j].ToString();
                    }
                }
                Ds11.Tables[0].Rows.Add(TbStukRow);

            }
            else if (FindMyRow != null)
            {
                string oid = FindMyRow[pk].ToString();
                int ds1Ver = int.Parse(FindMyRow["ver"].ToString());
                int ds11Ver = int.Parse(Ds1.Tables[0].Rows[i]["ver"].ToString());
                if (ds11Ver > ds1Ver)
                {

                    for (int j = 0; j < Ds1.Tables[0].Columns.Count; j++)
                    {
                        if (Ds1.Tables[0].Rows[i][j].ToString() == "")
                        {

                        }

                        else if (Ds1.Tables[0].Rows[i][j] is Single || Ds1.Tables[0].Rows[i][j] is Double || Ds1.Tables[0].Rows[i][j] is Decimal)
                        {
                            FindMyRow[j] = decimal.Parse(Ds1.Tables[0].Rows[i][j].ToString());
                        }
                        else if (Ds1.Tables[0].Rows[i][j] is DateTime)
                        {

                            FindMyRow[j] = DateTime.Parse(Ds1.Tables[0].Rows[i][j].ToString());

                        }
                        else if (Ds1.Tables[0].Rows[i][j] is string)
                        {
                            FindMyRow[j] = Ds1.Tables[0].Rows[i][j].ToString();
                        }
                    }
                }


            }




        }

        OracleCommandBuilder cmd = new OracleCommandBuilder(dadCats);
        dadCats.Update(Ds11);
        return Ds11;
    }


    public DataSet UpdateDataset(DataSet Ds11, DataSet Ds1, string pk)
    {


        Ds11.Tables[0].PrimaryKey = new DataColumn[] { Ds11.Tables[0].Columns[pk] };



        DataTable dt = Ds1.Tables[0];
        for (int i = 0; i < Ds1.Tables[0].Rows.Count; i++)
        {

            DataRow FindMyRow = Ds11.Tables[0].Rows.Find(Ds1.Tables[0].Rows[i][pk]);

            if (FindMyRow == null)
            {
                DataRow TbStukRow = Ds11.Tables[0].NewRow();
                for (int j = 0; j < Ds1.Tables[0].Columns.Count; j++)
                {
                    if (Ds1.Tables[0].Rows[i][j].ToString() == "")
                    {

                    }

                    else if (Ds1.Tables[0].Rows[i][j] is Single || Ds1.Tables[0].Rows[i][j] is Double || Ds1.Tables[0].Rows[i][j] is Decimal)
                    {
                        TbStukRow[j] = decimal.Parse(Ds1.Tables[0].Rows[i][j].ToString());
                    }
                    else if (Ds1.Tables[0].Rows[i][j] is DateTime)
                    {

                        TbStukRow[j] = DateTime.Parse(Ds1.Tables[0].Rows[i][j].ToString());

                    }
                    else if (Ds1.Tables[0].Rows[i][j] is string)
                    {
                        TbStukRow[j] = Ds1.Tables[0].Rows[i][j].ToString();
                    }
                }
                Ds11.Tables[0].Rows.Add(TbStukRow);

            }
            else if (FindMyRow != null)
            {
                string oid = FindMyRow[pk].ToString();
                int ds1Ver = int.Parse(FindMyRow["ver"].ToString());
                int ds11Ver = int.Parse(Ds1.Tables[0].Rows[i]["ver"].ToString());
                if (ds11Ver > ds1Ver)
                {

                    for (int j = 0; j < Ds1.Tables[0].Columns.Count; j++)
                    {
                        if (Ds1.Tables[0].Rows[i][j].ToString() == "")
                        {

                        }

                        else if (Ds1.Tables[0].Rows[i][j] is Single || Ds1.Tables[0].Rows[i][j] is Double || Ds1.Tables[0].Rows[i][j] is Decimal)
                        {
                            FindMyRow[j] = decimal.Parse(Ds1.Tables[0].Rows[i][j].ToString());
                        }
                        else if (Ds1.Tables[0].Rows[i][j] is DateTime)
                        {

                            FindMyRow[j] = DateTime.Parse(Ds1.Tables[0].Rows[i][j].ToString());

                        }
                        else if (Ds1.Tables[0].Rows[i][j] is string)
                        {
                            FindMyRow[j] = Ds1.Tables[0].Rows[i][j].ToString();
                        }
                    }
                }


            }




        }

        //OracleCommandBuilder cmd = new OracleCommandBuilder(dadCats);
        //dadCats.Update(Ds11);
        return Ds11;
    }


    public string ExecuteCommand(string sql)
    {
        con = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand();
        cmd.CommandText = sql;
        cmd.Connection = this.con;
        try
        {
            this.con.Open();
            cmd.ExecuteNonQuery();

            return "Y";
        }
        catch (OracleException E)
        {

            return E.Message;
        }
        finally
        {
            MemoryManagement.FlushMemory();
            this.con.Close();
        }
    }


    public string ExecuteCommandMysql(string sql)
    {
        Mycon = new MySqlConnection(connectionString);
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = this.Mycon;
        try
        {
            this.Mycon.Open();
            cmd.ExecuteNonQuery();

            return "Y";
        }
        catch (OracleException E)
        {

            return E.Message;
        }
        finally
        {
            MemoryManagement.FlushMemory();
            this.Mycon.Close();
        }
    }

    //public string DataTableToJsonWithJavaScriptSerializer(DataTable table)
    //{
    //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
    //    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
    //    Dictionary<string, object> childRow;
    //    foreach (DataRow row in table.Rows)
    //    {
    //        childRow = new Dictionary<string, object>();
    //        foreach (DataColumn col in table.Columns)
    //        {
    //            childRow.Add(col.ColumnName, row[col]);
    //        }
    //        parentRow.Add(childRow);
    //    }
    //    return jsSerializer.Serialize(parentRow);
    //}

    public string DataTableToJSONWithStringBuilder(DataTable table)
    {
        //var JSONString = new StringBuilder();
        //if (table.Rows.Count > 0)
        //{
        //    JSONString.Append("[");
        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        JSONString.Append("{");
        //        for (int j = 0; j < table.Columns.Count; j++)
        //        {
        //            if (j < table.Columns.Count - 1)
        //            {
        //                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
        //            }
        //            else if (j == table.Columns.Count - 1)
        //            {
        //                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
        //            }
        //        }
        //        if (i == table.Rows.Count - 1)
        //        {
        //            JSONString.Append("}");
        //        }
        //        else
        //        {
        //            JSONString.Append("},");
        //        }
        //    }
        //    JSONString.Append("]");
        //}
        //return JSONString.ToString();

        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;



    }

    public string ExecuteCommandorc(string[] sql)
    {
        using (OracleConnection con = new OracleConnection(connectionString))
        {
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            OracleTransaction tx = con.BeginTransaction();
            cmd.Transaction = tx;
            try
            {

                for (int i = 0; i < sql.Count(); i++)
                {
                    cmd.CommandText = sql[i];
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                return "Y";
            }
            catch (OracleException E)
            {
                tx.Rollback();
                MemoryManagement.FlushMemory();
                return E.Message;
            }
        }
    }



    public DataTable GetDataTableOracle(string strSql)
    {
        try
        {
            using (con = new OracleConnection(connectionString))
            {
                con.Open();


                using (cmd = new OracleCommand(strSql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    //foreach (KeyValuePair<string, object> dc in ParamListOracle)
                    //{
                    //    cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                    //}

                    using (reader = cmd.ExecuteReader())
                    {
                        _dt = new DataTable("dt");
                        try
                        {
                            _dt.Load(reader);
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

    }

    //public string ConvertOracleDataTabletoJSON(string sQl)
    //{
    //    DataTable dt = GetDataTableOracle(sQl);

    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    Dictionary<string, object> row;
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        row = new Dictionary<string, object>();
    //        foreach (DataColumn col in dt.Columns)
    //        {
    //            row.Add(col.ColumnName, dr[col]);
    //        }
    //        rows.Add(row);
    //    }

    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
    //    return serializer.Serialize(rows);
    //}




    public string ExecuteSqlTran(ArrayList SQLStringList)
    {

        using (OracleConnection con = new OracleConnection(connectionString))
        {
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            OracleTransaction tx = con.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();



                return "Y";
            }
            catch (OracleException E)
            {
                tx.Rollback();
                MemoryManagement.FlushMemory();
                return E.Message;
            }
        }

    }






    public void moveFile()
    {
        try
        {
            string txtFolderName = "C:\\DataFile";
            DirectoryInfo l_dDirInfo = new DirectoryInfo(txtFolderName);
            if (l_dDirInfo.Exists == false)
                Directory.CreateDirectory(txtFolderName);

            string txtFolderNameCurrent = "C:\\DataFileCurrent";
            DirectoryInfo l_dDirInfo1 = new DirectoryInfo(txtFolderNameCurrent);
            if (l_dDirInfo1.Exists == false)
                Directory.CreateDirectory(txtFolderNameCurrent);


            List<String> MyMusicFiles = Directory.GetFiles(txtFolderNameCurrent, "*.sql", SearchOption.AllDirectories).ToList();
            foreach (string file in MyMusicFiles)
            {
                string findFileName = DateTime.Now.ToString("yyyyMMddHH", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                FileInfo mFile = new FileInfo(file);
                if (mFile.Name.Substring(0, 10) != findFileName)
                {
                    if (new FileInfo(l_dDirInfo + "\\" + mFile.Name).Exists == false)//to remove name collusion
                        mFile.MoveTo(l_dDirInfo + "\\" + mFile.Name);
                }
            }
        }
        catch { }
    }

    public string ExecuteBinaryDatacls(byte[] blob, string sql, string usr, string pass, string dbase)
    {
        try
        {
            if (sql != "")
            {
                con = new OracleConnection(connectionString);
                //insert the byte as oracle parameter of type blob
                OracleParameter blobParameter = new OracleParameter();
                OracleCommand oCmd = new OracleCommand();
                blobParameter.OracleDbType = OracleDbType.Blob;
                blobParameter.ParameterName = "bParameter";
                blobParameter.Value = blob;

                // con = new OracleConnection(connectionString);
                oCmd = new OracleCommand();

                oCmd.CommandText = sql;
                oCmd.Connection = con;
                oCmd.Parameters.Add(blobParameter);
                try
                {

                    con.Open();
                    oCmd.ExecuteNonQuery();
                    return "Y";
                }
                catch (OracleException Ex)
                {
                    return Ex.Message;
                }
                finally
                {
                    oCmd.Dispose();
                    con.Close();
                    con.Dispose();
                }

            }
            return null;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

}

