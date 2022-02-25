using DBAcess.Entityes;
using DBInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathCore.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using shop.Services.Interfaces;

namespace shop.ViewModels
{


    class DepartmentsViewModel : ViewModel
    {

        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IRepository<Department> _DepartmentsRepo;
        private readonly IUserDialog _UserDialog;
        private ObservableCollection<Department> _Departments;
        public ObservableCollection<Department> Departments { get => _Departments; set => Set(ref _Departments, value); }
        
        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees { get => _Employees; set => Set(ref _Employees, value); }


        #region SelectedDepartment : Department - Выбранный отдел

        /// <summary>Выбранный отдел</summary>
        private Department _SelectedDepartment;

        /// <summary>Выбранный отдел</summary>
        public Department SelectedDepartment { get => _SelectedDepartment; set => Set(ref _SelectedDepartment, value); }

        #endregion

        #region SelectedEmployee : Employee - Выбранный сотрудник

        /// <summary>Выбранный сотрудник</summary>
        private Employee _SelectedEmployee;

        /// <summary>Выбранный сотрудник</summary>
        public Employee SelectedEmployee { get => _SelectedEmployee; set => Set(ref _SelectedEmployee, value); }

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

            Departments = new ObservableCollection<Department>(await _DepartmentsRepo.Items.ToArrayAsync());
            

        }

        #endregion

        #region Command RemoveEmployeeCommand - Удаление сотрудника

        /// <summary>Удаление сотрудника</summary>
        private ICommand _RemoveEmployeeCommand;

        /// <summary>Удаление сотрудника</summary>
        public ICommand RemoveEmployeeCommand => _RemoveEmployeeCommand
            ??= new LambdaCommand(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление сотрудника</summary>
        private bool CanRemoveEmployeeCommandExecute(object p) => 
             p is Employee employee
            && SelectedDepartment != null
            && SelectedDepartment.Employees.Contains(employee);

        /// <summary>Логика выполнения - Удаление сотрудника</summary>
        private void OnRemoveEmployeeCommandExecuted(object p)
        {
            var dep = SelectedDepartment;
            dep.Employees.Remove((Employee)p);
            
            Employees.Remove(SelectedEmployee);

            //SelectedDepartment = null;
            //SelectedDepartment = dep;
        }

        #endregion

        #region Command NewDepartmentSelected

        /// <summary>Удаление сотрудника</summary>
        private ICommand _NewDepartmentSelectedCommand;

        /// <summary>Удаление сотрудника</summary>
        public ICommand NewDepartmentSelectedCommand => _NewDepartmentSelectedCommand
            ??= new LambdaCommand(OnNewDepartmentSelectedCommandExecuted, CanNewDepartmentSelectedCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление сотрудника</summary>
        private bool CanNewDepartmentSelectedCommandExecute(object p) => true;


        /// <summary>Логика выполнения - Удаление сотрудника</summary>
        private void OnNewDepartmentSelectedCommandExecuted(object p)
        {
            Employees = new ObservableCollection<Employee>(SelectedDepartment.Employees);

            Employees = new ObservableCollection<Employee>(_EmployeeRepository.Items.ToArray());

        }

        #endregion

        #region Command AddEmployeeCommand 
        
        private ICommand _AddEmployeeCommand;

        
        public ICommand AddEmployeeCommand => _AddEmployeeCommand
            ??= new LambdaCommand(OnAddEmployeeCommandExecuted, CanAddEmployeeCommandExecute);

        
        private bool CanAddEmployeeCommandExecute(object p) => true;

        
        private void OnAddEmployeeCommandExecuted(object p)
        {

            var depart = (Department)p;
            
            //var allEmplsWithEmptyDepartment=_DepartmentsRepo.Items.Where(x=>x.Employees)
            if (_UserDialog.Edit(depart))
            {
                // Сохранить employee в БД
                // Обновить состояние интерфейса
                Employees = new ObservableCollection<Employee>(SelectedDepartment.Employees);
                SelectedEmployee = null;

            }
            else
            {
                // Ничего не делаем
            }

        }

        #endregion

        public DepartmentsViewModel(IRepository<Employee> EmployeeRepository, IRepository<Department> departments, IUserDialog UserDialog)
        {
            _DepartmentsRepo = departments;
            _UserDialog = UserDialog;
            _EmployeeRepository = EmployeeRepository;



        }
    }
}
