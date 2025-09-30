using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonaWF;
using Oracle.ManagedDataAccess.Client;

namespace ATSTemplate
{
    internal class DBOp
    {
        string DBNameVN;
        public DBOp()
        {
            // DBName = "Data Source=AMTDB;User Id=MYDAS_DATABASE;Password=cpe2rd;";
            //DBName = "Data Source=AMTDBS;User Id=MYDAS_DBS;Password=cpe2rd;";
            DBNameVN = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =10.90.3.244)(PORT =1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME =XEPDB1)));Password=mydas;User ID=MYDAS";
        }

        public void track_log(string fName, string fLogtxt)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fName + ".txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ": " + fLogtxt);
                }
            }
            catch (Exception)
            { }
        }

        public string RemoveChar(string StrIn, string inchar)
        {
            string StrOut = StrIn;
            try
            {
                while (StrOut.Contains(inchar))
                {
                    StrOut = StrOut.Replace(inchar, ",");
                }

                return StrOut;
            }
            catch (Exception)
            {
                return StrOut;
            }
        }

        public string InsertDBVn(string Product, string Station, string PN, string TitleVer, string[] MainInfo, string DivDataDetail, string DivDataError)
        {
            //2017.05.03  Modify to fix duplicate data

            string newDivDataDetail = "";

            OracleConnection conVN = new OracleConnection(DBNameVN);
            OracleCommand cmdVN = new OracleCommand();

            try
            {
                DivDataError = DivDataError.Replace("'", "");
                DivDataDetail = DivDataDetail.Replace("'", "");
                DivDataDetail = RemoveChar(DivDataDetail, "\t");
                DivDataDetail = RemoveChar(DivDataDetail, "  "); // 2 space to not split FT RC
                //DivDataDetail = RemoveChar(DivDataDetail, ";");
                //DivDataDetail = RemoveChar(DivDataDetail, ",,");

                try
                {

                    string[] countItem = DivDataDetail.Split(',');
                    foreach (string iS in countItem)
                    {
                        if (Station.Contains("PT") || Station.Contains("NFT"))
                        {
                            string[] newStr = iS.Split(' ');
                            if (newStr.Length > 0)
                            {
                                foreach (string iStr in newStr)
                                {
                                    if (iStr != "")
                                    {
                                        newDivDataDetail += iStr.Trim() + ",";
                                    }
                                }
                            }
                            else
                            {
                                newDivDataDetail += iS.Trim() + ",";
                            }
                        }
                        else
                        {
                            newDivDataDetail += iS.Trim() + ",";
                        }
                    }

                    if (newDivDataDetail.StartsWith(","))
                    {
                        newDivDataDetail = newDivDataDetail.Remove(0, 1);
                    }
                    DivDataDetail = newDivDataDetail;
                }
                catch (Exception r)
                {
                    //track_log("RemoveChar",r.ToString());
                }

                string tmpCycleTime = "";
                string finalCycleTime = "";
                DateTime t, v;
                double tSecond = 0;
                //WN2500RP-100NASV2	124	3.0	@ZO10FSB	1	F:\lsy\ID\U12H294\RC\PASS\20160418.txt	RV1.0.0.1	FOX-B05L2RC1	070110 <- cycle time co van de
                // support convert: 00:02:11.906 / 12(s) / 01:00 / 00:01:30 /7.188 
                try
                {
                    finalCycleTime = MainInfo[5];
                    //if (MainInfo[5].Contains("(s)"))
                    //{
                    //    tmpCycleTime = MainInfo[5].Substring(0, MainInfo[5].Length - 3);
                    //    finalCycleTime = tmpCycleTime;
                    //}
                    //else
                    //{

                    //    tmpCycleTime = MainInfo[5].Replace(":", "").Trim();
                    //    if (tmpCycleTime.Length < 6)
                    //    {
                    //        tmpCycleTime = "00" + tmpCycleTime;
                    //    }

                    //    if (tmpCycleTime.Length == 6)
                    //    {
                    //        t = DateTime.ParseExact(tmpCycleTime, "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    //        v = DateTime.ParseExact("000000", "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    //        tSecond = t.Subtract(v).TotalSeconds;
                    //        finalCycleTime = Convert.ToString(tSecond);


                    //    }
                    //    else
                    //    {
                    //        tmpCycleTime = tmpCycleTime.Substring(0, 10);
                    //        t = DateTime.ParseExact(tmpCycleTime, "HHmmss.fff", System.Globalization.CultureInfo.InvariantCulture);
                    //        v = DateTime.ParseExact("000000", "HHmmss.fff", System.Globalization.CultureInfo.InvariantCulture);
                    //        tSecond = t.Subtract(v).TotalSeconds;
                    //        finalCycleTime = Convert.ToString(tSecond);
                    //    }

                    //}

                }
                catch (Exception)
                {
                    finalCycleTime = MainInfo[5];
                }


                cmdVN.Connection = conVN;
                cmdVN.CommandText = "Pro_Insert_Data_New";
                cmdVN.CommandType = CommandType.StoredProcedure;

                cmdVN.Parameters.Add("IProduct", OracleDbType.Varchar2, Product, ParameterDirection.Input);

                cmdVN.Parameters.Add("IStation", OracleDbType.Varchar2, Station, ParameterDirection.Input);

                cmdVN.Parameters.Add("IModel", OracleDbType.Varchar2, PN, ParameterDirection.Input);

                cmdVN.Parameters.Add("ITitle_Version", OracleDbType.Varchar2, TitleVer, ParameterDirection.Input);

                cmdVN.Parameters.Add("ISN", OracleDbType.Varchar2, MainInfo[0], ParameterDirection.Input);

                cmdVN.Parameters.Add("ITEST_RESULT", OracleDbType.Varchar2, MainInfo[1], ParameterDirection.Input);

                cmdVN.Parameters.Add("IDETAIL_LOG_PATH", OracleDbType.Varchar2, MainInfo[2], ParameterDirection.Input);

                cmdVN.Parameters.Add("IDIAG_VERSION", OracleDbType.Varchar2, MainInfo[3], ParameterDirection.Input);

                cmdVN.Parameters.Add("IPC_NAME", OracleDbType.Varchar2, MainInfo[4], ParameterDirection.Input);

                cmdVN.Parameters.Add("ICYCLE_TIME", OracleDbType.Varchar2, finalCycleTime, ParameterDirection.Input);

                cmdVN.Parameters.Add("ITEST_TIME", OracleDbType.Varchar2, MainInfo[6], ParameterDirection.Input);

                cmdVN.Parameters.Add("IMAC1", OracleDbType.Varchar2, MainInfo[7], ParameterDirection.Input);

                cmdVN.Parameters.Add("IMAC2", OracleDbType.Varchar2, MainInfo[8], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_1", OracleDbType.Varchar2, MainInfo[9], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_2", OracleDbType.Varchar2, MainInfo[10], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_3", OracleDbType.Varchar2, MainInfo[11], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_4", OracleDbType.Varchar2, MainInfo[12], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_5", OracleDbType.Varchar2, MainInfo[13], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_6", OracleDbType.Varchar2, MainInfo[14], ParameterDirection.Input);

                cmdVN.Parameters.Add("IDetailData", OracleDbType.Varchar2, DivDataDetail, ParameterDirection.Input);

                cmdVN.Parameters.Add("IERRORINFO", OracleDbType.Varchar2, DivDataError, ParameterDirection.Input);

                cmdVN.Parameters.Add("RES", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                conVN.Open();
                OracleDataReader drVN = cmdVN.ExecuteReader();
                string strresultVN = cmdVN.Parameters["RES"].Value.ToString();

                drVN.Close();
                conVN.Close();
                return strresultVN;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string InsertDBVnNew(string Product, string Station, string PN, string TitleVer, string[] MainInfo, string DivDataDetail, string DivDataError)
        {
            //2017.05.03  Modify to fix duplicate data

            string newDivDataDetail = "";

            OracleConnection conVN = new OracleConnection(DBNameVN);
            OracleCommand cmdVN = new OracleCommand();

            try
            {
                DivDataError = DivDataError.Replace("'", "");
                DivDataDetail = DivDataDetail.Replace("'", "");
                DivDataDetail = RemoveChar(DivDataDetail, "\t");
                DivDataDetail = RemoveChar(DivDataDetail, "  "); // 2 space to not split FT RC
                DivDataDetail = RemoveChar(DivDataDetail, ";");
                DivDataDetail = RemoveChar(DivDataDetail, ",,");

                try
                {

                    string[] countItem = DivDataDetail.Split(',');
                    foreach (string iS in countItem)
                    {
                        if (Station.Contains("PT") || Station.Contains("NFT"))
                        {
                            string[] newStr = iS.Split(' ');
                            if (newStr.Length > 0)
                            {
                                foreach (string iStr in newStr)
                                {
                                    if (iStr != "")
                                    {
                                        newDivDataDetail += iStr.Trim() + ",";
                                    }
                                }
                            }
                            else
                            {
                                newDivDataDetail += iS.Trim() + ",";
                            }
                        }
                        else
                        {
                            newDivDataDetail += iS.Trim() + ",";
                        }
                    }

                    if (newDivDataDetail.StartsWith(","))
                    {
                        newDivDataDetail = newDivDataDetail.Remove(0, 1);
                    }
                    DivDataDetail = newDivDataDetail;
                }
                catch (Exception r)
                {
                    //track_log("RemoveChar",r.ToString());
                }

                string tmpCycleTime = "";
                string finalCycleTime = "";
                DateTime t, v;
                double tSecond = 0;
                //WN2500RP-100NASV2	124	3.0	@ZO10FSB	1	F:\lsy\ID\U12H294\RC\PASS\20160418.txt	RV1.0.0.1	FOX-B05L2RC1	070110 <- cycle time co van de
                // support convert: 00:02:11.906 / 12(s) / 01:00 / 00:01:30 /7.188 
                try
                {
                    if (MainInfo[5].Contains("(s)"))
                    {
                        tmpCycleTime = MainInfo[5].Substring(0, MainInfo[5].Length - 3);
                        finalCycleTime = tmpCycleTime;
                    }
                    else
                    {

                        tmpCycleTime = MainInfo[5].Replace(":", "").Trim();
                        if (tmpCycleTime.Length < 6)
                        {
                            tmpCycleTime = "00" + tmpCycleTime;
                        }

                        if (tmpCycleTime.Length == 6)
                        {
                            t = DateTime.ParseExact(tmpCycleTime, "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            v = DateTime.ParseExact("000000", "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            tSecond = t.Subtract(v).TotalSeconds;
                            finalCycleTime = Convert.ToString(tSecond);


                        }
                        else
                        {
                            tmpCycleTime = tmpCycleTime.Substring(0, 10);
                            t = DateTime.ParseExact(tmpCycleTime, "HHmmss.fff", System.Globalization.CultureInfo.InvariantCulture);
                            v = DateTime.ParseExact("000000", "HHmmss.fff", System.Globalization.CultureInfo.InvariantCulture);
                            tSecond = t.Subtract(v).TotalSeconds;
                            finalCycleTime = Convert.ToString(tSecond);
                        }

                    }

                }
                catch (Exception)
                {
                    finalCycleTime = MainInfo[5];
                }


                cmdVN.Connection = conVN;
                cmdVN.CommandText = "Pro_Insert_Data_update";
                cmdVN.CommandType = CommandType.StoredProcedure;


                cmdVN.Parameters.Add("IProduct", OracleDbType.Varchar2, Product, ParameterDirection.Input);

                cmdVN.Parameters.Add("IStation", OracleDbType.Varchar2, Station, ParameterDirection.Input);

                cmdVN.Parameters.Add("IModel", OracleDbType.Varchar2, PN, ParameterDirection.Input);

                cmdVN.Parameters.Add("ITitle_Version", OracleDbType.Varchar2, TitleVer, ParameterDirection.Input);

                cmdVN.Parameters.Add("ISN", OracleDbType.Varchar2, MainInfo[0], ParameterDirection.Input);

                cmdVN.Parameters.Add("ITEST_RESULT", OracleDbType.Varchar2, MainInfo[1], ParameterDirection.Input);

                cmdVN.Parameters.Add("IDETAIL_LOG_PATH", OracleDbType.Varchar2, MainInfo[2], ParameterDirection.Input);

                cmdVN.Parameters.Add("IDIAG_VERSION", OracleDbType.Varchar2, MainInfo[3], ParameterDirection.Input);

                cmdVN.Parameters.Add("IPC_NAME", OracleDbType.Varchar2, MainInfo[4], ParameterDirection.Input);

                cmdVN.Parameters.Add("ICYCLE_TIME", OracleDbType.Varchar2, finalCycleTime, ParameterDirection.Input);

                cmdVN.Parameters.Add("ITEST_TIME", OracleDbType.Varchar2, MainInfo[6], ParameterDirection.Input);

                cmdVN.Parameters.Add("IMAC1", OracleDbType.Varchar2, MainInfo[7], ParameterDirection.Input);

                cmdVN.Parameters.Add("IMAC2", OracleDbType.Varchar2, MainInfo[8], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_1", OracleDbType.Varchar2, MainInfo[9], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_2", OracleDbType.Varchar2, MainInfo[10], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_3", OracleDbType.Varchar2, MainInfo[11], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_4", OracleDbType.Varchar2, MainInfo[12], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_5", OracleDbType.Varchar2, MainInfo[13], ParameterDirection.Input);

                cmdVN.Parameters.Add("IEXT_6", OracleDbType.Varchar2, MainInfo[14], ParameterDirection.Input);

                cmdVN.Parameters.Add("IGOLDEN", OracleDbType.Varchar2, MainInfo[15], ParameterDirection.Input);

                cmdVN.Parameters.Add("IDetailData", OracleDbType.Varchar2, DivDataDetail, ParameterDirection.Input);

                cmdVN.Parameters.Add("IERRORINFO", OracleDbType.Varchar2, DivDataError, ParameterDirection.Input);

                cmdVN.Parameters.Add("RES", OracleDbType.Varchar2, 32).Direction = ParameterDirection.Output;

                conVN.Open();
                OracleDataReader drVN = cmdVN.ExecuteReader();
                string strresultVN = cmdVN.Parameters["RES"].Value.ToString();

                drVN.Close();
                conVN.Close();
                return "";
            }
            catch (Exception ee)
            {
                return "Insert VN Server Fail";
            }
        }


        public string InsertDBRecord(string Product, string Station, string PN, string TitleVer, string[] MainInfo, string DivDataDetail, string DivDataError)
        {
            string errMsg = "";

            string isnew = SonaWF.IO_ini.ReadIniFile(PN, Station, "0", ".\\ModelConfig.ini");
            if (isnew == "1")
            {
                errMsg = InsertDBVnNew(Product, Station, PN, TitleVer, MainInfo, DivDataDetail, DivDataError);
            }
            else
            {
                errMsg = InsertDBVn(Product, Station, PN, TitleVer, MainInfo, DivDataDetail, DivDataError);
            }
            return errMsg;

        }
    }
}
