
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtThanhTien.Text = "0";
        }

        // CLICK GHẾ
        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int state = int.Parse(btn.Tag.ToString());

            // Ghế đã bán
            if (state == 2)
            {
                MessageBox.Show("Ghế này đã được bán!");
                return;
            }

            // Trống -> chọn
            if (state == 0)
                btn.Tag = "1";
            // Chọn -> trống
            else if (state == 1)
                btn.Tag ="0";

            TinhTongTien();
        }

        // TÍNH TỔNG TIỀN TỰ ĐỘNG
        private void TinhTongTien()
        {
            int tong = 0;

            foreach (Button btn in GheButtons())
            {
                if (btn.Tag.ToString() == "1")
                {
                    int soGhe = int.Parse(btn.Content.ToString());
                    tong += GiaVe(soGhe);
                }
            }

            txtThanhTien.Text = tong.ToString();
        }

        // NÚT CHỌN
        private void BtnChon_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in GheButtons())
            {
                if (btn.Tag.ToString() == "1")
                    btn.Tag = 2; // đã bán
            }
        }

        // NÚT HỦY
        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in GheButtons())
            {
                if (btn.Tag.ToString() == "1")
                    btn.Tag = 0;
            }

            txtThanhTien.Text = "0";
        }

        // NÚT KẾT THÚC
        private void BtnKetThuc_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // GIÁ VÉ
        private int GiaVe(int soGhe)
        {
            if (soGhe <= 5) return 1000;
            if (soGhe <= 10) return 1500;
            return 2000;
        }

        // LẤY DANH SÁCH GHẾ
        private System.Collections.Generic.List<Button> GheButtons()
        {
            var list = new System.Collections.Generic.List<Button>();

            foreach (var item in LogicalTreeHelper.GetChildren(this))
            {
                if (item is Grid grid)
                {
                    foreach (var b in grid.Children)
                    {
                        if (b is Border border)
                        {
                            foreach (var sp in LogicalTreeHelper.GetChildren(border))
                            {
                                if (sp is StackPanel stack)
                                {
                                    foreach (var c in stack.Children)
                                    {
                                        if (c is UniformGrid ug)
                                        {
                                            foreach (Button btn in ug.Children)
                                                list.Add(btn);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}
