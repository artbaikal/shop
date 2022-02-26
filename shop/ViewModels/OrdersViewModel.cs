using DBAcess.Entityes;
using DBInterfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace shop.ViewModels
{
    class OrdersViewModel:ViewModel
    {
        private readonly IRepository<Order> _OrderRepository;
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IUserDialog _UserDialog;


        private ObservableCollection<Order> _Orders = new();
        public ObservableCollection<Order> Orders { get => _Orders; set => Set(ref _Orders, value); }

        #region SelectedOrder

        /// <summary>Выбранный сотрудник</summary>
        private Order _SelectedOrder;

        /// <summary>Выбранный сотрудник</summary>
        public Order SelectedOrder { get => _SelectedOrder; set => Set(ref _SelectedOrder, value); }

        #endregion

        #region Command LoadDataCommand - Команда загрузки данных из репозитория

        /// <summary>Команда загрузки данных из репозитория</summary>
        private ICommand _LoadDataCommand;

        /// <summary>Команда загрузки данных из репозитория</summary>
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        /// <summary>Проверка возможности выполнения - Команда загрузки данных из репозитория</summary>
        private bool CanLoadDataCommandExecute() => true;

        /// <summary>Логика выполнения - Команда загрузки данных из репозитория</summary>
        private async Task OnLoadDataCommandExecuted()
        {

            Orders = new ObservableCollection<Order>(await _OrderRepository.Items.ToArrayAsync());


        }

        #endregion

        #region Command OrderDelete

        /// <summary>Удаление сотрудника</summary>
        private ICommand _OrderDeleteCommand;

        /// <summary>Удаление сотрудника</summary>
        public ICommand OrderDeleteCommand => _OrderDeleteCommand
            ??= new LambdaCommand(OnOrderDeleteCommandExecuted, CanOrderDeleteCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление сотрудника</summary>
        private bool CanOrderDeleteCommandExecute(object p) => SelectedOrder != null;




        private void OnOrderDeleteCommandExecuted(object p)
        {
            string messageBoxText = "Удалить выбранный заказ? ";
            string caption = "Удаленее заказа";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                var index = Orders.IndexOf(SelectedOrder);

                try
                {
                    _OrderRepository.Remove(SelectedOrder.Id);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex?.InnerException?.Message);
                    return;
                }
                Orders.Remove(SelectedOrder);

                int tmp = 0;

                try
                {
                    if (index < Orders.Count)
                        tmp = index;
                    else
                    {
                        tmp = index - 1;
                    }
                    SelectedOrder = Orders[tmp];
                }
                catch
                {
                    if (Orders?.Count > 0)
                    {
                        SelectedOrder = Orders[0];
                    }
                    else
                    {
                        SelectedOrder = null;
                    }

                }
            }



        }

        #endregion

        #region Command OrderAdd


        private ICommand _OrderAddCommand;


        public ICommand OrderAddCommand => _OrderAddCommand
            ??= new LambdaCommand(OnOrderAddCommandExecuted, CanOrderAddCommandExecute);


        private bool CanOrderAddCommandExecute(object p) => true;




        private void OnOrderAddCommandExecuted(object p)
        {

            var tmpdep = new Order();


            if (tmpdep == null) return;
            if (_UserDialog.Edit(0, tmpdep, _EmployeeRepository,  _UserDialog))
            {

                // Сохранить  в БД

                _OrderRepository.Add(tmpdep);

                // Обновить состояние интерфейса

                Orders.Add(tmpdep);


                SelectedOrder = tmpdep;




            }
            else
            {
                // Ничего не делаем

            }



        }

        #endregion

        #region Command OrderEdit


        private ICommand _OrderEditCommand;


        public ICommand OrderEditCommand => _OrderEditCommand
            ??= new LambdaCommand(OnOrderEditCommandExecuted, CanOrderEditCommandExecute);


        private bool CanOrderEditCommandExecute(object p) => SelectedOrder != null;




        private void OnOrderEditCommandExecuted(object p)
        {


            //public EditOrderViewModel(int mode, Order dep, IRepository<Order> OrderRepository, IUserDialog UserDialog)

            var tmpdep = _OrderRepository.Items.Where(x => x.Id == SelectedOrder.Id).FirstOrDefault();


            if (tmpdep == null) return;

            if (_UserDialog.Edit(1, tmpdep,_EmployeeRepository, _UserDialog))
            {


                SelectedOrder.Employee = tmpdep.Employee;
                SelectedOrder.Product = tmpdep.Product;




                // Сохранить  в БД


                _OrderRepository.Update(SelectedOrder);

                // Обновить состояние интерфейса
                OnPropertyChanged("SelectedOrder");
                var tmpempl = SelectedOrder;
       
                var deps = Orders;

                Orders = null;
                Orders = deps;

                SelectedOrder = null;
                SelectedOrder = tmpempl;



            }
            else
            {
                // Ничего не делаем

            }


        }

        #endregion


        public OrdersViewModel(IRepository<Employee> EmployeeRepository, IRepository<Order> OrderRepository, IUserDialog UserDialog)
        {
            
            _UserDialog = UserDialog;
            _OrderRepository = OrderRepository;
            _EmployeeRepository = EmployeeRepository;


        }

        public OrdersViewModel()
        {

        }

    }

}
