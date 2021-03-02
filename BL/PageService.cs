using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;
namespace BL
{
    public class PageService
    {
        public event Action<Page> PageChanged;
        public void ChangePage<TPage>() where TPage: Page, new()
        {
            var page = new TPage();
            ChangePage(page);
        }
        public void ChangePage(Page page)
        {
            PageChanged?.Invoke(page);
        }        
    }
}
