using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BL
{
    public class PagesBinder
    {

        private Dictionary<Type, Action> _bindings = new Dictionary<Type, Action>();

        public void Bind<TPage>(Action action) where TPage : Page, new()
        {
            var type = typeof(TPage);

            if (!_bindings.ContainsKey(type))
            {
                _bindings.Add(type, action);
            }
        }
        public void CheckBind<TPage>() where TPage: Page, new()
        {
            var type = typeof(TPage);

            if (_bindings.ContainsKey(type))
            {
                _bindings[type]?.Invoke();
            }
        }
    }
}
