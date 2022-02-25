using DBAcess.Entityes;
using DBInterfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace shop.ViewModels
{
    class EditDepartmentViewModel : ViewModel
    {
        public event EventHandler<EventArgs<bool>> Complete;
        private readonly IRepository<Employee> _EmployeeRepository;
        private readonly IUserDialog _UserDialog;

        //Name
        //    HeadEmpl

        #region  department

        
        private Department _Department;

        
        public Department  Department { get => _Department; set => Set(ref _Department, value); }

        #endregion

        #region HeadEmpl department


        private Employee _HeadEmpl;
        private string _HeadEmplName;


        public Employee HeadEmpl { get => _HeadEmpl; set => Set(ref _HeadEmpl, value); }
        public string HeadEmplName { get => _HeadEmplName; set => Set(ref _HeadEmplName, value); }



        #endregion

        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Редактирование подразделения";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion


        #region Command SelectHeadEmplCommand


        private ICommand _SelectHeadEmplCommand;


        public ICommand SelectHeadEmplCommand => _SelectHeadEmplCommand
            ??= new LambdaCommand(OnSelectHeadEmplCommandExecuted, CanSelectHeadEmplCommandExecute);


        private bool CanSelectHeadEmplCommandExecute(object p) => true;


        private void OnSelectHeadEmplCommandExecuted(object p)
        {
            

            var allEmpls = _EmployeeRepository.Items.ToArray();
            var selectedEmpl = new Employee[1];
            if (_UserDialog.Edit(allEmpls, selectedEmpl))
            {

                // Зафиксировать выбор
                HeadEmpl = selectedEmpl[0];
                Department.Head= selectedEmpl[0];
                HeadEmplName = HeadEmpl.ToString();

            }
            else
            {
                // Ничего не делаем
                
            }

        }

        #endregion
        #region Command CommitCommand


        private ICommand _CommitCommand;


        public ICommand CommitCommand => _CommitCommand
            ??= new LambdaCommand(OnCommitCommandExecuted, CanCommitCommandExecute);


        private bool CanCommitCommandExecute(object p) => true;


        private void OnCommitCommandExecuted(object p)
        {
            


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

        public EditDepartmentViewModel(int mode,Department dep, IRepository<Employee> EmployeeRepository,IUserDialog UserDialog)
        {
            _UserDialog = UserDialog;
            _EmployeeRepository = EmployeeRepository;
            _Department = dep;
            if(mode==0)
            {
                _Title = "Добавление нового подразделения";
            }
            _HeadEmpl = dep?.Head;
            _HeadEmplName = _HeadEmpl.ToString();
        }
        public EditDepartmentViewModel()
        {

        }
    }
}
