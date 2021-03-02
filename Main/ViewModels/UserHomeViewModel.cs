using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using BL;
using DAL;
using Main.MVVM_Core;

namespace Main.ViewModels
{
    public class UserHomeViewModel: BaseViewModel
    {
        private readonly ICurrentUserService userService;

        public bool IsGreeting { get; set; }

        public bool IsBackgroundVisible { get; set; } = true;
        public string GreetingText { get; set; }

        public bool IsTextVisible { get; set; } = true;

        public UserHomeViewModel(ICurrentUserService userService)
        {
            this.userService = userService;
            ToGreet();
        }

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
            TextShowingStack(new string[] { text, "Почти все готово..." });

        }
        
        async void TextShowingStack(ICollection<string> strings)
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


        public ICommand GoToCalculatePage => new Command(x =>
        {
            
        });

    }
}
