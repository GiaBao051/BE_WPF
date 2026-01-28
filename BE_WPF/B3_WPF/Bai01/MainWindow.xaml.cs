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

namespace Bai01
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String strMessage, strHoTen, strTitle, strNgoaiNgu = "";
            strHoTen = textBox1.Text + " " + textBox2.Text;
            if (radNam.IsChecked == true)
                strTitle = "Mr.";
            else
                strTitle = "Miss/Mrs.";
            strMessage = "Xin chào: " + strTitle + " " + strHoTen;
            if (chkAnh.IsChecked == true)
            {
                strNgoaiNgu = "Tiếng Anh";
            }
            if (chkTrung.IsChecked == true)
                strNgoaiNgu = (strNgoaiNgu.Length == 0) ? "Tiếng Trung" :
                (strNgoaiNgu + " và Tiếng Trung");
            strMessage += "\n Ngoại ngữ: " + strNgoaiNgu;
            if (cbbQueQuan.SelectedIndex >= 0) //Nếu đã có một mục trong danh sách được chọn
{
                strMessage += "\n Quê quán: " + cbbQueQuan.Text;
            }
            MessageBox.Show(strMessage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            radNam.IsChecked = true;
            radNu.IsChecked = false;
            chkAnh.IsChecked = false;
            chkTrung.IsChecked = false;
            cbbQueQuan.SelectedIndex = 0;
        }
    }
}