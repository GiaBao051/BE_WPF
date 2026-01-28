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

namespace BT1
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
        private void btnXem_Click(object sender, RoutedEventArgs e)
        {
            string hoDem = txtHoDem.Text.Trim();
            string ten = txtTen.Text.Trim();

            string gioiTinh = radNam.IsChecked == true ? "Nam" : "Nữ"; // Toán tử 3 ngôi: "điều_kiện ? giá_trị_1 : giá_trị_2" --> Đúng lấy giá trị 1, sai lấy giá trị 2

            string loiChao;
            if (gioiTinh == "Nam")
                loiChao = "Xin chào Mr. ";
            else
                loiChao = "Xin chào Miss/Mrs. ";

            // Ngoại ngữ
            StringBuilder ngoaiNgu = new StringBuilder(); //Lớp dùng để xây dựng và nối chuỗi
            if (chkAnh.IsChecked == true) ngoaiNgu.Append("Tiếng Anh "); // Append (String) Thêm chuỗi vào cuối
            if (chkTrung.IsChecked == true) ngoaiNgu.Append("Tiếng Trung ");

            if (ngoaiNgu.Length == 0)
                ngoaiNgu.Append("Không");

            // Quê quán
            string queQuan = "";
            if (cboQueQuan.SelectedItem is ComboBoxItem item)
            {
                queQuan = item.Content.ToString();
            }

            //string thongTin =
            //     loiChao + hoDem + " " + ten + "\n\n" +
            //     "Giới tính: " + gioiTinh + "\n" +
            //     "Ngoại ngữ: " + ngoaiNgu.ToString() + "\n" +
            //     "Quê quán: " + queQuan;
            string thongTin = $"{loiChao} {hoDem} {ten}\n Giới tính: {gioiTinh} \n Ngoại ngữ: {ngoaiNgu} \n Quê quán: {queQuan}";

            MessageBox.Show(thongTin, "Thông tin");
        }

        private void btnNhapLai_Click(object sender, RoutedEventArgs e)
        {
            txtHoDem.Text = "";
            txtTen.Text = "";

            radNam.IsChecked = true;
            radNu.IsChecked = false;

            chkAnh.IsChecked = false;
            chkTrung.IsChecked = false;

            cboQueQuan.SelectedItem = null;

        }

    
    }
}
