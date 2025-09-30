using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSTemplate
{
    public enum Status
    {
        READY,
        TESTING,
        FAIL,
        PASS,
        WARNING
    }
    public enum TestMode
    {
        Production,
        Debug,
        RMA,
        RELIABILITY
    }

    public enum LogType
    {
        LOG,
        DUT,
        FIXTURE,
        PC,
        GOLDEN
    }

    public enum SFCS_mode
    {
        ON,
        OFF
    }

    public struct DUT
    {
        public string sn { get; set; }
        public string mlbsn { get; set; }
        public string ethernetmac { get; set; }
        public string type { get; set; }
        public string mo { get; set; }
        public string IP { get; set; }
    }

    public class Golden
    {
        public string sn = "";
        public string mblsn = "";
        public string mac = "";
    }

    public class Common
    {
        private Form1 ats;
        public Common(Form1 _ats)
        {
            this.ats = _ats;
        }
        
        public string GetStringByRegex(string source, string sregex)
        {
            try
            {
                Regex regex = new Regex(sregex);
                Match m = regex.Match(source);
                return m.Value;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public Dictionary<string, double[]> ReadPathLoss(string file, ref string error)
        {
            error = "";
            Dictionary<string, double[]> pathLoss = new Dictionary<string, double[]>();
            try
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    string[] items = line.Split(',');
                    if (items.Length <= 1)
                    {
                        continue;
                    }
                    double[] values = new double[items.Length - 1];
                    for (int i = 1; i < items.Length; i++)
                    {
                        values[i - 1] = double.Parse(items[i], CultureInfo.InvariantCulture);
                    }

                    pathLoss.Add(items[0], values);
                }
                return pathLoss;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public bool UpdatePathLoss(Dictionary<string, double[]> PathLoss, string file, ref string error)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (string key in PathLoss.Keys)
                {
                    string line = key;
                    double[] value = PathLoss[key];
                    for (int i = 0; i < value.Length; i++)
                    {
                        line += "," + value[i];
                    }

                    lines.Add(line);
                }

                File.WriteAllLines(file, lines.ToArray());

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool UpdatePathLoss(Dictionary<string, string> PathLoss, string file, ref string error)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (string key in PathLoss.Keys)
                {
                    string line = key + "," + PathLoss[key];
                    lines.Add(line);
                }

                File.WriteAllLines(file, lines.ToArray());

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

    }
}
