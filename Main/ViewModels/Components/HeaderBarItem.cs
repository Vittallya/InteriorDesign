using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Main.ViewModels.Components
{
    public class HeaderBarItem
    {
        public string Header { get; set; }
        public Type PageType { get; set; }

        public override string ToString()
        {
            return Header;
        }
    }
}
