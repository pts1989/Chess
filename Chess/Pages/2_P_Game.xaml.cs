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
        }

        
        #region Validation
        /**
         * Checks wether the move is a valid one based on data in the ObjectMovement class.
         * */
        private bool validateMove(Placeholder endPosition)
        {
            bool validMove = false;
            /**
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
            **/
            return validMove;
        }
        #endregion

    }
}
