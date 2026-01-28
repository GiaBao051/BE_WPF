using System;
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

namespace Bai02
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

        private void BtnXemThongTin_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
                return;
            // Lấy dữ liệu
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = radNam.IsChecked == true ? "Nam" :
            radNu.IsChecked == true ? "Nữ" : "Chưa chọn";
            string ngaySinh = dtpNgaySinh.SelectedDate?.ToString("dd/MM/yyyy") ?? "Chưa chọn";
            string quocTich = (cmbQuocTich.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Chưa chọn";
            string ngheNghiep = txtNgheNghiep.Text.Trim();

            // Sở thích
            var soThich = new StringBuilder();
            if (chkDocSach.IsChecked == true) soThich.Append("Đọc sách, ");
            if (chkNgheNhac.IsChecked == true) soThich.Append("Nghe nhạc,");
            if (chkTheThao.IsChecked == true) soThich.Append("Thể thao, ");
            if (chkDuLich.IsChecked == true) soThich.Append("Du lịch, ");
            string soThichText = soThich.Length > 0 ?
            soThich.ToString().TrimEnd(',', ' ') : "Không có";

            // Kỹ năng
            var kyNang = lstKyNang.SelectedItems.Cast<ListBoxItem>().Select(i => i.Content.ToString());
            string kyNangText = kyNang.Any() ? string.Join(", ", kyNang) : "Chưa chọn";
            // Ghi chú
            string ghiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? "Không có" : txtGhiChu.Text;

            // Hiển thị
            lblHoTen.Text = hoTen;
            lblGioiTinh.Text = gioiTinh;
            lblNgaySinh.Text = ngaySinh;
            lblQuocTich.Text = quocTich;
            lblNgheNghiep.Text = ngheNghiep;
            lblSoThich.Text = soThichText;
            lblKyNang.Text = kyNangText;
            lblGhiChu.Text = ghiChu;

            // Chuyển sang tab xem
            tabControl.SelectedIndex = 1;
        }
        private bool ValidateInputs()
        {
            bool valid = true;
            Brush normalBrush = Brushes.White;
            Brush errorBrush = Brushes.MistyRose;
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                txtHoTen.Background = errorBrush;
                valid = false;
            }
            else
                txtHoTen.Background = normalBrush;
            // Kiểm tra nghề nghiệp
            if (string.IsNullOrWhiteSpace(txtNgheNghiep.Text))
            {
                txtNgheNghiep.Background = errorBrush;
                valid = false;
            }
            else
                txtNgheNghiep.Background = normalBrush;
            // Kiểm tra ngày sinh
            if (!dtpNgaySinh.SelectedDate.HasValue ||
                dtpNgaySinh.SelectedDate >= DateTime.Today)
            {
                dtpNgaySinh.Background = errorBrush;
                valid = false;
            }
            else
                dtpNgaySinh.Background = normalBrush;
            // Kiểm tra quốc tịch
            if (cmbQuocTich.SelectedItem == null)
            {
                cmbQuocTich.Background = errorBrush;
                valid = false;
            }
            else
                cmbQuocTich.Background = normalBrush;
            if (!valid)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return valid;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}