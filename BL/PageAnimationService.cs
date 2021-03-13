using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BL
{
    public class PageAnimationService
    {
        public async Task fadeIn(UIElement element)
        {
            element.Opacity = 0;
            for (double i = 0; i <= 1; i += 0.1)
            {
                element.Opacity = i;
                await Task.Delay(10);
            }
            element.Opacity = 1;
        }

        public async Task fadeOut(UIElement element)
        {
            element.Opacity = 1;
            for (double i = 1; i >= 0; i -= 0.1)
            {
                element.Opacity = i;
                await Task.Delay(5);
            }
            element.Opacity = 0;
        }

        public void Play(UIElement uI, AnimateTo animate, int Width)
        {
            int from = animate == AnimateTo.Left ? Width : -Width;

            uI.RenderTransform = new TranslateTransform(from, 0);
            uI.Opacity = 1;

            DoubleAnimation anim = new DoubleAnimation(from, 0, TimeSpan.FromSeconds(0.5));

            anim.EasingFunction = new ElasticEase() { Oscillations = 0 };
            uI.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}