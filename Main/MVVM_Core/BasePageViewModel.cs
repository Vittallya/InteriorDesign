using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BL;
using Main.MVVM_Core;

namespace Main.MVVM_Core
{
    public abstract class BasePageViewModel: BaseViewModel
    {
        private readonly PageService pageservice;

        public ICommand BackCommand => new Command(x =>
        {
            pageservice.Back();
        });
        public BasePageViewModel(PageService pageservice)
        {
            this.pageservice = pageservice;
        }
    }
}
