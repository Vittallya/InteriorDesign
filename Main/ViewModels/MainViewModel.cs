using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using DAL;
using DAL.Models;
using Main.MVVM_Core;
using BL;
using System.Windows.Controls;
using Main.Pages;
using System.Reflection;

namespace Main.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel(PageService pageService, ICurrentUserService userService)
        {
            pageService.PageChanged += PageService_PageChanged;
            userService.Autorized += UserService_Autorized;
            userService.Exited += UserService_Exited;
            pageService.ChangePage<LoginPage>();
        }

        private void UserService_Exited()
        {
            
        }

        private void UserService_Autorized()
        {
            
        }

        public Page CurrentPage { get; set; }

        private void PageService_PageChanged(Page page)
        {
            CurrentPage = page;
        }
    }
}
