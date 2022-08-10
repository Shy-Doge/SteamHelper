using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamHelper
{
    [Serializable]
    public class games
    {
            public games(string selectedFileName) { }

            public string Name { get; set; }
            public string Folder { get; set; }

            public games(string folder, string name)
            {
                this.Folder = folder;
                this.Name = name;
            }
    }
}
