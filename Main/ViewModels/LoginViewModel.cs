using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Pages;
using BL;
using DAL;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        private readonly ILoginService loginService;
        private readonly DbContextLoader loader;
        private readonly PageService pageService;
        public PasswordBox PassBox { get; set; } = new PasswordBox
        {
            
        };
        public string Login { get; set; }
        public LoginViewModel(ILoginService loginService,
                                DbContextLoader loader,
                              PageService pageService)
        {
            this.loginService = loginService;
            this.loader = loader;
            this.pageService = pageService;
        }
        public bool IsErrorVisible { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsAnimationVisible { get; set; }
        public ICommand LoginCommand => new CommandAsync(async x =>
        {
            while (!loader.IsLoaded)
            {
                await Task.Delay(200);
            }

            IsErrorVisible = false;
            IsAnimationVisible = true;
            bool res = await loginService.TryLogin(Login, PassBox.Password, IamAdmin);
            IsAnimationVisible = false;

            if (res)
            {

                if (IamAdmin)
                    pageService.ChangePage<Pages.Admin.AdminPage>();
                else
                {
                    pageService.ChangePage<ClientHomePage>();
                }
            }
            else
            {
                IsErrorVisible = true;
                ErrorMessage = loginService.ErrorMessage;
            }

        });
        public ICommand AcceptCommand => new Command(x =>
        {
            loginService.Skip();
            pageService.ChangePage<ClientHomePage>();
        });
        public bool IamAdmin { get; set; }
    }
}
