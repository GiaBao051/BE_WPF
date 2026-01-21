using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace B5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Sự kiện LostFocus để kiểm tra rỗng
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                MessageBox.Show($"{tb.Name} không được để trống!",
                                "Cảnh báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        // Sự kiện Click của nút
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string info = $"Họ tên: {txtHoTen.Text}\n" +
                          $"Tuổi: {txtTuoi.Text}\n" +
                          $"Ghi chú: {txtGhiChu.Text}";

            MessageBox.Show(info,
                            "Thông tin đã nhập",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }


}