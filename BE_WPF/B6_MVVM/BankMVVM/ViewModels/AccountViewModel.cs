using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankMVVM.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BankMVVM.ViewModels
{

    public class AccountViewModel : BaseViewModel
    {
        // =========================================================
        // 1. DANH SÁCH DỮ LIỆU HIỂN THỊ
        // =========================================================

        // Danh sách tài khoản hiển thị trong DataGrid
        public ObservableCollection<AccountModel> Accounts { get; set; }

        // Danh sách thành phố hiển thị trong ComboBox
        public ObservableCollection<string> Cities { get; set; }

        // =========================================================
        // 2. ACCOUNT ĐANG ĐƯỢC CHỌN TRONG DATAGRID
        // =========================================================

        private AccountModel _selectedAccount;
        public AccountModel SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;

                // báo cho UI biết property đã thay đổi
                OnPropertyChanged("SelectedAccount");

                // cập nhật trạng thái enable/disable của các button
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // =========================================================
        // 3. INPUT DỮ LIỆU TỪ UI (TextBox / ComboBox)
        // =========================================================

        private string _inputAccountNumber;
        public string InputAccountNumber
        {
            get { return _inputAccountNumber; }
            set
            {
                _inputAccountNumber = value;
                OnPropertyChanged("InputAccountNumber");
            }
        }

        private string _inputCustomerName;
        public string InputCustomerName
        {
            get { return _inputCustomerName; }
            set
            {
                _inputCustomerName = value;
                OnPropertyChanged("InputCustomerName");
            }
        }

        private string _inputAddress;
        public string InputAddress
        {
            get { return _inputAddress; }
            set
            {
                _inputAddress = value;
                OnPropertyChanged("InputAddress");
            }
        }

        private string _inputCity;
        public string InputCity
        {
            get { return _inputCity; }
            set
            {
                _inputCity = value;
                OnPropertyChanged("InputCity");
            }
        }

        private decimal _inputBalance;
        public decimal InputBalance
        {
            get { return _inputBalance; }
            set
            {
                _inputBalance = value;
                OnPropertyChanged("InputBalance");
            }
        }

        // =========================================================
        // 4. TRẠNG THÁI THÊM / SỬA
        // =========================================================

        private bool _isAdding;
        public bool IsAdding
        {
            get { return _isAdding; }
            set
            {
                _isAdding = value;

                OnPropertyChanged("IsAdding");

                // cập nhật text button
                OnPropertyChanged("AddButtonText");

                // cập nhật trạng thái button Save
                (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string AddButtonText  // CHỈ để hiển thị UI
        {
            get
            {
                if (IsAdding)
                    return "Hủy";
                else
                    return "Thêm";
            }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;

                OnPropertyChanged("IsEditing");
                OnPropertyChanged("EditButtonText");

                (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
                
            }
        }

        public string EditButtonText  //CHỈ để hiển thị UI
        {
            get
            {
                if (IsEditing)
                    return "Hủy";
                else
                    return "Sửa";
            }
        }

        // =========================================================
        // 5. COMMANDS
        // =========================================================

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        // =========================================================
        // 6. CONSTRUCTOR
        // =========================================================

        public AccountViewModel()
        {
            // khởi tạo danh sách
            Accounts = new ObservableCollection<AccountModel>();

            Cities = new ObservableCollection<string>()
        {
            "HCM",
            "HN",
            "Đà Nẵng",
            "Cần Thơ"
        };

            // load dữ liệu mẫu
            LoadSampleData();

            // khởi tạo commands
            AddCommand = new RelayCommand(AddOrCancel);                     //luôn nhấn được không cần điều kiện
            EditCommand = new RelayCommand(EditOrCancel, CanEditOrDelete); //chỉ nhấn được khi SelectedAccount != null
            SaveCommand = new RelayCommand(Save, CanSave);                  //chỉ nhấn được khi đang Add hoặc Edit
            DeleteCommand = new RelayCommand(Delete, CanEditOrDelete);      //chỉ nhấn được khi SelectedAccount != null
        }

        // =========================================================
        // 7. LOAD DỮ LIỆU MẪU
        // =========================================================

        private void LoadSampleData()
        {
            AccountModel acc = new AccountModel();

            acc.STT = 1;
            acc.AccountNumber = "001";
            acc.CustomerName = "Nguyễn Văn A";
            acc.Address = "Q1";
            acc.City = "HCM";
            acc.Balance = 1000000;

            Accounts.Add(acc);
        }

        // =========================================================
        // 8. LOGIC ADD / CANCEL
        // =========================================================

        private void AddOrCancel(object obj) //Điều khiển bắt đầu / hủy thêmn
        {
            if (!IsAdding)
            {
                // bắt đầu thêm
                ClearInput();

                IsAdding = true;
                IsEditing = false;
            }
            else
            {
                // hủy thêm
                ClearInput();
                IsAdding = false;
            }
        }

        // =========================================================
        // 9. LOGIC EDIT / CANCEL
        // =========================================================

        private void EditOrCancel(object obj) //Điều khiển bắt đầu / hủy sửa
        {
            if (!IsEditing)
            {
                if (SelectedAccount == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
                    return;
                }

                // copy dữ liệu sang input
                InputAccountNumber = SelectedAccount.AccountNumber;
                InputCustomerName = SelectedAccount.CustomerName;
                InputAddress = SelectedAccount.Address;
                InputCity = SelectedAccount.City;
                InputBalance = SelectedAccount.Balance;

                IsEditing = true;
                IsAdding = false;
            }
            else
            {
                // hủy sửa
                ClearInput();
                IsEditing = false;
            }
        }

        // =========================================================
        // 10. SAVE
        // =========================================================

        private void Save(object obj)
        {
            if (!ValidateInput())
                return;

            if (IsAdding)
            {
                AccountModel acc = new AccountModel();

                acc.STT = Accounts.Count + 1;
                acc.AccountNumber = InputAccountNumber;
                acc.CustomerName = InputCustomerName;
                acc.Address = InputAddress;
                acc.City = InputCity;
                acc.Balance = InputBalance;

                Accounts.Add(acc);
            }
            else if (IsEditing)
            {
                if (SelectedAccount == null)
                    return; 
                SelectedAccount.AccountNumber = InputAccountNumber;
                SelectedAccount.CustomerName = InputCustomerName;
                SelectedAccount.Address = InputAddress;
                SelectedAccount.City = InputCity;
                SelectedAccount.Balance = InputBalance;
            }

            UpdateSTT();
            OnPropertyChanged("TotalBalance");

            ClearInput();

            IsAdding = false;
            IsEditing = false;
            SelectedAccount = null;
        }

        // =========================================================
        // 11. DELETE
        // =========================================================

        private void Delete(object obj)
        {
            if (SelectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.");
                return;
            }

            Accounts.Remove(SelectedAccount);
            SelectedAccount = null;

            UpdateSTT();
            OnPropertyChanged("TotalBalance");

            ClearInput();
        }

        // =========================================================
        // 12. VALIDATE
        // =========================================================

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(InputAccountNumber))
            {
                MessageBox.Show("Số tài khoản không được rỗng.");
                return false;
            }

            if (string.IsNullOrEmpty(InputCustomerName))
            {
                MessageBox.Show("Tên khách hàng không được rỗng.");
                return false;
            }

            if (InputBalance < 0)
            {
                MessageBox.Show("Số tiền không được âm.");
                return false;
            }

            bool duplicate = Accounts.Any(a => a.AccountNumber == InputAccountNumber);

            if (duplicate && IsAdding)
            {
                MessageBox.Show("Trùng số tài khoản.");
                return false;
            }

            return true;
        }

        // =========================================================
        // 13. CANEXECUTE
        // =========================================================

        private bool CanSave(object obj)  //quyết định khi nào button được phép nhấn, hỉ cần một trong hai đúng thì cho phép save
        {
            return IsAdding || IsEditing;
        }

        private bool CanEditOrDelete(object obj)  //Chỉ khi user đã chọn 1 dòng trong DataGrid thì mới được sửa hoặc xóa
        {
            return SelectedAccount != null;
        }

        // =========================================================
        // 14. HELPER
        // =========================================================

        private void ClearInput()
        {
            InputAccountNumber = "";
            InputCustomerName = "";
            InputAddress = "";
            InputCity = null;
            InputBalance = 0;
        }

        private void UpdateSTT()
        {
            int i = 1;

            foreach (AccountModel acc in Accounts)
            {
                acc.STT = i;
                i++;
            }
        }

        // tổng tiền của tất cả account
        public decimal TotalBalance
        {
            get
            {
                decimal total = 0;

                foreach (AccountModel acc in Accounts)
                {
                    total += acc.Balance;
                }

                return total;
            }
        }
    }
}


//namespace BankMVVM.ViewModels
//{
//    public class AccountViewModel : BaseViewModel
//    {
//        public ObservableCollection<AccountModel> Accounts { get; set; }

//        public ObservableCollection<string> Cities { get; set; }

//        private AccountModel _selectedAccount;
//        public AccountModel SelectedAccount
//        {
//            get { return _selectedAccount; }
//            set
//            {
//                _selectedAccount = value;
//                OnPropertyChanged("SelectedAccount");
//                EditCommand.RaiseCanExecuteChanged();
//                DeleteCommand.RaiseCanExecuteChanged();
//            }
//        }

//        // Input properties cho TextBox / ComboBox
//        private string _inputAccountNumber;
//        public string InputAccountNumber
//        {
//            get { return _inputAccountNumber; }
//            set { _inputAccountNumber = value; OnPropertyChanged("InputAccountNumber"); }
//        }

//        private string _inputCustomerName;
//        public string InputCustomerName
//        {
//            get { return _inputCustomerName; }
//            set { _inputCustomerName = value; OnPropertyChanged("InputCustomerName"); }
//        }

//        private string _inputAddress;
//        public string InputAddress
//        {
//            get { return _inputAddress; }
//            set { _inputAddress = value; OnPropertyChanged("InputAddress"); }
//        }

//        private string _inputCity;
//        public string InputCity
//        {
//            get { return _inputCity; }
//            set { _inputCity = value; OnPropertyChanged("InputCity"); }
//        }

//        private decimal _inputBalance;
//        public decimal InputBalance
//        {
//            get { return _inputBalance; }
//            set { _inputBalance = value; OnPropertyChanged("InputBalance"); }
//        }

//        private bool _isAdding;
//        public bool IsAdding
//        {
//            get { return _isAdding; }
//            set
//            {
//                _isAdding = value;
//                OnPropertyChanged("IsAdding");
//                OnPropertyChanged("AddButtonText");
//                SaveCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public string AddButtonText
//        {
//            get
//            {
//                if (IsAdding)
//                {
//                    return "Hủy";
//                }
//                else
//                {
//                    return "Thêm";
//                }
//            }
//        }

//        private bool _isEditing;
//        public bool IsEditing
//        {
//            get { return _isEditing; }
//            set
//            {
//                _isEditing = value;
//                OnPropertyChanged("IsEditing");
//                OnPropertyChanged("EditButtonText");
//                SaveCommand.RaiseCanExecuteChanged();
//            }
//        }

//        public string EditButtonText
//        {
//            get
//            {
//                if (IsEditing)
//                {
//                    return "Hủy";
//                }
//                else
//                {
//                    return "Sửa";
//                }
//            }
//        }

//        // Commands
//        public RelayCommand AddCommand { get; set; }
//        public RelayCommand EditCommand { get; set; }
//        public RelayCommand SaveCommand { get; set; }
//        public RelayCommand DeleteCommand { get; set; }

//        public AccountViewModel()
//        {
//            Accounts = new ObservableCollection<AccountModel>();
//            Cities = new ObservableCollection<string>();
//            Cities.Add("HCM");
//            Cities.Add("HN");
//            Cities.Add("Đà Nẵng");
//            Cities.Add("Cần Thơ");

//            LoadSampleData();

//            AddCommand = new RelayCommand(AddOrCancel, null);
//            EditCommand = new RelayCommand(EditOrCancel, CanEditOrDelete);
//            SaveCommand = new RelayCommand(Save, CanSave);
//            DeleteCommand = new RelayCommand(Delete, CanEditOrDelete);
//        }

//        private void LoadSampleData()
//        {
//            AccountModel acc = new AccountModel();
//            acc.STT = 1;
//            acc.AccountNumber = "001";
//            acc.CustomerName = "Nguyễn Văn A";
//            acc.Address = "Q1";
//            acc.City = "HCM";
//            acc.Balance = 1000000;
//            Accounts.Add(acc);
//        }

//        #region Logic

//        private void AddOrCancel(object obj)
//        {
//            if (!IsAdding)
//            {
//                ClearInput();
//                IsAdding = true;
//                IsEditing = false;
//            }
//            else
//            {
//                // Hủy thêm
//                ClearInput();
//                IsAdding = false;
//            }
//        }

//        private void EditOrCancel(object obj)
//        {
//            if (!IsEditing)
//            {
//                if (SelectedAccount == null)
//                {
//                    MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
//                    return;
//                }

//                // Copy dữ liệu từ SelectedAccount sang Input
//                InputAccountNumber = SelectedAccount.AccountNumber;
//                InputCustomerName = SelectedAccount.CustomerName;
//                InputAddress = SelectedAccount.Address;
//                InputCity = SelectedAccount.City;
//                InputBalance = SelectedAccount.Balance;

//                IsEditing = true;
//                IsAdding = false;
//            }
//            else
//            {
//                // Hủy sửa
//                ClearInput();
//                IsEditing = false;
//            }
//        }

//        private void Save(object obj)
//        {
//            if (IsAdding)
//            {
//                if (!ValidateInput()) return;

//                AccountModel acc = new AccountModel();
//                acc.STT = Accounts.Count + 1;
//                acc.AccountNumber = InputAccountNumber;
//                acc.CustomerName = InputCustomerName;
//                acc.Address = InputAddress;
//                acc.City = InputCity;
//                acc.Balance = InputBalance;

//                Accounts.Add(acc);
//                UpdateSTT();
//                OnPropertyChanged("TotalBalance");

//                ClearInput();
//                IsAdding = false;
//            }
//            else if (IsEditing)
//            {
//                if (SelectedAccount == null)
//                {
//                    MessageBox.Show("Không có tài khoản nào để sửa.");
//                    return;
//                }

//                if (!ValidateInput()) return;

//                SelectedAccount.AccountNumber = InputAccountNumber;
//                SelectedAccount.CustomerName = InputCustomerName;
//                SelectedAccount.Address = InputAddress;
//                SelectedAccount.City = InputCity;
//                SelectedAccount.Balance = InputBalance;

//                OnPropertyChanged("TotalBalance");
//                ClearInput();
//                IsEditing = false;
//            }
//        }

//        private void Delete(object obj)
//        {
//            if (SelectedAccount == null)
//            {
//                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.");
//                return;
//            }

//            Accounts.Remove(SelectedAccount);
//            UpdateSTT();
//            OnPropertyChanged("TotalBalance");
//            ClearInput();
//        }

//        #endregion

//        #region Validate

//        private bool ValidateInput()
//        {
//            if (string.IsNullOrEmpty(InputAccountNumber))
//            {
//                MessageBox.Show("Số tài khoản không được rỗng.");
//                return false;
//            }

//            if (string.IsNullOrEmpty(InputCustomerName))
//            {
//                MessageBox.Show("Tên khách hàng không được rỗng.");
//                return false;
//            }

//            if (InputBalance < 0)
//            {
//                MessageBox.Show("Số tiền không được âm.");
//                return false;
//            }

//            bool duplicate = false;

//            if (IsAdding)
//            {
//                duplicate = Accounts.Any(a => a.AccountNumber == InputAccountNumber);
//            }
//            else if (IsEditing)
//            {
//                if (InputAccountNumber != SelectedAccount.AccountNumber)
//                {
//                    duplicate = Accounts.Any(a => a.AccountNumber == InputAccountNumber);
//                }
//            }

//            if (duplicate)
//            {
//                MessageBox.Show("Trùng số tài khoản.");
//                return false;
//            }

//            return true;
//        }

//        #endregion

//        #region CanExecute

//        private bool CanSave(object obj)
//        {
//            return IsAdding || IsEditing;
//        }

//        private bool CanEditOrDelete(object obj)
//        {
//            return SelectedAccount != null;
//        }

//        #endregion

//        #region Helper

//        private void ClearInput()
//        {
//            InputAccountNumber = "";
//            InputCustomerName = "";
//            InputAddress = "";
//            InputCity = null;
//            InputBalance = 0;
//        }

//        private void UpdateSTT()
//        {
//            int i = 1;
//            foreach (var acc in Accounts)
//            {
//                acc.STT = i;
//                i++;
//            }
//        }

//        public decimal TotalBalance
//        {
//            get
//            {
//                decimal total = 0;
//                foreach (AccountModel acc in Accounts)
//                {
//                    total += acc.Balance;
//                }
//                return total;
//            }
//        }

//        #endregion
//    }
//}

