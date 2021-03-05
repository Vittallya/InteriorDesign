using Microsoft.Extensions.DependencyInjection;
using System;
using Main.ViewModels;
using DAL;
using BL;
using System.Threading.Tasks;

namespace Main
{
    public class Locator
    {
        public static IServiceProvider Services { get; set; }
        public async static void InitServices()
        {
            IServiceCollection services = new ServiceCollection();

            //ViewModels//////////////////////////////////
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<ClientHomeViewModel>();
            services.AddTransient<StylesViewModel>();
            services.AddSingleton<DesignParamsViewModel>();
            services.AddTransient<OrderConfirmViewModel>();
            services.AddTransient<NewClientViewModel>();
            services.AddTransient<ServicesViewModel>();


            //Services/////////////////////////////////


            services.AddTransient<AllDbContext>();


            services.AddSingleton<PageService>();
            services.AddSingleton<ICurrentUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<StylesService>();
            services.AddTransient<EditStylesService>();
            services.AddSingleton<OrderService>();
            services.AddSingleton<OrderDetailsService>();
            services.AddTransient<IServicesModelService, ServicesModelSerivce>();


            



            Services = services.BuildServiceProvider();
        }
        public  MainViewModel MainViewModel => Services.GetRequiredService<MainViewModel>();
        public  LoginViewModel LoginViewModel => Services.GetRequiredService<LoginViewModel>();
        public  ClientHomeViewModel UserHomeViewModel => Services.GetRequiredService<ClientHomeViewModel>();
        public  StylesViewModel StylesViewModel => Services.GetRequiredService<StylesViewModel>();
        public  EditStylesViewModel EditStylesViewModel => Services.GetRequiredService<EditStylesViewModel>();
        public  DesignParamsViewModel DesignParamsViewModel => Services.GetRequiredService<DesignParamsViewModel>();
        public  OrderConfirmViewModel OrderConfirmViewModel => Services.GetRequiredService<OrderConfirmViewModel>();
        public  NewClientViewModel NewClientViewModel => Services.GetRequiredService<NewClientViewModel>();
        public  ServicesViewModel ServicesViewModel => Services.GetRequiredService<ServicesViewModel>();
    }
}
