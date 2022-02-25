using DBAcess.Entityes;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace shop.ViewModels
{
    class AddEmpToDepartmentViewModel:ViewModel
    {
        public event EventHandler<EventArgs<bool>> Complete;

        private readonly Department _Department;

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
        private bool CanCommitCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Принять изменения</summary>
        private void OnCommitCommandExecuted(object p)
        {
            


            Complete?.Invoke(this, true);
        }

        #endregion

        public AddEmpToDepartmentViewModel()
        {

        }
        public AddEmpToDepartmentViewModel(Department Department)
        {
            _Department = Department;

        }
    }
}
