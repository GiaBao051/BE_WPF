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
            AddPhongBan("Giám đốc", "BGĐ");
            AddPhongBan("Kế hoạch", "PKH");
            AddPhongBan("Kế toán", "PKT");
        }

        void AddPhongBan(string tenPB, string maPB)
        {
            TreeViewItem pb = new TreeViewItem
            {
                Header = $"{tenPB} - {maPB}"
            };
            tvPhongBan.Items.Add(pb);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPhongBan(txtPhongBan.Text, txtMaPhong.Text);
        }

        private void tvPhongBan_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvPhongBan.SelectedItem != null)
            {
                TreeViewItem item = tvPhongBan.SelectedItem as TreeViewItem;
                string[] pb = (item.Header.ToString()).Split('-');
                lbPB1.Text = pb[0];
                lbMPB.Text = pb[1];
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}