using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSTemplate
{
    public class Limits
    {
        public string model;
        public string station_type;
        public string timestamp;
        public string limits_validation;
        public Dictionary<string, ItemLimits> limits;
    }

    public class ItemLimits
    {
        public int id;
        public DateTime last_updated_at;
        public string model;
        public string station_type;
        public string test_name;
        public string limit_type;
        public int required;
        public string lower_limit;
        public string upper_limit;
        public string units;
        public string error_code;
        public int locked;
    }
}
