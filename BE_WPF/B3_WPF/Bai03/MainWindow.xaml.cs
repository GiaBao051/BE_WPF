using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bai03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
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

        private void txtSDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnThemMon_Click(object sender, RoutedEventArgs e)
        {
            string mon = cboMon.SelectedItem.ToString();

            if (lstMon.Items.Contains(mon))
            {
                countItem(mon);
            }
            lstMon.Items.Add(mon);
            UpdateThongTin();
        }

        private int countItem(string mon)
        {
            int count = 0;
            foreach(string item in lstMon.Items)
            {
                count++;
            }
            return count;
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

            MessageBox.Show("Đặt món thành công!");
        }

        void UpdateThongTin()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Khách hàng: " + txtTen.Text);
            sb.AppendLine("SĐT: " + txtSDT.Text);
            sb.AppendLine("Bàn: " + cboBan.SelectedItem);

            txtThongTin.Text = sb.ToString();
        }

        private void txtSDT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}