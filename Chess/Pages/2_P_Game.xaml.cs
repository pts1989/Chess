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
    /// Interaction logic for _2_P_Game.xaml
    /// </summary>
    public partial class _2_P_Game : Page
    {
        private int turnCounter = 0;

        public _2_P_Game()
        {
            InitializeComponent();
            initialise_Board();
        }

        // Method that clears the board, then places all white and black pieces
        // at their respective starting positions
        public void initialise_Board()
        {

            UIElementCollection col = boardArea.Children;
            List<List<Placeholder>> spaces = new List<List<Placeholder>>();
            for (int i = 0; i < 8; i++)
            {
                // loop through columns A - H
                spaces.Add(new List<Placeholder>());
                for (int j = 0; j < 8; j++)
                {
                    spaces[i].Add((Placeholder)col[j + (8 * i)]);
                }
            }

            // place pieces on board
            for (int i = 0; i < 8; i++)
            {
                // place pawns on 2nd rows from each side
                spaces[i][1].chessImage.Source = Chess.Utilities.Chesspieces.white_pawn;
                spaces[i][1].chessPieceID = 1;
                spaces[i][6].chessImage.Source = Chess.Utilities.Chesspieces.black_pawn;
                spaces[i][6].chessPieceID = 7;
            }
            for (int i = 0; i < 8; i++)
            {
                BitmapImage image_white = null, image_black = null;
                int white_id=0, black_id = 0;
                switch (i)
                {
                    case 0: // tower
                        image_white = Chess.Utilities.Chesspieces.white_tower;
                        white_id = 2;
                        image_black = Chess.Utilities.Chesspieces.black_tower;
                        black_id = 8;
                        break;
                    case 1:
                        image_white = Chess.Utilities.Chesspieces.white_horse;
                        white_id = 3;
                        image_black = Chess.Utilities.Chesspieces.black_horse;
                        black_id = 9;
                        break;
                    case 2:
                        image_white = Chess.Utilities.Chesspieces.white_bishop;
                        white_id = 4;
                        image_black = Chess.Utilities.Chesspieces.black_bishop;
                        black_id = 10;
                        break;
                    case 3:
                        image_white = Chess.Utilities.Chesspieces.white_queen;
                        white_id = 5;
                        image_black = Chess.Utilities.Chesspieces.black_queen;
                        black_id = 11;
                        break;
                    case 4:
                        image_white = Chess.Utilities.Chesspieces.white_king;
                        white_id = 6;
                        image_black = Chess.Utilities.Chesspieces.black_king;
                        black_id = 12;
                        break;
                    case 5:
                        image_white = Chess.Utilities.Chesspieces.white_bishop;
                        white_id = 4;
                        image_black = Chess.Utilities.Chesspieces.black_bishop;
                        black_id = 10;
                        break;
                    case 6:
                        image_white = Chess.Utilities.Chesspieces.white_horse;
                        white_id = 3;
                        image_black = Chess.Utilities.Chesspieces.black_horse;
                        black_id = 9;
                        break;
                    case 7:
                        image_white = Chess.Utilities.Chesspieces.white_tower;
                        white_id = 2;
                        image_black = Chess.Utilities.Chesspieces.black_tower;
                        black_id = 8;
                        break;
                }
                spaces[i][0].chessImage.Source = image_white;
                spaces[i][0].chessPieceID = white_id;
                spaces[i][7].chessImage.Source = image_black;
                spaces[i][7].chessPieceID = black_id;
            }

        }


        private ImageSource draggedImage;
        private Point mousePosition;
        private int pieceID;
        private string originName;
        private void placeholder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var src = (Placeholder)e.Source;
            var image = src.chessImage.Source;

            if (image != null)
            {
                mousePosition = e.GetPosition(boardArea);
                draggedImage = image;
                pieceID = src.chessPieceID;
                originName = src.Name;
                src.chessImage.Source = null;
            }
        }

        private void placeholder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (draggedImage != null)
            {
                var src = (Placeholder)e.Source;
                src.chessImage.Source = draggedImage;
                src.chessPieceID = pieceID;
                if (!originName.Equals(src.Name))
                {
                    // moved to different spot, so register and pass turn to other player
                    if (turnCounter % 2 == 0) {
                        P1_Textbox.Text += "\n" + getPieceType(pieceID) + " to " + src.Name; 
                    }
                    else
                    {
                        P2_Textbox.Text += "\n" + getPieceType(pieceID) + " to " + src.Name; 
                    }
                    turnCounter++;
                }
            }
        }

        private string getPieceType(int pieceID)
        {
            int type = pieceID;
            if (pieceID > 6) { type = pieceID - 6; }
            switch (type)
            {
                case 1:
                    return "Pawn";
                case 2:
                    return "Tower";
                case 3:
                    return "Horse";
                case 4:
                    return "Bishop";
                case 5:
                    return "Queen";
                case 6:
                    return "King";
                default:
                    return "Error";

            }
        }
    }
}
