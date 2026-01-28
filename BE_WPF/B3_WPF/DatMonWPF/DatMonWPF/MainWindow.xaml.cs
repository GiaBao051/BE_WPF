using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace DatMonWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // tạo control từ XAML
            LoadData();   // <-- BẮT BUỘC PHẢI CÓ
        }
        void LoadData()
        {
            cboBan.ItemsSource = new string[]
             {
                "Bàn 1",
                "Bàn 2",
                "Bàn 3"
            };

            cboMon.ItemsSource = new string[]
            {
                "Bún bò",
                "Trà sữa",
                "Cơm tấm",
                "Nước ngọt"
            };

            cboBan.SelectedIndex = 0;
            cboMon.SelectedIndex = 0;
        }

        // KIỂM TRA, CHẶN DỮ LIỆU TỪ ĐẦU VÀO - CHỈ CHO NHẬP SỐ - e: phím người dùng vừa gõ, văn bản sắp được nhập 
        private void txtSDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]");//biểu thức chính quy (regular expression): [^0-9+]: Không phải số hoặc +
            e.Handled = regex.IsMatch(e.Text);//e.Text là ký tự người dùng vừa nhập - regex.IsMatch(e.Text): kiểm tra e.Text có khớp với regex hay không trả về T/F.
                                              //e.Handled là một thuộc tính boolean (T: chặn; F: Cho phép) mặc định là F, nếu regex trả true, ký tự sẽ bị chặn.
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            string mon = cboMon.SelectedItem.ToString();

            if (!lstMon.Items.Contains(mon)) //lstMon.Items là ItemCollection, chứa tất cả item trong ListBox -- Kiểm tra xem item mon đã tồn tại trong ListBox chưa
                                             //Contains và Add là method có sẵn của ListBox.Items. Contains kiểm tra sự tồn tại của item, Add thêm item vào ListBox.
                lstMon.Items.Add(mon);

            UpdateThongTin();
        }

        private void btnXoaMon_Click(object sender, RoutedEventArgs e)
        {
            if (lstMon.SelectedItem != null)
                lstMon.Items.Remove(lstMon.SelectedItem);

            UpdateThongTin();
        }

        private void btnDatMon_Click(object sender, RoutedEventArgs e)
        {
            if (txtTen.Text == "" || txtSDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (lstMon.Items.Count == 0)
            {
                MessageBox.Show("Chưa chọn món!");
                return;
            }

            MessageBox.Show("Đặt món thành công 🎉");
        }

        void UpdateThongTin()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Khách hàng: " + txtTen.Text);
            sb.AppendLine("SĐT: " + txtSDT.Text);
            sb.AppendLine("Bàn: " + cboBan.SelectedItem);

            //if (cboBan.SelectedItem is ComboBoxItem item)
            //{
            //    sb.AppendLine("Bàn: " + item.Content.ToString());
            //}

            txtThongTin.Text = sb.ToString();
        }

     }
}
