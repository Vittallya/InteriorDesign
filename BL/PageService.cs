using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;
namespace BL
{

    public enum AnimateTo
    {
        None, Left, Rigth
    };

    public class PageService
    {
        public event Action<Page, AnimateTo> PageChanged;
        public void ChangePage<TPage>(AnimateTo animate = AnimateTo.None) where TPage: Page, new()
        {
            var page = new TPage();

            ChangePage(page, animate);
            _history.Add(page);
        }
        public void ChangePage(Page page, AnimateTo animateTo)
        {
            PageChanged?.Invoke(page, animateTo);
        }

        List<Page> _history = new List<Page>();


        public void Back(bool animatePlaying = true)
        {
            if(_history.Count > 1)
            {
                Page last = _history.LastOrDefault();
                Page target = _history.Skip(_history.Count - 2).FirstOrDefault();

                _history.Remove(last);
                PageChanged?.Invoke(target, animatePlaying ? AnimateTo.Rigth : AnimateTo.None);
            }


        }
    }
}
