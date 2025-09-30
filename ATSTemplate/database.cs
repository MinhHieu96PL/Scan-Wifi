using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSTemplate
{
    public class database
    {
        private string connection_string = @"server=10.90.10.168;userid=ats;password=atsAdmin123#;database=ats_deploy";
        MySqlConnection connection;

        public database(string ip)
        {
            connection_string = @"server=" + ip + ";userid=ats;password=atsAdmin123#;database=ats_deploy";
        }
        private void Connect()
        {
            connection = new MySqlConnection(connection_string);
            connection.Open();
        }

        private void Close()
        {
            connection.Close();
        }

        public int Login(string user, string password)
        {
            try
            {
                Connect();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select count(*) from unlock_account where user='" + user +"' and password='"+ password +"'";

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                Close();
            }

            return -1;
        }

    }
}
