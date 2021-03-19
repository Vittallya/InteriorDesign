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
        public static void InitServices()
        {
            IServiceCollection services = new ServiceCollection();

            //ViewModels//////////////////////////////////
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<ClientHomeViewModel>();
            services.AddTransient<StylesViewModel>();
            services.AddSingleton<DesignParamsViewModel>();
            services.AddTransient<OrderConfirmViewModel>();
            services.AddTransient<ServicesViewModel>();
            services.AddTransient<OtherParamsViewModel>();
            services.AddTransient<ClientRegisterViewModel>();
            services.AddTransient<ClientResultViewModel>();
            services.AddTransient<ClientCheckCodeViewModel>();
            services.AddTransient<ViewModels.WorkersViewModel>();


            //Services/////////////////////////////////


            services.AddTransient<AllDbContext>();


            services.AddSingleton<PageService>();
            services.AddSingleton<ICurrentUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<StylesService>();
            services.AddTransient<EditStylesService>();
            services.AddSingleton<OrderService>();
            services.AddTransient<IServicesModelService, ServicesModelSerivce>();
            services.AddSingleton<IEmailCodeGenerator, EmailCodeGenerator>();
            services.AddTransient<IEmailMessageSender, EmailMessageSender>();
            services.AddSingleton<IUserRegisterService, UserRegisterService>();
            services.AddTransient<IOrderParamsCalculatorService, OrderCalculatorServiceUsual>();
            services.AddTransient<PageAnimationService>();
            services.AddTransient<EmployeeForClientService>();
            services.AddTransient<ViewModels.ClientOrdersViewModel>();
            services.AddSingleton<MVVM_Core.EventBus>();
            services.AddTransient<IClientOrders, ClientOrders>();


            Services = services.BuildServiceProvider();
        }
        public  MainViewModel MainViewModel => Services.GetRequiredService<MainViewModel>();
        public  LoginViewModel LoginViewModel => Services.GetRequiredService<LoginViewModel>();
        public  ClientHomeViewModel UserHomeViewModel => Services.GetRequiredService<ClientHomeViewModel>();
        public  StylesViewModel StylesViewModel => Services.GetRequiredService<StylesViewModel>();
        public  EditStylesViewModel EditStylesViewModel => Services.GetRequiredService<EditStylesViewModel>();
        public  DesignParamsViewModel DesignParamsViewModel => Services.GetRequiredService<DesignParamsViewModel>();
        public  OrderConfirmViewModel OrderConfirmViewModel => Services.GetRequiredService<OrderConfirmViewModel>();
        public  ServicesViewModel ServicesViewModel => Services.GetRequiredService<ServicesViewModel>();
        public  OtherParamsViewModel OtherParamsViewModel => Services.GetRequiredService<OtherParamsViewModel>();
        public  ClientRegisterViewModel ClientRegisterViewModel => Services.GetRequiredService<ClientRegisterViewModel>();
        public  ClientResultViewModel ClientResultViewModel => Services.GetRequiredService<ClientResultViewModel>();
        public  ClientCheckCodeViewModel ClientCheckCodeViewModel => Services.GetRequiredService<ClientCheckCodeViewModel>();
        public  WorkersViewModel WorkersViewModel => Services.GetRequiredService<WorkersViewModel>();
        public  ClientOrdersViewModel ClientOrdersViewModel => Services.GetRequiredService<ClientOrdersViewModel>();
    }
}
