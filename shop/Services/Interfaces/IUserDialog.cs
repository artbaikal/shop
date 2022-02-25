

using DBAcess.Entityes;

namespace shop.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool Edit(Department dep);
        bool Edit(Employee empl);

        bool Edit(Employee[] emlp, Employee[] selectedEmpl ) ;

        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
    }
}
