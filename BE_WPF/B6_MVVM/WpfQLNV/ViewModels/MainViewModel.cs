using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WpfQLNV.Models;
using System.Windows;

namespace WpfQLNV.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            // Khởi tạo danh sách phòng ban
            Departments = new ObservableCollection<Department>();
            Departments.Add(new Department { Name = "Giám đốc" });
            Departments.Add(new Department { Name = "Tổ chức hành chính" });
            Departments.Add(new Department { Name = "Kế hoạch" });
            Departments.Add(new Department { Name = "Kế Toán" });

            // Khởi tạo Command
            AddEmployeeCommand = new RelayCommand(AddEmployeeExecute, CanAddEmployeeExecute);
            AddDepartmentCommand = new RelayCommand(AddDepartmentExecute, CanAddDepartmentExecute);
            RemoveDepartmentCommand = new RelayCommand(RemoveDepartmentExecute, CanRemoveDepartmentExecute);
        }

        // ===================
        // PHÒNG BAN
        // ===================

        private ObservableCollection<Department> _departments;

        // Danh sách phòng ban
        public ObservableCollection<Department> Departments
        {
            get { return _departments; }
            set { _departments = value; OnPropertyChanged("Departments"); }
        }

        private Department _selectedDepartment;

        // Phòng ban đang được chọn
        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");

                // ⚠️ Quan trọng: cập nhật lại trạng thái Button
                (AddEmployeeCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (RemoveDepartmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private string _newDepartmentName;

        // Tên phòng ban mới nhập vào
        public string NewDepartmentName
        {
            get { return _newDepartmentName; }
            set
            {
                _newDepartmentName = value;
                OnPropertyChanged("NewDepartmentName");

                // Cập nhật trạng thái button Add Department
                (AddDepartmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // ===================
        // NHÂN VIÊN
        // ===================

        private string _employeeId;

        public string EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged("EmployeeId");

                (AddEmployeeCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private string _employeeName;

        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged("EmployeeName");

                (AddEmployeeCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private string _employeeAddress;

        public string EmployeeAddress
        {
            get { return _employeeAddress; }
            set
            {
                _employeeAddress = value;
                OnPropertyChanged("EmployeeAddress");
            }
        }

        // ===================
        // COMMANDS
        // ===================

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand AddDepartmentCommand { get; set; }
        public ICommand RemoveDepartmentCommand { get; set; }


        // ===================
        // ADD EMPLOYEE
        // ===================

        // Hàm thực thi khi click Add Employee
        private void AddEmployeeExecute(object parameter)
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Thông báo");
                return;
            }

            // Kiểm tra trùng mã:Kiểm tra xem trong phòng ban hiện tại có nhân viên nào có EmployeeId trùng với EmployeeId đang nhập(không phân biệt hoa thường) không ?
            //.Any(emp (tham số) => điều_kiện) =Có ít nhất 1 phần tử thỏa điều kiện không?
            //.Any(...): có ít nhât 1 người đúng trả về T, ko có cái nào đúng trả về F
            bool isDuplicate = SelectedDepartment.Employees
                .Any(e => e.EmployeeId.Equals(EmployeeId, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại!", "Thông báo");
                return;
            }

            // Tạo nhân viên mới
            Employee emp = new Employee
            {
                EmployeeId = EmployeeId,
                FullName = EmployeeName,
                Address = EmployeeAddress
            };
            SelectedDepartment.Employees.Add(emp);

            // Clear form
            EmployeeId = string.Empty;
            EmployeeName = string.Empty;
            EmployeeAddress = string.Empty;

            // 🔥 cập nhật lại trạng thái Button
            (AddEmployeeCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        // Điều kiện để Button Add Employee được bật
        private bool CanAddEmployeeExecute(object parameter)
        {
            return SelectedDepartment != null &&
                   !string.IsNullOrWhiteSpace(EmployeeId) &&
                   !string.IsNullOrWhiteSpace(EmployeeName);
        }

        // ===================
        // ADD DEPARTMENT
        // ===================

        private void AddDepartmentExecute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewDepartmentName))
                return;

            // Kiểm tra trùng tên
            bool isDuplicate = Departments.Any(d =>
                d.Name.Equals(NewDepartmentName.Trim(), StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                MessageBox.Show("Phòng ban đã tồn tại!", "Thông báo");
                return;
            }

            // Thêm phòng ban
            Department dep = new Department
            {
                Name = NewDepartmentName.Trim()
            };
            Departments.Add(dep);

            // Clear input
            NewDepartmentName = string.Empty;

            (AddDepartmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private bool CanAddDepartmentExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewDepartmentName);
        }

        // ===================
        // REMOVE DEPARTMENT
        // ===================

        private void RemoveDepartmentExecute(object parameter)
        {
            if (SelectedDepartment == null)
                return;

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa \"{SelectedDepartment.Name}\"?",
                "Xác nhận",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Departments.Remove(SelectedDepartment);
                // 🔥 QUAN TRỌNG: reset lại selection
                SelectedDepartment = null;

                (RemoveDepartmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private bool CanRemoveDepartmentExecute(object parameter)
        {
            return SelectedDepartment != null;
        }
    }


    //public class MainViewModel : BaseViewModel
    //{
    //    public MainViewModel()
    //    {
    //        Departments = new ObservableCollection<Department>();
    //        Departments.Add(new Department("Giám đốc"));
    //        Departments.Add(new Department("Tổ chức hành chính"));
    //        Departments.Add(new Department("Kế hoạch"));
    //        Departments.Add(new Department("Kế Toán"));

    //        AddEmployeeCommand = new RelayCommand(AddEmployeeExecute, CanAddEmployeeExecute);
    //        AddDepartmentCommand = new RelayCommand(AddDepartmentExecute, CanAddDepartmentExecute);
    //        RemoveDepartmentCommand = new RelayCommand(RemoveDepartmentExecute, CanRemoveDepartmentExecute);
    //    }

    //    // ===================
    //    // Phòng ban
    //    // ===================
    //    private ObservableCollection<Department> _departments;
    //    public ObservableCollection<Department> Departments
    //    {
    //        get { return _departments; }
    //        set { _departments = value; OnPropertyChanged("Departments"); }
    //    }

    //    private Department _selectedDepartment;
    //    public Department SelectedDepartment
    //    {
    //        get { return _selectedDepartment; }
    //        set { _selectedDepartment = value; OnPropertyChanged("SelectedDepartment"); }
    //    }

    //    private string _newDepartmentName;
    //    public string NewDepartmentName
    //    {
    //        get { return _newDepartmentName; }
    //        set { _newDepartmentName = value; OnPropertyChanged("NewDepartmentName"); }
    //    }

    //    // ===================
    //    // Nhân viên
    //    // ===================
    //    private string _employeeId;
    //    public string EmployeeId
    //    {
    //        get { return _employeeId; }
    //        set { _employeeId = value; OnPropertyChanged("EmployeeId"); }
    //    }

    //    private string _employeeName;
    //    public string EmployeeName
    //    {
    //        get { return _employeeName; }
    //        set { _employeeName = value; OnPropertyChanged("EmployeeName"); }
    //    }

    //    private string _employeeAddress;
    //    public string EmployeeAddress
    //    {
    //        get { return _employeeAddress; }
    //        set { _employeeAddress = value; OnPropertyChanged("EmployeeAddress"); }
    //    }

    //    // ===================
    //    // Commands
    //    // ===================
    //    public ICommand AddEmployeeCommand { get; set; }
    //    public ICommand AddDepartmentCommand { get; set; }
    //    public ICommand RemoveDepartmentCommand { get; set; }

    //    // ===================
    //    // ADD EMPLOYEE
    //    // ===================
    //    private void AddEmployeeExecute(object parameter)
    //    {
    //        if (SelectedDepartment == null)
    //        {
    //            MessageBox.Show("Vui lòng chọn phòng ban trước khi thêm nhân viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
    //            return;
    //        }

    //        bool isDuplicate = SelectedDepartment.Employees.Any(e => e.EmployeeId.Equals(EmployeeId, StringComparison.OrdinalIgnoreCase));
    //        if (isDuplicate)
    //        {
    //            MessageBox.Show("Mã nhân viên đã tồn tại trong phòng ban!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
    //            return;
    //        }

    //        Employee emp = new Employee(EmployeeId, EmployeeName, EmployeeAddress);
    //        SelectedDepartment.Employees.Add(emp);

    //        // Clear form
    //        EmployeeId = string.Empty;
    //        EmployeeName = string.Empty;
    //        EmployeeAddress = string.Empty;
    //    }

    //    private bool CanAddEmployeeExecute(object parameter)
    //    {
    //        return SelectedDepartment != null &&
    //               !string.IsNullOrWhiteSpace(EmployeeId) &&
    //               !string.IsNullOrWhiteSpace(EmployeeName);
    //    }

    //    // ===================
    //    // ADD DEPARTMENT
    //    // ===================
    //    private void AddDepartmentExecute(object parameter)
    //    {
    //        if (string.IsNullOrWhiteSpace(NewDepartmentName))
    //            return;

    //        bool isDuplicate = Departments.Any(d => d.Name.Equals(NewDepartmentName.Trim(), StringComparison.OrdinalIgnoreCase));
    //        if (isDuplicate)
    //        {
    //            MessageBox.Show("Phòng ban đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
    //            return;
    //        }

    //        Department dep = new Department(NewDepartmentName.Trim());
    //        Departments.Add(dep);

    //        NewDepartmentName = string.Empty;
    //    }

    //    private bool CanAddDepartmentExecute(object parameter)
    //    {
    //        return !string.IsNullOrWhiteSpace(NewDepartmentName);
    //    }

    //    // ===================
    //    // REMOVE DEPARTMENT
    //    // ===================
    //    private void RemoveDepartmentExecute(object parameter)
    //    {
    //        if (SelectedDepartment == null)
    //            return;

    //        MessageBoxResult result = MessageBox.Show(
    //            $"Bạn có chắc muốn xóa phòng ban \"{SelectedDepartment.Name}\"?",
    //            "Xác nhận",
    //            MessageBoxButton.YesNo,
    //            MessageBoxImage.Question);

    //        if (result == MessageBoxResult.Yes)
    //        {
    //            Departments.Remove(SelectedDepartment);
    //        }
    //    }

    //    private bool CanRemoveDepartmentExecute(object parameter)
    //    {
    //        return SelectedDepartment != null;
    //    }


    //}

}
