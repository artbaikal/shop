using DBAcess.Entityes;
using DBInterfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using shop.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace shop.ViewModels
{



    class EditOrdersViewModel : ViewModel
    {
        private IRepository<Employee> _EmployeeRepository;
        public event EventHandler<EventArgs<bool>> Complete;
        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Редактирование заказа";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion
        private readonly IUserDialog _UserDialog;

        private Order _Order;
        public Order Order { get => _Order; set => Set(ref _Order, value); }

        private Employee _Empl;
        private string _EmplName;

        private string _orderNumber;
        public string orderNumber { get => _orderNumber; set => Set(ref _orderNumber, value); }

        public Employee Empl { get => _Empl; set => Set(ref _Empl, value); }
        public string EmplName { get => _EmplName; set => Set(ref _EmplName, value); }

        #region Command CommitCommand


        private ICommand _CommitCommand;


        public ICommand CommitCommand => _CommitCommand
            ??= new LambdaCommand(OnCommitCommandExecuted, CanCommitCommandExecute);


        private bool CanCommitCommandExecute(object p) => true;


        private void OnCommitCommandExecuted(object p)
        {


            if (Order.Product == null || Order.Product?.Trim().Length < 2)
            {
                MessageBox.Show("Длина Имени должна быть больше 2 символов");
                return;
            }
            int tmp;
            if(!Int32.TryParse(orderNumber, out tmp))
            {
                MessageBox.Show("Номер заказа не может быть преобразован в число");
                return;
            }
            if(Order.Employee ==null)
            {
                MessageBox.Show("Не выбран сотрудник");
                return;
            }
            Order.Number = tmp;

            Complete?.Invoke(this, true);
        }

        #endregion


        #region Command CancelCommand


        private ICommand _CancelCommand;


        public ICommand CancelCommand => _CancelCommand
            ??= new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecute);


        private bool CanCancelCommandExecute(object p) => true;


        private void OnCancelCommandExecuted(object p)
        {
            Complete?.Invoke(this, false);
        }

        #endregion


        #region Command SelectEmplCommand


        private ICommand _SelectEmplCommand;


        public ICommand SelectEmplCommand => _SelectEmplCommand
            ??= new LambdaCommand(OnSelectEmplCommandExecuted, CanSelectEmplCommandExecute);


        private bool CanSelectEmplCommandExecute(object p) => true;


        private void OnSelectEmplCommandExecuted(object p)
        {


            var allEmpls = _EmployeeRepository.Items.ToArray();
            var selectedEmpl = new Employee[1];
            if (_UserDialog.Edit(allEmpls, selectedEmpl))
            {

                // Зафиксировать выбор
                Empl = selectedEmpl[0];
                Order.Employee = selectedEmpl[0];
                EmplName = Empl.ToString();

            }
            else
            {
                // Ничего не делаем

            }

        }

        #endregion

        public EditOrdersViewModel(int mode, Order order, IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)
        {

            _EmployeeRepository = EmployeeRepository;

            _UserDialog = UserDialog;
            if (mode == 0)
            {
                _Title = "Добавление нового заказа";
                
            }

            _Order = order;
            _Empl = order?.Employee;
            _EmplName = _Empl?.ToString();

            _orderNumber = order.Number.ToString();
        }
        public EditOrdersViewModel()
        {

        }

    }
}
