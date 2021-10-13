using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace IbsCoreapi
{
    public class DBManager
    {
        DatabaseManagerORCL dm = new DatabaseManagerORCL();

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }


        public string ExecuteQuery(Common comParam)
        {

            try
            {
                // ClsConnection cl = new global::ClsConnection(comParam.User, comParam.Password, comParam.Host);
                //  DataTable dt = cl.GetDataTableOracle(comParam.Query);


                DataTable dt = dm.ExecuteQuery(comParam);

                //xml
                string rv = ConvertDatatableToXML(dt);

                //Json format
                //   string rv = cl.DataTableToJSONWithStringBuilder(dt);
                //   rv = rv.Replace(@"\""", @"""");



                return rv;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }





        }

        public string ExecuteQueryRetJson(Common comParam)
        {

            try
            {

                ClsConnection cl = new global::ClsConnection(comParam.Query, comParam.User, comParam.Password, comParam.Host);
                DataSet ds = new DataSet();
                ds = cl.GetDataset(comParam.Query);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return JSONString;


                //System.Web.Script.Serialization.JavaScriptSerializer serializer1 = new System.Web.Script.Serialization.JavaScriptSerializer();
                //Dictionary<string, object> row1;
                //foreach (DataRow dr in dt1.Rows) //use the old variable rows only
                //{
                //    row1 = new Dictionary<string, object>();
                //    foreach (DataColumn col in dt1.Columns)
                //    {
                //        row1.Add(col.ColumnName, dr[col]);
                //    }
                //    rows.Add(row1); // Finally You can add into old json array in this way
                //}
                //return serializer.Serialize(rows);




            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public string ExecuteQueryRetJsonMysql(Common comParam)
        {

            try
            {

                ClsConnection cl = new global::ClsConnection(comParam.Query, comParam.User, comParam.Password, comParam.Host, 1, comParam.ServiceName);
                DataSet ds = new DataSet();
                ds = cl.GetDatasetMySql(comParam.Query);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return JSONString;


            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public string writexmlmySql(commp comParam)
        {

            try
            {

                ClsConnection cl = new global::ClsConnection(comParam.Query, comParam.User, comParam.Password, comParam.Host, 1, comParam.ServiceName);
                DataSet carsInventoryDS = cl.GetDatasetMySql(comParam.Query);

                string filename = "fcwberp/DataFile/" + comParam.FileName + ".bin";
                //write XML
                //  WriteXmlToFile(carsInventoryDS, comParam.FileName) ;


                using (var fileStream = File.Create(filename))
                {
                    using (var zipStream = new GZipStream(fileStream, CompressionMode.Compress))
                    {
                        carsInventoryDS.WriteXml(zipStream, XmlWriteMode.WriteSchema);
                        zipStream.Close();
                        fileStream.Close();
                        MemoryManagement.FlushMemory();
                    }
                }



                return "Y";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }


        public string writexml(commp comParam)
        {

            try
            {
                //ClsConnection cl = new global::ClsConnection(comParam.User, comParam.Password, comParam.Host);
                //DataSet carsInventoryDS = cl.GetDataset(comParam.Query);

                DataSet carsInventoryDS = new DataSet();
                DataTable dt = dm.ExecuteQuery(comParam);
                carsInventoryDS.Tables.Add(dt);


                DeleteAllFromFolder();

                string filename = "fcwberp/DataFile/" + comParam.FileName + ".bin";
                //write XML
                //  WriteXmlToFile(carsInventoryDS, comParam.FileName) ;


                using (var fileStream = File.Create(filename))
                {
                    using (var zipStream = new GZipStream(fileStream, CompressionMode.Compress))
                    {
                        carsInventoryDS.WriteXml(zipStream, XmlWriteMode.WriteSchema);
                        zipStream.Close();
                        fileStream.Close();
                        MemoryManagement.FlushMemory();
                    }
                }



                return "Y";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        protected void DeleteAllFromFolder()
        {
            try
            {
                string fullPath = "fcwberp/DataFile/";
                string txtFolderNameCurrent = @fullPath;
                DirectoryInfo d = new DirectoryInfo(txtFolderNameCurrent);
                foreach (FileInfo f in d.GetFiles())
                {
                    if (f.LastWriteTime.AddMinutes(5) < DateTime.Now)
                        File.Delete(f.FullName);
                }
            }
            catch { }
        }

        public string ConvertDatatableToXML(DataTable dt)
        {
            //MemoryStream str = new MemoryStream();
            //dt.WriteXml(str, true);
            ////str.Seek(0, SeekOrigin.Begin);
            //StreamReader sr = new StreamReader(str);
            //string xmlstr;
            //xmlstr = sr.ReadToEnd();
            //return (xmlstr);



            // Create a file name to write to.


            // Create the FileStream to write with.
            //MemoryStream str = new MemoryStream();

            //// Create an XmlTextWriter with the fileStream.
            //System.Xml.XmlTextWriter xmlWriter =
            //    new System.Xml.XmlTextWriter(str,
            //    System.Text.Encoding.Unicode);
            //string xmlstr;
            //// Write to the file with the WriteXml method.
            //xmlstr = dt.WriteXml(xmlWriter);
            //xmlWriter.Close();
            //return xmlstr;


            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema);
            return writer.ToString();

        }


        private void WriteXmlToFile(DataSet thisDataSet, string fileName)
        {
            if (thisDataSet == null) { return; }

            // Create a file name to write to.
            string filename = "fcwberp/Datafile/" + fileName + ".xml";

            // Create the FileStream to write with.
            System.IO.FileStream stream = new System.IO.FileStream
                (filename, System.IO.FileMode.Create);

            // Create an XmlTextWriter with the fileStream.
            System.Xml.XmlTextWriter xmlWriter =
                new System.Xml.XmlTextWriter(stream,
                System.Text.Encoding.Unicode);

            // Write to the file with the WriteXml method.
            thisDataSet.WriteXml(xmlWriter);
            xmlWriter.Close();
        }

        public string ExecuteNonQuery(Common peram)
        {
            string res = "Failed";
            try
            {

                dm.ExecuteNonQuery(peram);
                res = "Y";

            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        public string ExecuteNonQueryMySql(Common comParam)
        {
           
            try
            {

                ClsConnection cl = new global::ClsConnection(comParam.Query, comParam.User, comParam.Password, comParam.Host, 1, comParam.ServiceName);
                string Resulttring = cl.ExecuteCommandMysql(comParam.Query);                
                return Resulttring;


            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public string ExecPRM(Mailp peram)
        {

            string email = peram.email;
            string passWord = peram.passWord;
            string usr = peram.usr;
            string siTe = peram.siTe;


            string sType = "";
            string company = "";
            string appserver = "";
            ClsConnection CL = new ClsConnection("sql", "PPL1", "ppl", siTe);
            string[] words = Regex.Split(passWord, ";");

            try
            {
                sType = words[0].ToString();
                company = words[1].ToString();
                appserver = words[2].ToString();
            }
            catch { }


            if (sType == "1")
            {
                try
                {
                    string msg = "";

                    DataSet DS = CL.GetDataset("SELECT COUNT(*) from FC1.FC_TRAN_SHEET_MASTER M where M.FTSM_CHKS='0' and M.FTSM_TSNO='" + email + "'");
                    if (DS.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        msg = "Already Verified";
                    }
                    else
                    {

                        string sql = @"UPDATE FC1.FC_TRAN_SHEET_MASTER M SET M.FTSM_CHKS='0',M.FTSM_CHKB='" + usr + "'  where M.FTSM_TSNO='" + email + "' and M.FTSM_CHKS='1'";
                        msg = CL.ExecuteCommand(sql);

                        try
                        {
                            Semail(email, company, "2", usr, siTe, appserver);
                        }
                        catch { }
                    }
                    return msg;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            else if (sType == "2")
            {
                try
                {
                    string msg = "";

                    DataSet DS = CL.GetDataset("SELECT COUNT(*) from FC1.FC_TRAN_SHEET_MASTER M where M.FTSM_RECS='0' and M.FTSM_TSNO='" + email + "'");
                    if (DS.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        msg = "Already Verified";
                    }
                    else
                    {

                        string sql = @"UPDATE FC1.FC_TRAN_SHEET_MASTER M SET M.FTSM_RECS='0',M.FTSM_RECB='" + usr + "' where M.FTSM_TSNO='" + email + "' and M.FTSM_RECS='1' ";
                        msg = CL.ExecuteCommand(sql);

                        try
                        {
                            Semail(email, company, "3", usr, siTe, appserver);
                        }
                        catch { }

                    }
                    return msg;

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            else if (sType == "3")
            {
                try
                {
                    ArrayList myString = new ArrayList();

                    string msg = "";

                    DataSet DS = CL.GetDataset("SELECT COUNT(*) from FC1.FC_TRAN_SHEET_MASTER M where M.FTSM_POSF='Y' and M.FTSM_TSNO='" + email + "'");
                    if (DS.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        msg = "Already Verified";
                    }
                    else
                    {

                        myString.Add(@"UPDATE FC1.FC_TRAN_SHEET_MASTER M SET M.FTSM_POSF='Y',M.FTSM_PSNA='" + usr + "',M.FTSM_READYFORJV='Y'  where M.FTSM_TSNO='" + email + "'  AND M.FTSM_PTYP='1' AND M.FTSM_PTBY='1'");
                        myString.Add(@"UPDATE FC1.FC_TRAN_SHEET_MASTER M SET M.FTSM_POSF='Y',M.FTSM_PSNA='" + usr + "' where M.FTSM_TSNO='" + email + "'  AND M.FTSM_PTYP='1' AND (M.FTSM_PTBY='2' or M.FTSM_PTBY='3')");
                        myString.Add(@"UPDATE FC1.FC_TRAN_SHEET_MASTER M SET M.FTSM_POSF='Y',M.FTSM_PSNA='" + usr + "',M.FTSM_READYFORJV='Y' where M.FTSM_TSNO='" + email + "' AND M.FTSM_PTYP='1' AND( M.FTSM_PTBY='4') ");
                        myString.Add(@"UPDATE FC1.FC_TRAN_SHEET_MASTER M SET M.FTSM_POSF='Y',M.FTSM_PSNA='" + usr + "',M.FTSM_READYFORJV='Y' where M.FTSM_TSNO='" + email + "' AND( M.FTSM_PTYP='2' or M.FTSM_PTYP='3' or M.FTSM_PTYP='4') ");


                        msg = CL.ExecuteSqlTran(myString);
                        if (msg == "Y")
                        {
                            try
                            {
                                Semail(email, company, "4", usr, siTe, appserver);
                            }
                            catch { }

                            return "Y";
                        }

                    }
                    return msg;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            else
            {



                string sql = @"UPDATE FC1.T_ACCOUNTS_USERS SET USER_PASS='" + passWord.Replace("\"", "") + "' WHERE USER_TEXT = '" + usr.Replace("\"", "") + "' AND EMAIL_ADD='" + email.Replace("\"", "") + "'";


                DataSet DS = CL.GetDataset("SELECT COUNT(*) from FC1.T_ACCOUNTS_USERS_V where USER_PASS='" + passWord.Replace("\"", "") + "' and USER_TEXT = '" + usr.Replace("\"", "") + "'");
                if (DS.Tables[0].Rows[0][0].ToString() == "1")
                {
                    return "Already Verified";
                }
                else
                {
                    try
                    {
                        ArrayList myString = new ArrayList();
                        myString.Add("INSERT INTO FC1.T_ACCOUNTS_USERS_V (USER_TEXT,USER_PASS)VALUES('" + usr.Replace("\"", "") + "','" + passWord.Replace("\"", "") + "')");
                        myString.Add("ALTER USER " + usr.Replace("\"", "") + " IDENTIFIED BY \"" + passWord.Replace("\"", "") + "\" password expire");
                        myString.Add("ALTER USER " + usr.Replace("\"", "") + " ACCOUNT UNLOCK");
                        CL.ExecuteSqlTran(myString);
                        CL.ExecuteCommand(sql);
                        return "Your temporary password is " + passWord.Replace("\"", "") + "";
                    }
                    catch
                    {
                        return "Unknown error,Please try again";
                    }
                }
            }
        }

        public string Semail(string TSnumber, string cmp, string TYPE, string usr, string database, string appServer)
        {
            try
            {

                DataSet ds = new DataSet();
                ClsConnection CL = new ClsConnection("sql", "PPL1", "ppl", database);
                string dg = "";
                DataSet ds1 = CL.GetDataset("Select distinct TEMAIL_ADD,TUSER_TEXT from fc1.T_EMAIL_USERS where FUSER_TEXT='" + usr + "' and COMPANY_ID='" + cmp + "' AND TYPE='" + TYPE + "'");

                string sQl = @"SELECT CMP.ACMP_NAME, D.FTSD_FCAC,AH.ACCH_NAME, D.FTSD_DESC, D.FTSD_DEBT, D.FTSD_CRDT,DT.TYPE_NAME
FROM FC1.FC_TRAN_SHEET_DETAIL D, FC1.FC_TRAN_SHEET_MASTER M, FC1.T_ACCOUNTS_COMPANY_HEAD AH, PPL1.DISTRIBUTOR_TYPE DT, FC1.T_ACCOUNTS_COMPANY CMP
WHERE M.FTSM_TSNO = D.FTSD_FTSM AND M.FTSM_TSNO = '" + TSnumber + @"' AND D.FTSD_FCAC = AH.ACCH_TEXT AND CMP.ACMP_TEXT = M.FTSM_ACMP
AND D.FTSD_COCN = DT.OID(+)
ORDER BY D.FTSD_SEQN";

                // sQl = @"SELECT FT.FTSM_TSNO FROM FC1.FC_TRAN_SHEET_MASTER FT WHERE FT.FTSM_POSF='N' and FTSM_ACMP='" + cmbCompany.ActiveRow.Cells[1].Value.ToString() + "'";
                ds = CL.GetDataset(sQl);
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {


                    string textBody = " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 1000 + "><tr bgcolor='#4da6ff'><td><b>CNAME</b></td> <td><b>Name</b></td> <td> <b> Description </b> </td> <td> <b> Debit </b></td> <td> <b> Credit </b> </td> </tr>";
                    for (int loopCount = 0; loopCount < ds.Tables[0].Rows.Count; loopCount++)
                    {
                        textBody += "<tr><td>" + ds.Tables[0].Rows[loopCount]["ACMP_NAME"] + "</td> <td>" + ds.Tables[0].Rows[loopCount]["ACCH_NAME"] + "</td> <td> " + ds.Tables[0].Rows[loopCount]["FTSD_DESC"] + "</td>  <td> " + ds.Tables[0].Rows[loopCount]["FTSD_DEBT"] + "</td> <td> " + ds.Tables[0].Rows[loopCount]["FTSD_CRDT"] + "</td> </tr>";
                    }
                    textBody += "</table>";
                    if (TYPE == "4")
                    {
                        dg = "Please create JV :";

                    }
                    else
                    {
                        string type_cmp_ip = TYPE + ";" + cmp + ";" + appServer;
                        usr = ds1.Tables[0].Rows[i]["TUSER_TEXT"].ToString();
                        dg = "http://" + appServer + "/fcwberp/Service.asmx/ExecPRM?email=" + TSnumber + "&passWord=" + type_cmp_ip + "&usr=" + usr + "&siTe=" + database + "";
                    }



                    String body3 = @"
<div>
<p>Dear Sir/ Madam,</p>
</div>
<div>
<p>You are kindly requested to check bellow transaction(" + TSnumber + @")and arrange to approve.</p> 

" + textBody + @"
<p>   </p>   

  <p><a href='" + dg + @"'><b>Approve<b> </a></p>
<p>   </p>   
<p>   </p>   
<p>   </p>  
<p>  <br/> </p>  
<p>  <br/> </p>  
<div>
<p>Thanks & Regards</p>
</div>
<div style=color:#0000FF>
<h3>    </h3>
</div>
<div>
  <br/>
  <br/>
 <br/>
 
</div>
<h3> This is an auto generated email </h3>
 </div>";


                    Sendmail(body3, ds1.Tables[0].Rows[0][0].ToString(), "");
                }
                return "Y";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }

        public string Sendmail(string emailBody, string toEmail, string bccmail)
        {

            try

            {





                string FromAD = "IBS<Autoemail1@prgoverseas.com>";
                SmtpClient server = new SmtpClient("smtp.office365.com");
                server.UseDefaultCredentials = false;
                server.Port = 587;
                server.EnableSsl = true;
                server.Credentials = new System.Net.NetworkCredential("Autoemail1@prgoverseas.com", "pranbd1@@@");
                server.Timeout = 25000;
                MailAddress fromAddress = new MailAddress(FromAD);
                // server.UseDefaultCredentials = true;
                MailMessage mail = new MailMessage();
                // mail.From = new MailAddress("Autoemail1@prgoverseas.com");
                mail.From = fromAddress;
                mail.To.Add(toEmail);
                if (bccmail.Length > 5)
                {
                    mail.Bcc.Add(bccmail);
                }
                mail.Subject = "Pending for Approval";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                server.Send(mail);
                return "Y";
            }
            catch
            {
                try
                {
                    string FromAD = "IBS<bi@prangroup.com>";
                    SmtpClient server = new SmtpClient("smtp.office365.com");
                    server.UseDefaultCredentials = false;
                    server.Port = 587;
                    server.EnableSsl = true;
                    server.Credentials = new System.Net.NetworkCredential("bi@prangroup.com", "prg@1234560");
                    server.Timeout = 25000;
                    MailAddress fromAddress = new MailAddress(FromAD);
                    // server.UseDefaultCredentials = true;
                    MailMessage mail = new MailMessage();
                    // mail.From = new MailAddress("Autoemail1@prgoverseas.com");
                    mail.From = fromAddress;
                    mail.To.Add(toEmail);
                    if (bccmail.Length > 5)
                    {
                        mail.Bcc.Add(bccmail);
                    }
                    mail.Subject = "Pending for Approval";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    server.Send(mail);
                    return "Y";
                }
                catch (Exception ex)
                { return ex.Message; }
            }
        }





    }
}
