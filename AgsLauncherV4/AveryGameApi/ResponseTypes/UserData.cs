using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgsLauncherV4.AveryGameApi.ResponseTypes
{
    public class UserData
    {
        public string username { get; set; }
        public string id { get; set; }
        public bool banned { get; set; }
        public bool isAdmin { get; set; }
        public bool betaTester { get; set; }
        public string profilePhoto { get; set; }
        public string MtxCurrency { get; set; }
    }
}
