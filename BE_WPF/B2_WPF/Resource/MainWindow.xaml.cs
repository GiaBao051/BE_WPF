using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Resource
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

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            string name = txtYourName.Text.Trim();

            // Chuyển đổi an toàn, không kiểm tra lại
            int.TryParse(txtYear.Text.Trim(), out int birthYear);

            int age = DateTime.Now.Year - birthYear;

            string s = $"My name is: {name}\nAge: {age}";
            MessageBox.Show(s, "Thông tin");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //clear Text
            txtYourName.Clear();
            txtYear.Clear();
            //Textbox YourName reset focus
            txtYourName.Focus();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtYourName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return; //Nếu đối tượng gây ra sự kiện không phải là TextBox thì không xử lý

            if (string.IsNullOrEmpty(tb.Text))
            {
                lblNameError.Content = $"{tb.Tag} không được để trống";
                tb.BorderBrush = Brushes.Red;
            } else
            {
                lblNameError.Content = "";
                tb.BorderBrush = Brushes.Gray;
            }    

        }

        private void txtYear_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            // Lấy text từ TextBox
            string text = tb.Text.Trim();

            // Kiểm tra rỗng hoặc không phải số nguyên
            if (!int.TryParse(text, out int value))
            {
                lblYearError.Content = $"{tb.Tag} không được để trống và phải là số nguyên";
                tb.BorderBrush = Brushes.Red;
            }
            else
            // (Tuỳ chọn) kiểm tra khoảng năm sinh
            if (value < 1900 || value > DateTime.Now.Year)
            {
                lblYearError.Content = $"{tb.Tag} phải nhỏ hơn năm hiện tại";
                tb.BorderBrush = Brushes.Red;
            }
            else
            {
                lblYearError.Content = "";
                tb.BorderBrush = Brushes.Gray;
            }    
        }
    }
}
