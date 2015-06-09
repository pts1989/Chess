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
            Chessboard.checkMate += new EventHandler(showCheckMate);
            p1_arrow.Visibility = System.Windows.Visibility.Visible;
        }


        /**
         * Eventhandler for when a move is made. Chessboard will mainly use this to let this GUI show
         * a graphic representation of the move
         * */
        void moveMade(object sender, EventArgs e)
        {
            MoveData data = (MoveData)sender;
            addMoveString(data.moveInfo, (data.turn % 2)+1);
            if (data.turn % 2 == 1) { p1_arrow.Visibility = System.Windows.Visibility.Visible; p2_arrow.Visibility = System.Windows.Visibility.Hidden; }
            else if (data.turn % 2 == 0) { p2_arrow.Visibility = System.Windows.Visibility.Visible; p1_arrow.Visibility = System.Windows.Visibility.Hidden; }
        }

        /**
         * Eventhandler for when a checkmate occurs. Chessboard will end the game and show the winning player he won.
         * */
        void showCheckMate(object sender, EventArgs e)
        {
            int counter = (int)sender;
            if (counter % 2 == 0)
            {
                P1_Textbox.Text += "\n YOU WINNNN!";
            }
            else
            {
                P2_Textbox.Text += "\n YOU WINNNN!";
            }
            Chessboard.clearBoard();
            Chessboard.initialise_Board();
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
