using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL;
using DAL.Models;
using Main.MVVM_Core;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Main.Pages;

namespace Main.ViewModels
{
    public class StylesViewModel: BasePageViewModel
    {
        private readonly StylesService service;
        private readonly PageService pageService;
        private readonly DesignParamsService designParams;

        public ObservableCollection<Style> Styles { get; set; }

        public Style Selected { get; set; }

        public StylesViewModel(StylesService service, PageService pageService, DesignParamsService designParams):base(pageService)
        {
            this.service = service;
            this.pageService = pageService;
            this.designParams = designParams;
            Init();
        }

        async void Init()
        {
            Styles = await service.GetStyles();
        }


        public ICommand NextPage => new Command(x =>
        {
            designParams.Order.Style = Selected;
            pageService.ChangePage<DesignParamsPage>(AnimateTo.Left);

        }, y => Selected != null);
    }
}
