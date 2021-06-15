using BL;
using Main.MVVM_Core;
using Main.ViewModels.Components;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Main.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly PageService pageService;
        private readonly ICurrentUserService userService;
        private readonly PageAnimationService animationService;

        public MainViewModel(PageService pageService, ICurrentUserService userService, PageAnimationService animationService)
        {
            this.animationService = animationService;
            this.pageService = pageService;
            this.userService = userService;

            pageService.PageChanged += PageService_PageChanged;
            userService.Autorized += UserService_Autorized;
            userService.Exited += UserService_Exited;
            userService.Skipped += UserService_Skipped;
            pageService.ChangePage<Main.Pages.LoginPage>();
        }
        public string UserName { get; set; }
        public bool IsClient { get; set; }
        private void UserService_Skipped()
        {
            UserName = "Гость";
            HeaderPanelVisibility = true;
            IsGuest = true;
        }
        public bool HeaderPanelVisibility { get; set; }
        public bool IsGuest { get; set; }

        public bool IsOrdersVis { get; set; }

        private void UserService_Exited()
        {
            pageService.ClearAllPools();
            HeaderPanelVisibility = false;
            pageService.ChangePage<Main.Pages.LoginPage>();
        }
        private void UserService_Autorized()
        {
            UserName = userService.CurrentUser?.Name;
            HeaderPanelVisibility = true;
            IsGuest = false;
            IsOrdersVis = !userService.IsAdmin;
        }
        public ICommand ToLoginPageCommand => new Command(x =>
        {
            userService.Logout();
        });
        public ICommand ToOrdersViewCommand => new Command(x =>
        {
            pageService.ChangePage<Main.Pages.Client.ClientOrdersPage>();
        });

        public bool RightUserIconVis { get; set; }

        public ObservableCollection<HeaderBarItem> HeaderItems { get; set; } = new ObservableCollection<Components.HeaderBarItem>
        {
            new Components.HeaderBarItem
            {
                Header = "Главная",
                PageType = typeof(Pages.ClientHomePage)
            },
            new Components.HeaderBarItem
            {
                Header = "Услуги",
                PageType = typeof(Pages.ServicesPage)
            },
            new Components.HeaderBarItem
            {
                Header = "Наши сотрудники",
                PageType = typeof(Pages.WorkersPage)
            },
            new Components.HeaderBarItem
            {
                Header = "О Нас",
                PageType = typeof(Pages.AboutPage)
            },
        };
        public HeaderBarItem SelectedHeader 
        { 
            get => selectedHeader;
            set 
            { 
                selectedHeader = value;
                OnPropertyChanged();

                if (!changedFromVm)
                {
                    HeaderPageSelected(value.PageType);
                }
            } 
        }

        bool changedFromVm;

        public bool HeaderVisible { get; set; } = true;

        void HeaderPageSelected(Type pageType)
        {
            if(pageType == typeof(Pages.ServicesPage))
            {
                if (pageService.HasActualPool())
                {
                    pageService.ChangeToLastByActualPool();
                }
                else
                {
                    pageService.ChangePage<Pages.ServicesPage>(Rules.Pages.SERVICES_POOL);
                }
            }
            else
            {
                pageService.ChangePage(pageType);
            }
        }

        void HeaderSelector(Type pageType)
        {
            HeaderPanelVisibility = HeaderItems.Any(x => x.PageType == pageType);

            if (HeaderPanelVisibility)
            {
                changedFromVm = true;
                SelectedHeader = HeaderItems.First(x => x.PageType == pageType);
                changedFromVm = false;                
            }
        }

        private HeaderBarItem selectedHeader;

        public int Width { get; set; } = 800;
        public Page CurrentPage { get; set; }

        public SolidColorBrush GridBackground { get; set; }
        public SolidColorBrush MenuForeground { get; set; }


        public bool LeftUserIconVis { get; set; }

        private async void PageService_PageChanged(Page page, AnimateTo animate)
        {
            RightUserIconVis = page.GetType() == typeof(Pages.ClientHomePage);

            LeftUserIconVis = !RightUserIconVis && page.GetType() != typeof(Pages.LoginPage);

            if (RightUserIconVis)
            {
                GridBackground = new SolidColorBrush(Colors.Black) {Opacity = 0.4 };
                MenuForeground = new SolidColorBrush(Colors.White);
            }
            else
            {
                MenuForeground = new SolidColorBrush(Colors.Black);
                GridBackground = new SolidColorBrush(Colors.White);
            }

            HeaderSelector(page.GetType());
            if (CurrentPage != null)
            {
                await animationService.fadeOut(CurrentPage.Content as UIElement);
            }

            CurrentPage = page;

            if (animate != AnimateTo.None)
            {
                animationService.Play(page.Content as UIElement, animate, Width);
            }
            else
            {
                await animationService.fadeIn(CurrentPage.Content as UIElement);

            }     
            

        }
    }
}
