using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using BL;
using DAL;
using Main.MVVM_Core;
using Main.Pages;
using Main.ViewModels.Components;

namespace Main.ViewModels
{
    public class ClientHomeViewModel: BaseViewModel
    {
        private readonly ICurrentUserService userService;
        private readonly EventBus eventBus;
        private readonly IUserRegisterService registerService;
        private readonly PageService pageService;

        public bool IsGreeting { get; set; }

        public bool IsBackgroundVisible { get; set; } = true;
        public string GreetingText { get; set; }

        bool _first = true;

        public bool IsTextVisible { get; set; } = true;

        public ClientHomeViewModel(ICurrentUserService userService, EventBus eventBus, IUserRegisterService registerService, PageService pageService)
        {
            this.userService = userService;
            this.eventBus = eventBus;
            this.registerService = registerService;
            this.pageService = pageService;

            //registerService.Registered += UserService_Registered;
            eventBus.Subscribe<MVVM_Core.Events.OrderCompleted, ClientHomeViewModel>(OrderCompleted, false);
            eventBus.Subscribe<MVVM_Core.Events.ClientRegistered, ClientHomeViewModel>(OnClientRegistered, false);
            
        }

        private async Task OrderCompleted(MVVM_Core.Events.OrderCompleted @event)
        {
            Notifications.Add(
                new NotificationItem($"Заказ по усулуге \"{@event.ServiceName}\" отравлен!", type: NotificationType.Success));
            
        }

        private async Task OnClientRegistered(MVVM_Core.Events.ClientRegistered @event)
        {
            Notifications.Add(new NotificationItem("Регистрация прошла успешно!", type: NotificationType.Success));            
        }

        public ICommand RemoveNotification => new Command(x =>
        {
            if(x is ContentControl control && control.Content is NotificationItem item)
            {
                Notifications.Remove(item);
            }
        });

        public ObservableCollection<NotificationItem> Notifications { get; set; } = new ObservableCollection<NotificationItem>()
        {
            new NotificationItem("Привет"),
        };

        async void ToGreet()
        {

            int hour = DateTime.Now.Hour;
            var text = string.Empty;

            if (hour < 12 && hour >= 6)
            {
                text = "Доброе утро";
            }
            else if(hour >= 12 && hour < 16)
            {
                text = "Добрый день";
            }
            else if(hour >= 16 && hour < 20)
            {
                text = "Добрый вечер";
            }
            else
            {
                text = "Добрый ночи";
            }

            if (userService.IsAutorized)
            {
                text += $", {userService.CurrentUser.Name}!";
            }

            await TextShowingStack(new string[] { text });
            _first = false;

        }
        
        async Task TextShowingStack(ICollection<string> strings)
        {

            foreach(var str in strings)
            {
                GreetingText = str;
                IsGreeting = true;

                int time = str.Length * 100 + 1000;
                await Task.Delay(time);

                IsGreeting = false;

                await Task.Delay(900);
            }
            IsBackgroundVisible = IsTextVisible = false;
            
        }


        public ICommand ToServicesView => new Command(x =>
        {
            pageService.ChangePage<ServicesPage>(AnimateTo.Left);
        });

    }
}
