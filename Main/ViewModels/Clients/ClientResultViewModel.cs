using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DAL.Models;
using System.Windows.Input;
using Main.Pages;

namespace Main.ViewModels
{
    public class ClientResultViewModel: BaseViewModel
    {
        private readonly PageService pageService;

        public ClientResultViewModel(
            BL.PageService pageService)
        {
            this.pageService = pageService;
            Message = "Заказ успешно оформлен";
        }


        public ICommand ToHomePage => new Command(x =>
        {
            pageService.ChangePage<Pages.ClientHomePage>(AnimateTo.Rigth);
        });

        public string Message { get; set; }
    }
}
