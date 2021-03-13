using BL;
using Main.MVVM_Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void UserService_Skipped()
        {
            UserName = "Гость";
            HeaderPanelVisibility = true;
            IsGuest = true;
        }

        public bool HeaderPanelVisibility { get; set; }
        public bool IsGuest { get; set; }

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
        }

        public ICommand ToLoginPageCommand => new Command(x =>
        {
            userService.Logout();
        });

        

        public ICommand ToOrdersViewCommand => new Command(x =>
        {
            MessageBox.Show("...ждите в дипломной =)..");
        });

        public ICommand ToMainPageCommand => new Command(x =>
        {
            if (x is RadioButton radio && radio != _selected)
            {
                _selected = radio;                
                pageService.ChangePage<Main.Pages.ClientHomePage>();                
            }
        });

        public ICommand ChooseService => new Command(x =>
        {
            if (x is RadioButton radio && radio != _selected)
            {
                _selected = radio;
                if (pageService.HasActualPool())
                {
                    pageService.ChangeToLastByActualPool();
                }
                else
                {
                    pageService.ChangePage<Main.Pages.ServicesPage>(Rules.Pages.SERVICES_POOL);
                }
            }
        });

        public ICommand OurEmployees => new Command(x =>
        {
            if (x is RadioButton radio && radio != _selected)
            {
                _selected = radio;
                pageService.ChangePage<Main.Pages.WorkersPage>();
            }
        });

        public ICommand About => new Command(x =>
        {
            if (x is RadioButton radio && radio != _selected)
            {
                _selected = radio;
                pageService.ChangePage<Main.Pages.AboutPage>();
            }
        });

        RadioButton _selected;

        public ListViewItem SelectedItemBar { get; set; }
        public int SelectedItemIndex { get; set; }

        public int Width { get; set; } = 800;

        public ObservableCollection<Page> Pages { get; set; } = new ObservableCollection<Page>();

        public Page CurrentPage { get; set; }

        private async void PageService_PageChanged(Page page, AnimateTo animate)
        {

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
