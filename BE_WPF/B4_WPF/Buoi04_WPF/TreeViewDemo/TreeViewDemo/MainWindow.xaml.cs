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

namespace TreeViewDemo
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
        // Sự kiện của TreeView
        private void tvData_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //chạy khi item được chọn thay đổi: sender chính là tvData: e:Chứa thông tin trước và sau khi selection thay đổi
        // e.NewValue: item vừa được chọn
        //e.OldValue: item trước đó
        {
            //TreeViewItem item = e.NewValue as TreeViewItem; //Lấy item vừa được chọn
            //if (item != null)
            //{
            //    MessageBox.Show("Đang chọn: " + item.Header);
            //}
            //cách 2
            if (e.NewValue is TreeViewItem item)
            {
                MessageBox.Show($"Đang chọn: {item.Header}");
            }
        }

        // Sự kiện mở rộng nút
        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            MessageBox.Show("Mở rộng: " + item.Header);
        }

        // Sự kiện thu gọn nút
        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            MessageBox.Show("Thu gọn: " + item.Header);
        }
    }
}
