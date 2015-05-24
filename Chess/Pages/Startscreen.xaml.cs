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

namespace Chess.Pages
{
    /// <summary>
    /// Interaction logic for Startscreen.xaml
    /// </summary>
    public partial class Startscreen : Page
    {
        public Startscreen()
        {
            InitializeComponent();
            TwoPButton.Text = "2-Player game";
            AIButton.Text = "Versus A.I.";
        }

        private void TwoPButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow.GetWindow(this).Content = new _2_P_Game();
        }
    }
}
