using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class King : Unit
    {
        public King(int PIECE_COLOR)
        {
            pieceType = "King";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 6;
                chessImage.Source = Utilities.Chesspieces.white_king;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 12;
                chessImage.Source = Utilities.Chesspieces.black_king;
            }
        }

        /**
         * Override method of base Unit class. Checks wether the move with the given destination
         * is a valid one.
         * */
        public override bool validateMove(Point dest, List<List<Placeholder>> spaces)
        {
            Point movementCoordinates = base.getMovementCoordinates(origin, dest);
            bool validMove = false;

            if (Math.Abs(movementCoordinates.X) <= 1 && Math.Abs(movementCoordinates.Y) <= 1)
            {
                validMove = true;
            }
            //can move in every direction, but can not move more than one place. Also he can not make a move that would result in a checkmate or check. Also 
            // can switch places with tower once'castling'.
            return validMove;
        }

        public bool check(Point position)
        {
            return false;
        }

        public bool checkMate(Point position)
        {
            return false;
        }
    }
}
