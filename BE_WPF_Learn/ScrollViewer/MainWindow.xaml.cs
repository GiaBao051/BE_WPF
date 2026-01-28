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

namespace ScrollViewer
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get croll bar offset value
            //MessageBox.Show(scvMain.VerticalOffset.ToString());

            //Get viewport heigh
            //MessageBox.Show(scvMain.ViewportHeight.ToString());

            //Scroll To end
            //scvMain.ScrollToEnd();

            //Scroll To home
            //scvMain.ScrollToHome();

            //Maxium scroll offset
            MessageBox.Show(scvMain.ScrollableHeight.ToString());
        }
    }
}