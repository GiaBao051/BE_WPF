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
using System.Windows.Shapes;

namespace WpfQLNV.View
{
    /// <summary>
    /// Interaction logic for MainWindowQLNV.xaml
    /// </summary>
    public partial class MainWindowQLNV : Window
    {
        public MainWindowQLNV()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Cập nhật SelectedDepartment khi chọn TreeView, do TreeView không biding được tới SelectedItem
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is ViewModels.MainViewModel vm)
            {
                if (e.NewValue is Models.Department dep)
                    vm.SelectedDepartment = dep; 
                //Gán department vừa chọn từ TreeView vào property SelectedDepartment của ViewModel. Khi user click  vào TreeView -> ViewModel biết được đang chọn gì
            }
        }
    }
}
