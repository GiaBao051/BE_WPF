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

namespace Button_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button btn = new Button();
            //btn.Content = "Hoàn Kim";


            TextBlock txbl = new TextBlock();
            txbl.Text = "Gia Bao";
            btn.Content = txbl;

            TextBox txb = new TextBox();
            txb.Height = 100;
            txb.Width = 100;
        }
    }
}