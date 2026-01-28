using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace BT2_Control
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
        private bool ValidateForm()
        {
            bool valid = true;

            Brush normalBrush = Brushes.White; //Tạo một biến tên normalBrush kiểu Brush và gán cho nó màu trắng có sẵn trong WPF.
            Brush errorBrush = Brushes.MistyRose;

            // Họ tên
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                txtTen.Background = errorBrush;
                valid = false;
            }
            else
                txtTen.Background = normalBrush;

            // Nghề nghiệp
            if (string.IsNullOrWhiteSpace(txtNghe.Text))
            {
                txtNghe.Background = errorBrush;
                valid = false;
            }
            else
                txtNghe.Background = normalBrush;

            // Giới tính
            if (rdNam.IsChecked != true && rdNu.IsChecked != true)
            {
                lblGioiTinh.Foreground = Brushes.Red;
                valid = false;
            }
            else
                lblGioiTinh.Foreground = Brushes.Black;

            // Ngày sinh
            if (!dpNgaySinh.SelectedDate.HasValue ||
                dpNgaySinh.SelectedDate >= DateTime.Today) //HasValue: kiểm tra giá trị, trả về T/F
            {
                dpNgaySinh.Background = errorBrush;
                valid = false;
            }
            else
                dpNgaySinh.Background = normalBrush;

            // Quốc tịch
            if (cboQuocTich.SelectedItem == null)
            {
                cboQuocTich.Background = errorBrush;
                valid = false;
            }
            else
                cboQuocTich.Background = normalBrush;

            // Sở thích
            if (chkDocSach.IsChecked != true &&
                chkNhac.IsChecked != true &&
                chkTheThao.IsChecked != true &&
                chkDuLich.IsChecked != true)
            {
                lblSoThich.Foreground = Brushes.Red;
                valid = false;
            }
            else
                lblSoThich.Foreground = Brushes.Black;

            if (!valid)
                MessageBox.Show("Vui lòng nhập đầy đủ và đúng thông tin!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

            return valid;
        }


        private void BtnXem_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
                return;

            string gioiTinh = rdNam.IsChecked == true ? "Nam" : "Nữ";

            StringBuilder soThich = new StringBuilder(); //biến do bạn khai báo để lưu danh sách các sở thích đã chọn
            if (chkDocSach.IsChecked == true) soThich.Append("Đọc sách ");
            if (chkNhac.IsChecked == true) soThich.Append("Nghe nhạc ");
            if (chkTheThao.IsChecked == true) soThich.Append("Thể thao ");
            if (chkDuLich.IsChecked == true) soThich.Append("Du lịch ");

            // Kỹ năng (lấy từ ListBox)
            var kyNang = new StringBuilder();
            foreach (ListBoxItem item in lstKyNang.SelectedItems) //foreach (Kiểu biến in Tập_hợp):duyệt từng phần tử trong một tập hợp.
            {
                kyNang.Append(item.Content + " ");
            }

            //string.Join(separator, collection);separator: Ký tự hoặc chuỗi dùng để ngăn cách các phần tử - collection: ds chuỗi cần nối
            //string kyNang = string.Join(" ",
            //   lstKyNang.SelectedItems   //Tập hợp các phần tử đang được chọn trong ListBox
            //  .Cast<ListBoxItem>()            // Ép từng item thành ListBoxItem
            //  .Select(item => item.Content.ToString()) // Lấy nội dung hiển thị, tạo ds chuỗi
            //                                           // string.Join(" ", ...) Nối tất cả chuỗi lại với nhau, mỗi phần tử cách nhau 1 khoảng trắng
            // );

            lblTenKQ.Text = "Họ tên: " + txtTen.Text;
            lblNgheKQ.Text = "Nghề nghiệp: " + txtNghe.Text;
            lblGioiTinhKQ.Text = "Giới tính: " + gioiTinh;
            lblNgaySinhKQ.Text = "Ngày sinh: " +
                dpNgaySinh.SelectedDate.Value.ToString("dd/MM/yyyy"); //Value: lấy giá trị thực --> trả ra giá trị

            //        lblNgaySinhKQ.Text = "Ngày sinh: " +
            //dpNgaySinh.SelectedDate?.ToString("dd/MM/yyyy") ?? "Chưa chọn"; //Toán tử ?? gọi là null-coalescing operator (toán tử kết hợp null).
            //a ?? b: trả về a nếu a #null , ngược lại trả về b
            lblQuocTichKQ.Text = "Quốc tịch: " +
                (cboQuocTich.SelectedItem as ComboBoxItem)?.Content.ToString(); //?. (toán tử null-conditional): as: gán giá trị, trả về object
            lblSoThichKQ.Text = "Sở thích: " + soThich;
            lblKyNangKQ.Text = "Kỹ năng: " + kyNang;
            lblGhiChuKQ.Text = "Ghi chú: " + txtGhiChu.Text;

            tabMain.SelectedIndex = 1;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTen.Text = "";
            txtNghe.Text = "";
            rdNam.IsChecked = false;
            rdNu.IsChecked = false;
            dpNgaySinh.SelectedDate = null;
            cboQuocTich.SelectedItem = null;
            chkDocSach.IsChecked = false;
            chkNhac.IsChecked = false;
            chkTheThao.IsChecked = false;
            chkDuLich.IsChecked = false;
            lstKyNang.SelectedItems.Clear();
            txtGhiChu.Text = "";

            // reset màu
            txtTen.Background = Brushes.White;
            txtNghe.Background = Brushes.White;
            dpNgaySinh.Background = Brushes.White;
            cboQuocTich.Background = Brushes.White;
            lblGioiTinh.Foreground = Brushes.Black;
            lblSoThich.Foreground = Brushes.Black;

            tabMain.SelectedIndex = 0;
        }

      
    }
}
