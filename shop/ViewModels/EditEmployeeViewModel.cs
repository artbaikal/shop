using DBAcess.Entityes;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using shop.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace shop.ViewModels
{

    public class sexTypes
    {
        public int id { get; set; }
        public string name { get; set; }

    }

    class EditEmployeeViewModel : ViewModel
    {

        public event EventHandler<EventArgs<bool>> Complete;
        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Редактирование сотрудника";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion
        private readonly IUserDialog _UserDialog;

        private Employee _Employee;
        public Employee Employee { get => _Employee; set => Set(ref _Employee, value); }


        private ObservableCollection<sexTypes> _SexEmumAll;
        public ObservableCollection<sexTypes> SexEmumAll { get => _SexEmumAll; set => Set(ref _SexEmumAll, value); }

        public sexTypes[] SexEmumAllArray = new sexTypes[] { new sexTypes { id = 0, name = "..." }, new sexTypes { id = 1, name = "М" },
            new sexTypes { id = 2, name = "Ж" } };


        private sexTypes _SexEmumItem;
        public sexTypes SexEmumItem { get => _SexEmumItem; set => Set(ref _SexEmumItem, value); }


        #region Command CommitCommand


        private ICommand _CommitCommand;


        public ICommand CommitCommand => _CommitCommand
            ??= new LambdaCommand(OnCommitCommandExecuted, CanCommitCommandExecute);


        private bool CanCommitCommandExecute(object p) => true;


        private void OnCommitCommandExecuted(object p)
        {


            if (Employee.Name == null || Employee.Name?.Trim().Length < 3)
            {
                MessageBox.Show("Длина Имени должна быть больше 3 символов");
                return;
            }

            if (_SexEmumItem.id==0)
            {
                Employee.Sex = EnumSex.Default;
            }
            else
            if (_SexEmumItem.id == 1)
            {
                Employee.Sex = EnumSex.Male;
            }
            else
            if (_SexEmumItem.id == 2)
            {
                Employee.Sex = EnumSex.Female;
            }

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

        public EditEmployeeViewModel(int mode, Employee dep, IUserDialog UserDialog)
        {

            _SexEmumAll = new(SexEmumAllArray);

            _UserDialog = UserDialog;
            if (mode == 0)
            {
                _Title = "Добавление нового сотрудника";
                dep.Birthday = Convert.ToDateTime("01.01.2000");
            }
            if(dep.Sex==EnumSex.Default)
            {
                _SexEmumItem = SexEmumAll[0];
            } else
            if (dep.Sex == EnumSex.Female)
            {
                _SexEmumItem = SexEmumAll[2];
            } else
            if (dep.Sex == EnumSex.Male)
            {
                _SexEmumItem = SexEmumAll[1];
            }

            _Employee = dep;
        }
        public EditEmployeeViewModel()
        {

        }

    }
}
