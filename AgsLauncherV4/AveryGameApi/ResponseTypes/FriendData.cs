using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgsLauncherV4.AveryGameApi.ResponseTypes
{
    public class FriendData
    {
        public List<FriendBase> friends { get; set; }
    }

    public class FriendBase
    {
        public string id { get; set; }
        public bool pending { get; set; }
        public bool sbna { get; set; }
    }
}
