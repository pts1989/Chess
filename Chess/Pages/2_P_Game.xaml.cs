#region using statements
using Chess.Utilities;
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
            initialise_Board();
        }

        // Method that clears the board, then places all white and black pieces
        // at their respective starting positions
        public void initialise_Board()
        {

            UIElementCollection col = boardArea.Children;
            spaces = new List<List<Placeholder>>();

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
                //Pawn pawn = new Pawn();
                //pawn.pieceId = 1;
                //pawn.Source = Chess.Utilities.Chesspieces.white_pawn;

                // place pawns on 2nd rows from each side
                spaces[i][1].chessPieceID = 1;
                spaces[i][1].chessImage.Source = Chess.Utilities.Chesspieces.white_pawn;
                //spaces[i][1].chessPiece = pawn;

                //Pawn pawnBlack = new Pawn();
                //pawn.pieceId = 7;
                //pawn.Source = Chess.Utilities.Chesspieces.black_pawn;

                spaces[i][6].chessImage.Source = Chess.Utilities.Chesspieces.black_pawn;
                spaces[i][6].chessPieceID = 7;
                //spaces[i][6].chessPiece = pawnBlack;

            }
            for (int i = 0; i < 8; i++)
            {
                BitmapImage image_white = null, image_black = null;
                int white_id=0, black_id = 0;
                //object white = null, black =null;
                
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
                //spaces[i][0].chessPiece = white;

                spaces[i][7].chessImage.Source = image_black;
                spaces[i][7].chessPieceID = black_id;
                //spaces[i][7].chessPiece = black;
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

        // returns the x(column) and y(row) of the placeholder on the board.
        // returns a -1,-1 point if no such placeholder was found.
        private Point getPiecePositionInArray(Placeholder piece)
        {
            // get the index of the target position
            int column=-1, row=-1;
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

        private Point getMovementCoordinates(Point start, Point end)
        {
            return new Point(end.X - start.X, end.Y - start.Y);
        }

        private bool pieceCollision(Point start, Point end, Point movement)
        {
            bool collision = false;
            // only two types, digonal or straight line movement.
            string direction = "diagonal";

            if ((movement.X == 0 && movement.Y != 0) || (movement.X != 0 && movement.Y == 0))
            {
                //moving over one line only.
                if(movement.X == 0 && movement.Y != 0)
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
                                if (spaces[colum][(int)start.Y].chessPieceID > 0)
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
                                if (spaces[row][(int)start.X].chessPieceID > 0)
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

        #region chess piece movement
        
        private bool diagonalCheck(Point start, Point end, Point movement,bool collision = false)
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
                            if (spaces[row][row].chessPieceID > 0)
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

        private void placeholder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var src = (Placeholder)e.Source;
            var image = src.chessImage.Source;

            // check if it is the right player, white starts
            if (((ObjectMovement.turnCounter % 2) == 0 && src.chessPieceID <= 6) || ((ObjectMovement.turnCounter % 2) == 1 && src.chessPieceID > 6))
            {
                if (image != null)
                {
                    ObjectMovement.source = src;
                    ObjectMovement.draggedImage = image;
                    ObjectMovement.pieceID = src.chessPieceID;
                    ObjectMovement.originName = src.Name;
                    src.chessPieceID = 0;
                    src.chessImage.Source = null;
                }
            }
        }

        private void placeholder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var src = (Placeholder)e.Source;
            // check if it is the right player, white starts
            if (((ObjectMovement.turnCounter % 2) == 0 && ObjectMovement.pieceID <= 6) || ((ObjectMovement.turnCounter % 2) == 1 && ObjectMovement.pieceID > 6))
            {
                if (ObjectMovement.draggedImage != null)
                {
                    if (validateMove(src))
                    {
                        src.chessImage.Source = ObjectMovement.draggedImage;
                        src.chessPieceID = ObjectMovement.pieceID;

                        if (!ObjectMovement.originName.Equals(src.Name))
                        {
                            // moved to different spot, so register and pass turn to other player
                            if (ObjectMovement.turnCounter % 2 == 0)
                            {
                                P1_Textbox.Text += "\n" + getPieceType(ObjectMovement.pieceID) + " to " + src.Name;
                            }
                            else
                            {
                                P2_Textbox.Text += "\n" + getPieceType(ObjectMovement.pieceID) + " to " + src.Name;
                            }

                            ObjectMovement.turnCounter++;
                        }
                    }
                    else
                    {
                        // place back the image at original position
                        ObjectMovement.source.chessImage.Source = ObjectMovement.draggedImage;
                        if (src.chessPieceID > 0)
                        {
                            //ObjectMovement.pieceID = src.chessPieceID;
                            //ObjectMovement.originName = src.Name;
                        }
           
                    }
                    //reset mogelijk nodig voor juiste validatie van velden.

                    //ObjectMovement.draggedImage = null;
                    //ObjectMovement.pieceID = 0;
                    //ObjectMovement.source = null;
                    //ObjectMovement.originName = null;

                }
                
            }
        }
        #endregion
        
        #region Validation
        /**
         * Checks wether the move is a valid one based on data in the ObjectMovement class.
         * */ 
        private bool validateMove(Placeholder endPosition)
        {
            bool validMove = false;

            if (endPosition.chessPieceID == 0|| ((ObjectMovement.pieceID <= 6 && endPosition.chessPieceID >= 7) || ((ObjectMovement.pieceID >= 7 && endPosition.chessPieceID <= 6))))
            { 
                Point target = getPiecePositionInArray(endPosition);
                Point source = getPiecePositionInArray(ObjectMovement.source);
                Point movementCoordinates = getMovementCoordinates(source, target);
            
                //if (endPosition.chessPiece == typeof(Pawn))
                //{
                //    Pawn pawn = (Pawn)endPosition.chessPiece;
                //    validMove = pawn.makenMove(source, target);           
                //}

                switch (getPieceType(ObjectMovement.pieceID))
                {
                    case "Pawn":
                        //can only move straight vertical, except when capturing. and enpassment
                        if (Math.Abs(movementCoordinates.Y) == 1 && Math.Abs(movementCoordinates.X) == 0)
                        {
                            validMove = true;
                            // first time max 2 movement.
                        }
                        break;
                    case "Tower":
                        if ((movementCoordinates.X != 0 && movementCoordinates.Y == 0) || (movementCoordinates.X == 0 && movementCoordinates.Y != 0))
                        {
                            // movement is allowed, now check for pieces in between.
                            if (!pieceCollision(source, target, movementCoordinates))
                            {
                                validMove = true;
                            }
                        
                        }
                        //Only horizontal and vertical movement allowed. If piece between start and end, invalid move. 
                        break;
                    case "Horse":
                        if ((Math.Abs(movementCoordinates.X) == 1 && Math.Abs(movementCoordinates.Y) == 2) || (Math.Abs(movementCoordinates.X) == 2 && Math.Abs(movementCoordinates.Y) == 1))
                        {
                            validMove = true;
                        }
                        // Can jump over other objects. Always eighter one or two places sideways and always eighter one or two places up/downwards. If sideways one, 
                        //then up/down two places, if up/down one then sideways two places
                        break;
                    case "Bishop":
                        if(Math.Abs(movementCoordinates.X) == Math.Abs(movementCoordinates.Y))
                        {
                            if (!pieceCollision(source, target, movementCoordinates))
                            {
                                validMove = true;
                            }
                        }
                        //Only diogonal movement allowed. If piece between start and end, invalid move. 
                        break;
                    case "Queen":
                        // can move in every direction. If piece between start and end, invalid move. 
                        if ((Math.Abs(movementCoordinates.X) <= 1 && Math.Abs(movementCoordinates.Y) <= 1) 
                            ||(Math.Abs(movementCoordinates.X) == Math.Abs(movementCoordinates.Y)) 
                            || ((movementCoordinates.X != 0 && movementCoordinates.Y == 0) || (movementCoordinates.X == 0 && movementCoordinates.Y != 0)))
                        {
                            if (!pieceCollision(source, target, movementCoordinates))
                            {
                                validMove = true;
                            }
                        }
                        break;
                    case "King":
                        if (Math.Abs(movementCoordinates.X) <= 1 && Math.Abs(movementCoordinates.Y) <= 1)
                        {
                            validMove = true;
                        }
                        //can move in every direction, but can not move more than one place. Also he can not make a move that would result in a checkmate or check. Also 
                        // can switch places with tower once'castling'.
                        break;
                    default:
                        return false;
                }
            }
            return validMove;
        }

        #endregion

        #region Mouse loc
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        #endregion
    }
}
