using BL;
using DAL;
using DAL.Models;
using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.ViewModels.Admin
{
    public class AdminViewModel : BasePageViewModel
    {
        private readonly AllDbContext dbContext;

        public AdminViewModel(PageService pageservice, AllDbContext dbContext) : base(pageservice )
        {
            this.dbContext = dbContext;
            Init();
        }

        async void Init()
        {
            await dbContext.Orders.LoadAsync();
            Orders = dbContext.Orders.Local;
        }

        public ICommand ShowDog => new Command(x =>
        {
            if (x is Order o)
            {
                var w = new Windwos.DogovorWindow();
                var dc = new DogovorViewModel
                {
                    Address = o.Address,
                    Area = o.OrderDetail?.Area ?? 0,
                    Customer = o.Client.Name,
                    Date = o.CreationDate,
                    DogovorNumber = o.Id,
                    Services = o.Services.Select(y => y.Name).ToArray(),
                };
                w.DataContext = dc;

                w.ShowDialog();

            }
        });

        public ICommand ShowDetails => new Command(x =>
        {
            if(x is Order o)
            {

            }
        });

        public ObservableCollection<Order> Orders { get; set; }

        public override int PoolIndex => Rules.Pages.ADMIN;
    }
}
