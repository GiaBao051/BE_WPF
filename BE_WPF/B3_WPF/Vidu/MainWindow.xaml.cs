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

namespace Vidu
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

        private void txtten_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageBox.Show(txtten.Text);
        }

        private void chk1_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã chọn sở thích");
        }

        private void chk1_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã bỏ chọn sở thích");
        }

        private void rdNam_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            MessageBox.Show($"Bạn chọn: {rb.Content}");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            MessageBox.Show($"Bạn chọn: {item.Content}");
        }

        //SelectionChangedEventArgs e là đối số sự kiện, chứa thông tin item nào vừa được chọn hoặc bỏ chọn.
        //e.AddedItems: Là danh sách các item mới được chọn kể từ lần thay đổi trước đó. Kiểu: IList → có thể chứa 1 hoặc nhiều item,

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            foreach (ListBoxItem item in e.AddedItems) //foreach (Kiểu biến in Tập_hợp): Duyệt từng ListBoxItem vừa được chọn trong lần thay đổi này.
                MessageBox.Show($"Bạn chọn: {item.Content}");
        }
    }
}
