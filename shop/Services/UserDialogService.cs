using DBAcess.Entityes;
using DBInterfaces;
using shop.Services.Interfaces;
using shop.ViewModels;
using shop.Views.Windows;
using System.Windows;


namespace shop.Services
{
    internal class UserDialogService : IUserDialog
    {
        public bool Edit(Employee empl)
        {


            return false;
        }

        

        public bool ConfirmInformation(string Information, string Caption) => MessageBox
           .Show(
                Information, Caption, 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Information)
                == MessageBoxResult.Yes;

        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
           .Show(
                Warning, Caption, 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Warning)
                == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
           .Show(
                Error, Caption, 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Error)
                == MessageBoxResult.Yes;

        public bool Edit(Department dep)
        {
            return false;


        }

        public bool Edit(Employee[] emlp,Employee[] selectedEmpl)
        {
            
            var view_model = new AddEmpToDepartmentViewModel(emlp,selectedEmpl);
            var view = new AddEmpToDepartment
            {
                DataContext = view_model,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += (_, p) =>
            {
                view.DialogResult = p.Argument;
                view.Close();
            };

            return view.ShowDialog() ?? false;
        }

        public bool Edit(int mode, Department dep, IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)
        {
            //public EditDepartmentViewModel(int mode, Department dep, IRepository<Employee> EmployeeRepository, IUserDialog UserDialog)
            var view_model = new EditDepartmentViewModel(mode, dep, EmployeeRepository, UserDialog);
            var view = new EditDepartmenWindow
            {
                DataContext = view_model,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += (_, p) =>
            {
                view.DialogResult = p.Argument;
                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
    }
}
