namespace Main.ViewModels
{
    using BL;
    using DAL;
    using DAL.Models;
    using Main.MVVM_Core;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows;

    public class ClientOrdersViewModel: BaseViewModel
    {
        private readonly IClientOrders clientOrders;
        private readonly PageService pageService;
        private readonly EventBus bus;

        public ObservableCollection<Components.OrderItem> Orders { get; set; }

        public bool AnimationVisible { get; set; }

        public ClientOrdersViewModel(IClientOrders clientOrders, PageService pageService, EventBus bus)
        {
            this.clientOrders = clientOrders;
            this.pageService = pageService;
            this.bus = bus;
            Init();
        }

        public ICommand BackCommand => new Command(x =>
        {
            pageService.ChangePage<Pages.ClientHomePage>();
        });

        public ICommand DeleteOrder => new CommandAsync(async x =>
        {
            

            if(x is ContentControl control && control.Content is Components.OrderItem item)
            {
                if (MessageBox.Show("Оменить заказ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string serviceName = item.Order.Service.Name;

                    await clientOrders.RemoveOrder(item.Order);
                    await bus.Publish(new MVVM_Core.Events.OrderCompleted(serviceName, 1));
                    Init();                    
                }
            }
        });

        public Components.OrderItem SelectedOrderItem { get; set; }

        public bool IsSelected => SelectedOrderItem != null;

        async void Init()
        {
            AnimationVisible = true;

            var orders = (await clientOrders.GetOrdersAsync()).ToList();

            orders = orders.OrderBy(x => x.StartWorkingDate).ToList();

            Orders = new ObservableCollection<Components.OrderItem>(orders.Select(x => new Components.OrderItem(x)));
            AnimationVisible = false;
        }

    }
}