using System.Globalization;
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

namespace B1_WPF
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = txt_nhapTen.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                txt_xuatTen.Text = "Vui lòng nhập tên bạn!";
            }
            else
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                string formatName = textInfo.ToTitleCase(name.ToLower());
                txt_xuatTen.Text = $"Xin chào, {formatName}";
            }
            txt_xuatTen.Focus();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_xuatTen_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}