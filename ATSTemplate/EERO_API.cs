using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSTemplate
{
    public class EERO_API
    {
        public string mode;
        public string station_name;
        public string error_code;
        public string error_details;
        public string position;
        public string serial;
        public string station_type;
        public string test_software_version;
        public string finish_time;//yyyy-MM-dd HH:mm:ss
        public string start_time;//yyyy-MM-dd HH:mm:ss
        public string status;
        public List<API_TestItem> tests;
    }

    public class API_TestItem
    {
        public string upper_limit;
        public string lower_limit;
        public string status;
        public string start_time;//yyyy-MM-dd HH:mm:ss
        public string finish_time;//yyyy-MM-dd HH:mm:ss
        public string test_name;
        public string error_code;
        public string test_value;
        public string units;
    }

    public class Data_API
    {
        public string product;
        public string mo;
        public string onSfis;
        public string line;
        public string pnname;
        public string mode;
        public string station;
        public string pcname;
        public string position;
        public string mlbsn;
        public string sn;
        public string ethernetmac;
        public string test_software_version;
        public string error_code;
        public string error_details;
        public string start_time;
        public string finish_time;
        public string status;
        public List<API_TestItem> tests;
    }
}
