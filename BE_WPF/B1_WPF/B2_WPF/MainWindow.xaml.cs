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

namespace B2_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] people = new string[5];
        private int count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txt_name.Text.Trim();
            string age = txt_age.Text.Trim();
            
            //Kiểm tra rỗng
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(age) )
            {
                txt_xuatDS.Text = "Vui lòng nhập đầy đủ họ tên và tuổi!";
                return;
            }

            //Kiểm tra tuổi là số
            if (!int.TryParse(age, out int ageNum))
            {
                txt_xuatDS.Text = "Tuổi phải là số!";
                return;
            }

            //Kiểm tra mảng đã đầy chưa
            if(count >= people.Length)
            {
                txt_xuatDS.Text = "Mảng đã đầy, không thể thêm!";
                return;
            }

            //Lưu vào mảng
            people[count] = $"{name} - {ageNum} tuổi";
            count++;

            //Hiển thị danh sách
            txt_xuatDS.Text = "Danh sách người:\n";
            for(int i = 0; i < count; i++)
            {
                txt_xuatDS.Text += $"{i + 1}. {people[i]}\n";
            }

            //Xóa textbox để nhập tiếp
            txt_name.Clear();
            txt_age.Clear();
            txt_name.Focus();
        }
    }
}