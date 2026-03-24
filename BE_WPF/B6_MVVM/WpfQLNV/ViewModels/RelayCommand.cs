using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace WpfQLNV.ViewModels
{

    public class RelayCommand : ICommand
    {
        // Hàm sẽ được thực thi khi Command chạy (ví dụ: click Button)
        private readonly Action<object> _execute;

        // Hàm kiểm tra điều kiện cho phép Command chạy
        // Nếu trả về false → Button sẽ bị disable
        private readonly Predicate<object> _canExecute;

        // Constructor: nhận hàm Execute và hàm CanExecute từ ViewModel
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            // Kiểm tra execute không được null
            // vì Command bắt buộc phải có hành động để chạy
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); //??: toán tử thay thế khi null

            // CanExecute có thể null (nghĩa là luôn cho phép chạy)
            _canExecute = canExecute;
        }

        // WPF gọi hàm này để kiểm tra Button có được click hay không; quyết định bật/tắt Button; true  → Button ENABLE; false → Button DISABLE
        public bool CanExecute(object parameter)
        {
            // Nếu không có điều kiện thì luôn cho phép chạy
            if (_canExecute == null)
                return true;

            // Nếu có điều kiện thì gọi hàm kiểm tra
            return _canExecute(parameter);
        }

        // WPF gọi hàm này khi user thực hiện Command (ví dụ click Button)
        public void Execute(object parameter)
        {
            // Gọi hàm được truyền từ ViewModel
            _execute(parameter);
        }

        // Event giúp UI cập nhật lại trạng thái Enable/Disable của Button
        public event EventHandler CanExecuteChanged;

        // Hàm dùng để thông báo cho UI kiểm tra lại CanExecute
        // Ví dụ: khi SelectedItem thay đổi
        public void RaiseCanExecuteChanged()
        {
            // ?. là null-safe operator
            // Nếu có subscriber thì event sẽ được gọi
            CanExecuteChanged?.Invoke(this, EventArgs.Empty); //?.: toán tử kiểm tra null trước khi truy cập
        }
    }
    //public class RelayCommand : ICommand
    //{
    //    // Fields để lưu các delegate truyền vào
    //    private readonly Action<object> _execute;
    //    private readonly Predicate<object> _canExecute;

    //    // Constructor
    //    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    //    {
    //        if (execute == null)
    //        {
    //            throw new ArgumentNullException("execute");
    //        }

    //        _execute = execute;
    //        _canExecute = canExecute;
    //    }

    //    // ICommand.CanExecute
    //    public bool CanExecute(object parameter)
    //    {
    //        if (_canExecute == null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return _canExecute(parameter);
    //        }
    //    }

    //    // ICommand.Execute
    //    public void Execute(object parameter)
    //    {
    //        _execute(parameter);
    //    }

    //    // ICommand.CanExecuteChanged
    //    private EventHandler _canExecuteChangedInternal;

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add
    //        {
    //            _canExecuteChangedInternal += value;
    //            CommandManager.RequerySuggested += value;
    //        }
    //        remove
    //        {
    //            _canExecuteChangedInternal -= value;
    //            CommandManager.RequerySuggested -= value;
    //        }
    //    }

    //    // Phương thức gọi CanExecuteChanged thủ công nếu cần
    //    public void RaiseCanExecuteChanged()
    //    {
    //        if (_canExecuteChangedInternal != null)
    //        {
    //            _canExecuteChangedInternal(this, EventArgs.Empty);
    //        }
    //    }
    //}
   
}
