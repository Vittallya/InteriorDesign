﻿using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<UserHomeViewModel>();




            //Services/////////////////////////////////


            services.AddTransient<AllDbContext>();


            services.AddSingleton<PageService>();
            services.AddSingleton<ICurrentUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();


            



            Services = services.BuildServiceProvider();
        }
        public  MainViewModel MainViewModel => Services.GetRequiredService<MainViewModel>();
        public  LoginViewModel LoginViewModel => Services.GetRequiredService<LoginViewModel>();
        public  UserHomeViewModel UserHomeViewModel => Services.GetRequiredService<UserHomeViewModel>();
    }
}
