using Main.MVVM_Core;
using System;
using BL;
using DAL.Models;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace Main.ViewModels
{
    public class WorkersViewModel: BaseViewModel
    {

        int _currentEmployee;
        private readonly PageAnimationService animationService;
        private readonly EmployeeForClientService empService;

        public WorkersViewModel(PageAnimationService animationService, EmployeeForClientService employeeForClientService)
        {
            this.animationService = animationService;
            this.empService = employeeForClientService;
            Init();
        }

        async void Init()
        {
            Employees = new ObservableCollection<Employee>(await empService.GetEmployees());
            if (Employees.Count > 0)
            {
                _currentEmployee = 0;
                ToEmployee();
            }
        }

        Random rand = new Random();

        async void PageChanged(Page page, AnimateTo animate)
        {
            if (CurrentPage != null)
            {
                await animationService.fadeOut(CurrentPage.Content as UIElement);
            }
            Employee = Employees[_currentEmployee];

            //animationService.SetStart(page.Content as UIElement, animate, 600);

            Angle = rand.Next(-15, 15);
            Console.WriteLine((CurrentPage?.Content as UIElement)?.Opacity);
            CurrentPage = page;

            if (animate != AnimateTo.None)
            {
                animationService.Play(CurrentPage.Content as UIElement, animate, 600);
            }
            else
            {
                await animationService.fadeIn(CurrentPage.Content as UIElement);
            }
        }

        public double Angle { get; set; }
        void ToEmployee(bool? toLeft = null)
        {

            var page = new Pages.WorkerFormPage();
            page.DataContext = this;
            (page.Content as UIElement).Opacity = 0;

            AnimateTo animate = AnimateTo.None;

            if (toLeft.HasValue)
            {
                animate = toLeft.Value ? AnimateTo.Left : AnimateTo.Rigth;
            }

            PageChanged(page, animate);
        }

        void NextEmployee()
        {
            if (Employees.Count > _currentEmployee + 1)
            {
                _currentEmployee++;
                ToEmployee(true);                
            }            
        }

        void PrevEmployee()
        {
            if (_currentEmployee > 0)
            {
                _currentEmployee--;
                ToEmployee(false);
            }
        }

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public Page CurrentPage { get; set; }
        public Employee Employee { get; set; }

        public ICommand Next => new Command(x =>
        {
            NextEmployee();


        }, x => Employees.Count > _currentEmployee + 1);

        public ICommand Back => new Command(x =>
        {
            PrevEmployee();

        }, x => _currentEmployee > 0);
    }
}