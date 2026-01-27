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

namespace BT2_Chuong2
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
        private void cbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTheme.SelectedItem is ComboBoxItem item)   //lấy item đang được chọn, Nếu item đang được chọn là một ComboBoxItem thì gán nó vào biến item và thực hiện khối lệnh bên trong
            {
                string theme = item.Content.ToString(); //lấy tên theme
                ResourceDictionary dict = new ResourceDictionary();// tạo mới object ResourceDictionary, rỗng chưa có resource nào

                switch (theme)
                {
                    case "Sáng":
                        dict.Source = new Uri("Themes/ThemeLight.xaml", UriKind.Relative);  //Khi gán Source -->WPF sẽ load toàn bộ nội dung XAML vào dict
                                              //Uri: Đại diện đường dẫn tới file theme UriKind.Relative = WPF hiểu đây đường dẫn tương đối dựa vào project,

                        //Ý nghĩa: Lấy file ThemeLight.xaml -> đọc toàn bộ resource trong file -> đưa vào object "dict" để dùng 
                        break;
                   case "Tối":
                        dict.Source = new Uri("Themes/ThemeDark.xaml", UriKind.Relative);
                        break;
                }

                // Xóa theme cũ
                Application.Current.Resources.MergedDictionaries.Clear();    //MergedDictionaries = resource được import từ file khác (ví dụ theme)
                // Thêm theme mới
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
        }
    }
}