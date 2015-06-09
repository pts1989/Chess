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
        private List<King> kingUnits = new List<King>();
        public Chessboard()
        {
            InitializeComponent();
        }

        public event EventHandler moveMade;
        private void MakeSomethingHappen(EventArgs e)
        {
            if (moveMade != null)
            {
                moveMade(this, e);
            }
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
                spaces[i][1].chessPiece = new Pawn(Chesspieces.WHITE, new Point(i, 1));
                spaces[i][6].chessPiece = new Pawn(Chesspieces.BLACK, new Point(i, 6));
            }

            for (int i = 0; i < 8; i++)
            {
                UserControl chesspiece_white = null, chesspiece_black = null;

                switch (i)
                {
                    case 0: // tower
                        chesspiece_white = new Tower(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Tower(Chesspieces.BLACK, new Point(i, 7));
                        break;
                    case 1:
                        chesspiece_white = new Horse(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Horse(Chesspieces.BLACK, new Point(i, 7));
                        break;
                    case 2:
                        chesspiece_white = new Bishop(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Bishop(Chesspieces.BLACK, new Point(i, 7));
                        break;
                    case 3:
                        chesspiece_white = new Queen(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Queen(Chesspieces.BLACK, new Point(i, 7));
                        break;
                    case 4:
                        chesspiece_white = new King(Chesspieces.WHITE, new Point(i,0));
                        chesspiece_black = new King(Chesspieces.BLACK, new Point(i,7));
                        kingUnits.Add((King)chesspiece_white);
                        kingUnits.Add((King)chesspiece_black);
                        break;
                    case 5:
                        chesspiece_white = new Bishop(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Bishop(Chesspieces.BLACK, new Point(i, 7));
                        break;
                    case 6:
                        chesspiece_white = new Horse(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Horse(Chesspieces.BLACK, new Point(i, 7));
                        break;
                    case 7:
                        chesspiece_white = new Tower(Chesspieces.WHITE, new Point(i, 0));
                        chesspiece_black = new Tower(Chesspieces.BLACK, new Point(i, 7));
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
                        // now the boardasks the king wether this move will put the king in danger
                        // only necessary when the moved piece is not a king, otherwise
                        // validation will have already happened
                        bool validMove = true;
                        Unit old = null;
                        if (piece.pieceID != 6 && piece.pieceID != 12)
                        {
                            old = (Unit)spaces[(int)destination.X][(int)destination.Y].chessPiece;
                            spaces[(int)destination.X][(int)destination.Y].chessPiece = currently_Dragged_Unit;
                            validMove = !kingUnits[turnCounter % 2].check(kingUnits[turnCounter % 2].origin, spaces);
                        }
                        if (validMove)
                        {
                            // actually replace the chessPiece and update the origin
                            dest.chessPiece = piece;
                            piece.origin = destination;

                            // moved to different spot, so register and pass turn to other player
                            MoveData newdata = new MoveData(piece.getPieceType() + " to " + dest.Name, turnCounter);
                            moveMade(newdata, new EventArgs());

                            // a move has been made, it's possible the opponent king is now checkmate
                            if (kingUnits[(turnCounter+1) % 2].checkMate(kingUnits[(turnCounter+1) % 2].origin, spaces))
                            {
                                
                            }

                            turnCounter++;
                        }
                        else
                        {
                            // place back the piece at the original position and remove the temporary movement
                            spaces[(int)destination.X][(int)destination.Y].chessPiece = old;
                            spaces[(int)piece.origin.X][(int)piece.origin.Y].chessPiece = currently_Dragged_Unit;
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
                        for (int colum = 0; colum < spaces.Count-1; colum++)
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
                        for (int row = 0; row < spaces.Count-1; row++)
                        {
                            if ((movement.Y > 0 && (row > start.Y && row < end.Y)) || (movement.Y < 0 && (row > end.Y && row < start.Y)))
                            {
                                if (spaces[(int)start.X][row].chessPiece != null)
                                {
                                    collision = true;
                                }
                            }

                        }
                        break;
                    }
                case "diagonal":
                    {
                        collision = diagonalCheck(start, end, movement);
                        break;
                    }
            }
            return collision;
        }

        /**
         * Returns a bool based on wether there are pieces between the origin and 
         * destination points
         * */
        public static bool diagonalCheck(Point origin, Point destination, Point movement)
        {
            // first make sure it's actually diagonal movement, otherwise return true
            if ((Math.Abs(movement.X) / Math.Abs(movement.Y) != 1))
            {
                return true;
            }
            else
            {
                // check for actual collision
                // loop through diagonally, adding i to the X and Y of movement
                for (int i = 0; i != (int)movement.X; i=(int)movement.X)
                {
                    for (int j = 0; j < (int)movement.Y; j += (int)(movement.Y / Math.Abs(movement.Y)))
                    {
                        if (new Point((int)origin.X + i, (int)origin.Y + j) != origin) // do not take into account the origin
                        {
                            if (spaces[((int)origin.X + i)][((int)origin.Y + j)].chessPiece != null)
                            {
                                return true;
                            }
                        }
                        i += (int)(movement.X / movement.Y);
                    }
                }
            }
            return false;
        }

    }
}
