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
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;

namespace Main.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel(PageService pageService, ICurrentUserService userService)
        {
            pageService.PageChanged += PageService_PageChanged;
            userService.Autorized += UserService_Autorized;
            userService.Exited += UserService_Exited;
            pageService.ChangePage<LoginPage>(AnimateTo.None);
        }

        private void UserService_Exited()
        {
            
        }

        private void UserService_Autorized()
        {
            
        }

        public int Width { get; set; } = 800;

        public ObservableCollection<Page> Pages { get; set; } = new ObservableCollection<Page>();

        public Page CurrentPage { get; set; }

        private async void PageService_PageChanged(Page page, AnimateTo animate)
        {
            if (CurrentPage != null)
            {
                await fadeOut(CurrentPage.Content as UIElement);
                
            }

            CurrentPage = page;

            if (animate != AnimateTo.None)
            {
                Play(page.Content as UIElement, animate);
            }

            //await fadeIn(page.Content as UIElement);
        }

        void Play(UIElement uI, AnimateTo animate)
        {
            int from =  animate == AnimateTo.Left ? Width : -Width;

            uI.RenderTransform = new TranslateTransform(from, 0);
            uI.Opacity = 1;

            DoubleAnimation anim = new DoubleAnimation(from, 0, TimeSpan.FromSeconds(0.5));

            anim.EasingFunction = new ElasticEase() { Oscillations = 0 };
            uI.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        async Task fadeOut(UIElement element)
        {
            for (double i = 1; i >= 0; i-= 0.1)
            {
                element.Opacity = i;
                await Task.Delay(5);
            }
            element.Opacity = 0;
        }
        async Task fadeIn(UIElement element)
        {
            for (double i = 0; i <= 1; i += 0.05)
            {
                element.Opacity = i;
                await Task.Delay(10);
            }
            element.Opacity = 1;
        }

    }
}
