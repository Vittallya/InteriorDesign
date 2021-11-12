using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL;
using DAL.Models;
using System.Windows.Input;
using System.Windows.Controls;

namespace Main.ViewModels
{
    public class ClientRegisterViewModel: BasePageViewModel, IDisposable
    {
        private readonly PageService pageService;
        private readonly IUserRegisterService registerService;
        private readonly EventBus eventBus;
        private readonly ICurrentUserService userService;

        public ClientRegisterViewModel(PageService pageService,
            IUserRegisterService registerService, EventBus eventBus, ICurrentUserService userService) : base(pageService)
        {
            this.pageService = pageService;
            this.registerService = registerService;
            this.eventBus = eventBus;
            this.userService = userService;
            if (registerService.NameAndEmailSetted)
            {
                Name = registerService.Name;
                Email = registerService.Email;
            }
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }

        public ICommand First => new Command(x =>
        {
            registerService.SetClientNameAndEmail(Name, Email);

            if (Rules.Pages.EMAIL_CONFIRM_REQUIRED)
            {
                pageService.ChangePage<Pages.ClientEmailCodeConfirmPage>(Rules.Pages.CLIENT_REGISTRATION_POOL, AnimateTo.Left);
            }
            else
            {
                pageService.ChangePage<Pages.ClientEnterPasswordPage>(Rules.Pages.CLIENT_REGISTRATION_POOL, AnimateTo.Left);
            }
        }, y => Email != null && Name != null);



        public string Message { get; set; }
        public bool MessageVisible { get; set; }

        public PasswordBox PasswordBox { get; set; } = new PasswordBox();
        public PasswordBox PasswordBoxConfirm { get; set; } = new PasswordBox();

        public ICommand EnterPasswordCommand => new CommandAsync(async x =>
        {
            MessageVisible = false;

            if (PasswordBox.Password != PasswordBoxConfirm.Password)
            {
                MessageVisible = true;
                Message = "Пароли не совпадают";
                return;
            }
            else if(PasswordBox.Password.Length < 3)
            {

                MessageVisible = true;
                Message = "Длина пароля должна быть больше 2х символов";
                return;
            }

            if(Login != null && Login.Length > 0 && Login.Length < 3)
            {
                MessageVisible = true;
                Message = "Длина логина должна быть больше 2х символов (если не хотите вводить логин, оставьте поле пустым)";
                return;
            }

            if (await registerService.TryRegister(PasswordBox.Password, Login))
            {
                userService.SetupUser(registerService.GetRegistered(), false);
                pageService.ClearHistoryByPool(Rules.Pages.CLIENT_REGISTRATION_POOL);
                await eventBus.Publish(new MVVM_Core.Events.ClientRegistered(userService.CurrentUser));
                this.Dispose();
            }
            else
            {
                MessageVisible = true;
                Message = registerService.ErrorMessage;
            }

        }, x => PasswordBox.Password != null && 
        PasswordBox.Password.Length > 0 && 
        PasswordBoxConfirm.Password != null);

        public override int PoolIndex => Rules.Pages.CLIENT_REGISTRATION_POOL;

        public override ICommand BackCommand => new Command(x =>
        {
            if (registerService.NameAndEmailSetted)
            {
                pageService.Back<Pages.ClientRegisterPage>(PoolIndex);
                registerService.NameAndEmailSetted = false;
            }
            else
                pageService.ChangeToLastByPool(Rules.Pages.SERVICES_POOL);
        });

        public void Dispose()
        {
            Login = null;
            PasswordBox.Password = null;
            PasswordBoxConfirm.Password = null;
            Email = null;
        }
    }
}
