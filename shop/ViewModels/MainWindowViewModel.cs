using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DBAcess.Entityes;
using DBInterfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using shop.Services.Interfaces;

namespace shop.ViewModels
{
    class MainWindowViewModel : ViewModel
    {

        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Department> _DepartmentRepository;
        private readonly IRepository<Order> _OrderRepository;
        private readonly IUserDialog _UserDialog;

        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Управление сотрудниками";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region CurrentModel : ViewModel - Текущая дочерняя модель-представления

        /// <summary>Текущая дочерняя модель-представления</summary>
        private ViewModel _CurrentModel;

        /// <summary>Текущая дочерняя модель-представления</summary>
        public ViewModel CurrentModel { get => _CurrentModel; private set => Set(ref _CurrentModel, value); }

        #endregion

        #region Command ShowDepartmentsViewCommand - Отобразить представление подразделений

        /// <summary>Отобразить представление книг</summary>
        private ICommand _ShowDepartmentsViewCommand;

        /// <summary>Отобразить представление книг</summary>
        public ICommand ShowDepartmentsViewCommand => _ShowDepartmentsViewCommand
            ??= new LambdaCommand(OnShowDepartmentsViewCommandExecuted, CanShowDepartmentsViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление книг</summary>
        private bool CanShowDepartmentsViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление книг</summary>
        private void OnShowDepartmentsViewCommandExecuted()
        {
            if(CurrentModel is DepartmentsViewModel)
            {
                return;
            }
            CurrentModel = new DepartmentsViewModel(_EmployeeRepository,_DepartmentRepository, _UserDialog);
        }

        #endregion

        public MainWindowViewModel(IRepository<Employee> EmployeeRepository,
            IRepository<Department> DepartmentRepository,
            IRepository<Order> OrderRepository,
            IUserDialog UserDialog
            )
        {
            _EmployeeRepository = EmployeeRepository;
            _DepartmentRepository = DepartmentRepository;
            _OrderRepository = OrderRepository;
            _UserDialog = UserDialog;



            //var employees = EmployeeRepository.Items.Take(1).ToArray();

            //var departments = DepartmentRepository.Items.Take(10).ToArray();
            //var orders = OrderRepository.Items.Take(10).ToArray();

            //OrderRepository.Remove(orders[0].Id);

        }

    }


}
