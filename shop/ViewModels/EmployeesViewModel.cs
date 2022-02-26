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
    class EmployeesViewModel:ViewModel
    {
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IUserDialog _UserDialog;


        private ObservableCollection<Employee> _Employees = new();
        public ObservableCollection<Employee> Employees { get => _Employees; set => Set(ref _Employees, value); }

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

            Employees = new ObservableCollection<Employee>(await _EmployeeRepository.Items.ToArrayAsync());


        }

        #endregion

        #region Command EmployeeDelete

        /// <summary>Удаление сотрудника</summary>
        private ICommand _EmployeeDeleteCommand;

        /// <summary>Удаление сотрудника</summary>
        public ICommand EmployeeDeleteCommand => _EmployeeDeleteCommand
            ??= new LambdaCommand(OnEmployeeDeleteCommandExecuted, CanEmployeeDeleteCommandExecute);

        /// <summary>Проверка возможности выполнения - Удаление сотрудника</summary>
        private bool CanEmployeeDeleteCommandExecute(object p) => SelectedEmployee != null;




        private void OnEmployeeDeleteCommandExecuted(object p)
        {
            string messageBoxText = "Удалить выбранного сотрудника? ";
            string caption = "Удаленее сотрудника";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                var index = Employees.IndexOf(SelectedEmployee);

                try
                {
                    _EmployeeRepository.Remove(SelectedEmployee.Id);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex?.InnerException?.Message);
                    return;
                }
                Employees.Remove(SelectedEmployee);

                int tmp = 0;

                try
                {
                    if (index < Employees.Count)
                        tmp = index;
                    else
                    {
                        tmp = index - 1;
                    }
                    SelectedEmployee = Employees[tmp];
                }
                catch
                {
                    if (Employees?.Count > 0)
                    {
                        SelectedEmployee = Employees[0];
                    }
                    else
                    {
                        SelectedEmployee = null;
                    }

                }
            }



        }

        #endregion

        #region Command EmployeeAdd


        private ICommand _EmployeeAddCommand;


        public ICommand EmployeeAddCommand => _EmployeeAddCommand
            ??= new LambdaCommand(OnEmployeeAddCommandExecuted, CanEmployeeAddCommandExecute);


        private bool CanEmployeeAddCommandExecute(object p) => true;




        private void OnEmployeeAddCommandExecuted(object p)
        {

            var tmpdep = new Employee();


            if (tmpdep == null) return;
            if (_UserDialog.Edit(0, tmpdep,  _UserDialog))
            {

                // Сохранить  в БД

                _EmployeeRepository.Add(tmpdep);

                // Обновить состояние интерфейса

                Employees.Add(tmpdep);


                SelectedEmployee = tmpdep;




            }
            else
            {
                // Ничего не делаем

            }



        }

        #endregion

        #region Command EmployeeEdit


        private ICommand _EmployeeEditCommand;


        public ICommand EmployeeEditCommand => _EmployeeEditCommand
            ??= new LambdaCommand(OnEmployeeEditCommandExecuted, CanEmployeeEditCommandExecute);


        private bool CanEmployeeEditCommandExecute(object p) => SelectedEmployee != null;




        private void OnEmployeeEditCommandExecuted(object p)
        {


            //public EditEmployeeViewModel(int mode, Employee dep, IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)

            var tmpdep = _EmployeeRepository.Items.Where(x => x.Id == SelectedEmployee.Id).FirstOrDefault();


            if (tmpdep == null) return;

            if (_UserDialog.Edit(1, tmpdep, _UserDialog))
            {
                SelectedEmployee.Name = tmpdep.Name;
                SelectedEmployee.Surname = tmpdep.Surname;
                SelectedEmployee.Patronymic = tmpdep.Patronymic;
                SelectedEmployee.Sex = tmpdep.Sex;
                SelectedEmployee.Birthday = tmpdep.Birthday;




                // Сохранить  в БД


                _EmployeeRepository.Update(SelectedEmployee);

                // Обновить состояние интерфейса
                OnPropertyChanged("SelectedEmployee");
                var tmpempl = SelectedEmployee;
       
                var deps = Employees;

                Employees = null;
                Employees = deps;

                SelectedEmployee = null;
                SelectedEmployee = tmpempl;



            }
            else
            {
                // Ничего не делаем

            }


        }

        #endregion


        public EmployeesViewModel(IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)
        {
            
            _UserDialog = UserDialog;
            _EmployeeRepository = EmployeeRepository;



        }

        public EmployeesViewModel()
        {

        }

    }

}
