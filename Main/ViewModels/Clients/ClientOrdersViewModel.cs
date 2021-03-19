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

    public class ClientOrdersViewModel: MVVM_Core.BaseViewModel
    {
        private readonly IClientOrders clientOrders;
        private readonly PageService pageService;

        public ObservableCollection<Components.OrderItem> Orders { get; set; }

        public bool AnimationVisible { get; set; }

        public ClientOrdersViewModel(IClientOrders clientOrders, PageService pageService)
        {
            this.clientOrders = clientOrders;
            this.pageService = pageService;
            Init();
        }

        public ICommand BackCommand => new Command(x =>
        {
            pageService.ChangePage<Pages.ClientHomePage>();
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