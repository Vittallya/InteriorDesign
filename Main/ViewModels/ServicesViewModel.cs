using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL;
using DAL.Models;
using System.Collections.ObjectModel;

namespace Main.ViewModels
{
    public class ServicesViewModel: BasePageViewModel
    {
        public ServicesViewModel(PageService pageService):base(pageService)
        {

        }
    }
}
