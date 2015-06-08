#region using statements
using Chess.Utilities;
using Chess.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
#endregion

namespace Chess.Pages
{
    /// <summary>
    /// Interaction logic for _2_P_Game.xaml
    /// </summary>
    public partial class _2_P_Game : Page
    {
        
        List<List<Placeholder>> spaces;
        public _2_P_Game()
        {
            InitializeComponent();
            Chessboard.initialise_Board();
            Chessboard.moveMade += new EventHandler(moveMade);
        }



        void moveMade(object sender, EventArgs e)
        {
            MoveData data = (MoveData)sender;
            addMoveString(data.moveInfo, (data.turn % 2)+1);
        }

        public void addMoveString(string move, int player)
        {
            if (player == 1)
            {
                P1_Textbox.Text += "\n" + move;
            }
            else if (player == 2)
            {
                P2_Textbox.Text += "\n" + move;
            }
        }
    }
}
