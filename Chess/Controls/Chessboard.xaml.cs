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
using Chess.Controls;
using Chess.Utilities;
using Chess.Units;

namespace Chess.Controls
{
    /// <summary>
    /// Interaction logic for Chessboard.xaml
    /// </summary>
    /// 
    public partial class Chessboard : UserControl
    {
        private static List<List<Placeholder>> spaces;
        private Unit currently_Dragged_Unit;
        private int turnCounter = 0;

        public Chessboard()
        {
            InitializeComponent();
        }

        public void initialise_Board()
        {

            UIElementCollection col = boardArea.Children;
            spaces = new List<List<Placeholder>>();

            // get all the spaces in an array
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
                spaces[i][1].chessPiece = new Pawn(Chesspieces.WHITE);
                spaces[i][6].chessPiece = new Pawn(Chesspieces.BLACK);
            }

            for (int i = 0; i < 8; i++)
            {
                UserControl chesspiece_white = null, chesspiece_black = null;
                int white_id = 0, black_id = 0;


                switch (i)
                {
                    case 0: // tower
                        chesspiece_white = new Tower(Chesspieces.WHITE);
                        chesspiece_black = new Tower(Chesspieces.BLACK);
                        break;
                    case 1:
                        chesspiece_white = new Horse(Chesspieces.WHITE);
                        chesspiece_black = new Horse(Chesspieces.BLACK);
                        break;
                    case 2:
                        chesspiece_white = new Bishop(Chesspieces.WHITE);
                        chesspiece_black = new Bishop(Chesspieces.BLACK);
                        break;
                    case 3:
                        chesspiece_white = new Queen(Chesspieces.WHITE);
                        chesspiece_black = new Queen(Chesspieces.BLACK);
                        break;
                    case 4:
                        chesspiece_white = new King(Chesspieces.WHITE);
                        chesspiece_black = new King(Chesspieces.BLACK);
                        break;
                    case 5:
                        chesspiece_white = new Bishop(Chesspieces.WHITE);
                        chesspiece_black = new Bishop(Chesspieces.BLACK);
                        break;
                    case 6:
                        chesspiece_white = new Horse(Chesspieces.WHITE);
                        chesspiece_black = new Horse(Chesspieces.BLACK);
                        break;
                    case 7:
                        chesspiece_white = new Tower(Chesspieces.WHITE);
                        chesspiece_black = new Tower(Chesspieces.BLACK);
                        break;
                }
                spaces[i][0].chessPiece = chesspiece_white;
                spaces[i][7].chessPiece = chesspiece_black;

            }

        }

        /**
         *  Method launches when a user 'picks up' a chess piece
         **/
        private void placeholder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var src = (Placeholder)e.Source;
            Unit piece = (Unit)src.chessPiece;
            if (piece != null)
            {
                // check if it is the right player, white starts
                if (((turnCounter % 2) == 0 && piece.pieceID <= 6) || ((turnCounter % 2) == 1 && piece.pieceID > 6))
                {

                    // save variables in the piece itself, then register this unit as being 'dragged'
                    piece.origin = getPiecePositionInArray(src);
                    currently_Dragged_Unit = piece;

                    src.chessPiece = null;

                }
            }
             
        }

        /**
         *  Method launches when the user 'places' a chesspiece
         **/
        private void placeholder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dest = (Placeholder)e.Source;
            Unit piece = currently_Dragged_Unit;

            Point destination = getPiecePositionInArray(dest);

            // check if it is the right player, white starts
            if (piece != null)
            {
                if (((turnCounter % 2) == 0 && piece.pieceID <= 6) || ((turnCounter % 2) == 1 && piece.pieceID > 6))
                {
                    // let the piece decide wether the move is valid.
                    if (piece.validateMove(destination, spaces))
                    {
                        dest.chessPiece = piece;

                        if (piece.origin != destination)
                        {

                            // moved to different spot, so register and pass turn to other player
                            if (turnCounter % 2 == 0)
                            {
                                //P1_Textbox.Text += "\n" + piece.getPieceType() + " to " + piece.pieceType;
                            }
                            else
                            {
                                //P2_Textbox.Text += "\n" + piece.getPieceType() + " to " + piece.pieceType;
                            }

                            turnCounter++;
                        }
                    }
                    else
                    {
                        // place back the image at original position
                        spaces[(int)piece.origin.X][(int)piece.origin.Y].chessPiece = currently_Dragged_Unit;
                    }
                }
            }
        }

        /**
         * returns the x(column) and y(row) of the placeholder on the board.
         *  returns a -1,-1 point if no such placeholder was found.
         **/
        private Point getPiecePositionInArray(Placeholder piece)
        {
            // get the index of the target position
            int column = -1, row = -1;
            for (int i = 0; i < spaces.Count; i++)
            {
                if (spaces[i].IndexOf(piece) > -1)
                {
                    column = i;
                    row = spaces[i].IndexOf(piece);
                }
            }
            return new Point(column, row);
        }

        public static bool pieceCollision(Point start, Point end, Point movement)
        {
            bool collision = false;
            // only two types, diagonal or straight line movement.
            string direction = "diagonal";

            if ((movement.X == 0 && movement.Y != 0) || (movement.X != 0 && movement.Y == 0))
            {
                //moving over one line only.
                if (movement.X == 0 && movement.Y != 0)
                {
                    direction = "vertical";
                }
                else
                {
                    direction = "horizontal";
                }
            }


            switch (direction)
            {
                case "horizontal":
                    {
                        for (int colum = 0; colum < spaces.Count; colum++)
                        {
                            if ((movement.X > 0 && (colum > start.X && colum < end.X)) || (movement.X < 0 && (colum > end.X && colum < start.X)))
                            {
                                if ((Unit)spaces[colum][(int)start.Y].chessPiece != null)
                                {
                                    collision = true;
                                }
                            }

                        }
                        break;
                    }
                case "vertical":
                    {
                        for (int row = 0; row < spaces.Count; row++)
                        {
                            if ((movement.Y > 0 && (row > start.Y && row < end.Y)) || (movement.Y < 0 && (row > end.Y && row < start.Y)))
                            {
                                if (spaces[row][(int)start.X].chessPiece != null)
                                {
                                    collision = true;
                                }
                            }

                        }
                        break;
                    }
                case "diagonal":
                    {
                        collision = diagonalCheck(start, end, movement, collision);
                        break;
                    }
            }
            return collision;
        }

        public static bool diagonalCheck(Point start, Point end, Point movement, bool collision = false)
        {
            int x = 1, y = 1;

            if (movement.X < 0)
            {
                x = -1;
            }

            if (movement.Y < 0)
            {
                y = -1;
            }

            Point checkPoint = new Point(start.X + x, start.Y + y);

            for (int colum = 0; colum < spaces.Count; colum++)
            {
                if (colum == checkPoint.X)
                {
                    for (int row = 0; row < spaces[colum].Count; row++)
                    {
                        if (row == checkPoint.Y)
                        {
                            if (spaces[row][row].chessPiece != null)
                            {
                                collision = true;
                            }
                        }
                    }
                }
            }

            if (!collision && checkPoint != end)
            {
                diagonalCheck(checkPoint, end, movement, collision);
            }

            return collision;
        }
    }
}
