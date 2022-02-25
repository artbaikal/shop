using DBAcess.Entityes;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace shop.ViewModels
{
    class AddEmpToDepartmentViewModel:ViewModel
    {
        public event EventHandler<EventArgs<bool>> Complete;

        


        private Employee[] _selectedEmpl;

        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees { get => _Employees; set => Set(ref _Employees, value); }

        #region SelectedEmployee : Employee - Выбранный сотрудник

        /// <summary>Выбранный сотрудник</summary>
        private Employee _SelectedEmployee;

        /// <summary>Выбранный сотрудник</summary>
        public Employee SelectedEmployee { get => _SelectedEmployee; set => Set(ref _SelectedEmployee, value); }

        #endregion


        #region Command CommitCommand - Принять изменения

        /// <summary>Принять изменения</summary>
        private ICommand _CommitCommand;

        /// <summary>Принять изменения</summary>
        public ICommand CommitCommand => _CommitCommand
            ??= new LambdaCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        /// <summary>Проверка возможности выполнения - Принять изменения</summary>
        private bool CanCommitCommandExecute(object p) => SelectedEmployee !=null;

        /// <summary>Логика выполнения - Принять изменения</summary>
        private void OnCommitCommandExecuted(object p)
        {


            _selectedEmpl[0] = SelectedEmployee;


            Complete?.Invoke(this, true);
        }

        #endregion



        #region Command CancelCommand - Принять изменения

        /// <summary>Принять изменения</summary>
        private ICommand _CancelCommand;

        /// <summary>Принять изменения</summary>
        public ICommand CancelCommand => _CancelCommand
            ??= new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        /// <summary>Проверка возможности выполнения - Принять изменения</summary>
        private bool CanCancelCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Принять изменения</summary>
        private void OnCancelCommandExecuted(object p)
        {

            Complete?.Invoke(this, false);
        }

        #endregion

        public AddEmpToDepartmentViewModel()
        {

        }
        public AddEmpToDepartmentViewModel(Employee[] emlp, Employee[] selectedEmpl)
        {

            _Employees = new ObservableCollection<Employee>(emlp);
            _selectedEmpl = selectedEmpl;
        }
    }
}
