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
using System.Windows;

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

            string messageBoxText = "Удалить выбранную запись сотрудника? ";
            string caption = "Удаленее сотрудника";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {

                _EmployeeRepository.Remove(SelectedEmployee.Id);
                Employees.Remove(SelectedEmployee);
                
                SelectedEmployee = null;
            }


            
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
            if (SelectedDepartment != null)
            {
                Employees = new ObservableCollection<Employee>(SelectedDepartment.Employees);
            }
            else
            {
                Employees = null;
            }


        }

        #endregion


        #region Command DepartmentDelete

        /// <summary>Удаление сотрудника</summary>
        private ICommand _DepartmentDeleteCommand;

        /// <summary>Удаление сотрудника</summary>
        public ICommand DepartmentDeleteCommand => _DepartmentDeleteCommand
            ??= new LambdaCommand(OnDepartmentDeleteCommandExecuted, CanDepartmentDeleteCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление сотрудника</summary>
        private bool CanDepartmentDeleteCommandExecute(object p) => SelectedDepartment != null;


        
        
        private void OnDepartmentDeleteCommandExecuted(object p)
        {
            string messageBoxText = "Удалить выбранное подразделение? ";
            string caption = "Удаленее подразделения";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                var index = Departments.IndexOf(SelectedDepartment);

                _DepartmentsRepo.Remove(SelectedDepartment.Id);
                Departments.Remove(SelectedDepartment);

                int tmp = 0;

                try
                {
                    if (index < Departments.Count)
                        tmp = index;
                    else
                    {
                        tmp = index - 1;
                    }
                    SelectedDepartment = Departments[tmp];
                }
                catch
                {
                    if (Departments?.Count > 0)
                    {
                        SelectedDepartment = Departments[0];
                    }
                    else
                    {
                        SelectedDepartment = null;
                    }

                }
            }

            

        }

        #endregion

        #region Command DepartmentAdd

        
        private ICommand _DepartmentAddCommand;

        
        public ICommand DepartmentAddCommand => _DepartmentAddCommand
            ??= new LambdaCommand(OnDepartmentAddCommandExecuted, CanDepartmentAddCommandExecute);

        
        private bool CanDepartmentAddCommandExecute(object p) => true;




        private void OnDepartmentAddCommandExecuted(object p)
        {
            
            



        }

        #endregion

        #region Command DepartmentEdit


        private ICommand _DepartmentEditCommand;


        public ICommand DepartmentEditCommand => _DepartmentEditCommand
            ??= new LambdaCommand(OnDepartmentEditCommandExecuted, CanDepartmentEditCommandExecute);


        private bool CanDepartmentEditCommandExecute(object p) => SelectedDepartment != null;




        private void OnDepartmentEditCommandExecuted(object p)
        {


            //public EditDepartmentViewModel(int mode, Department dep, IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)

            var tmpdep = _DepartmentsRepo.Items.Where(x => x.Id == SelectedDepartment.Id).FirstOrDefault();


            if (tmpdep == null) return;
            if (_UserDialog.Edit(1, tmpdep, _EmployeeRepository,_UserDialog ))
            {
                SelectedDepartment.Head = tmpdep.Head;
                SelectedDepartment.Name = tmpdep.Name;

                // Сохранить  в БД


                _DepartmentsRepo.Update(SelectedDepartment);

                // Обновить состояние интерфейса
                OnPropertyChanged("SelectedDepartment");
                var tmpempl = SelectedEmployee;
                var tdep = SelectedDepartment;
                var deps = Departments;

                Departments = null;
                Departments = deps;

                SelectedDepartment = tdep;
                SelectedEmployee = tmpempl;



            }
            else
            {
                // Ничего не делаем

            }


        }

        #endregion

        #region Command AddEmployeeCommand 

        private ICommand _AddEmployeeCommand;

        
        public ICommand AddEmployeeCommand => _AddEmployeeCommand
            ??= new LambdaCommand(OnAddEmployeeCommandExecuted, CanAddEmployeeCommandExecute);


        private bool CanAddEmployeeCommandExecute(object p) => SelectedDepartment != null;
        
        
        private void OnAddEmployeeCommandExecuted(object p)
        {

            var depart = (Department)p;

            var allEmplsWithEmptyDepartment = _EmployeeRepository.Items.Where(x => x.Department == null).ToArray();
            var selectedEmpl = new Employee[1];
            if (_UserDialog.Edit(allEmplsWithEmptyDepartment, selectedEmpl))
            {

                // Сохранить employee в БД
                SelectedDepartment.Employees.Add(selectedEmpl[0]);
                _DepartmentsRepo.Update(SelectedDepartment);

                Employees.Add(selectedEmpl[0]);
                // Обновить состояние интерфейса
                //Employees = new ObservableCollection<Employee>(SelectedDepartment.Employees);
                //SelectedEmployee = null;

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

        public DepartmentsViewModel()
        {

        }
    }
}
