using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Horse : Unit
    {
        public Horse(int PIECE_COLOR, Point origin)
        {
            pieceType = "Horse";
            base.origin = origin;
            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 3;
                chessImage.Source = Utilities.Chesspieces.white_horse;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 9;
                chessImage.Source = Utilities.Chesspieces.black_horse;
            }
        }

        /**
         * Override method of base Unit class. Checks wether the move with the given destination
         * is a valid one.
         * */
        public override bool validateMove(Point dest, List<List<Placeholder>> spaces)
        {
            // make sure no invalid coordinates are given
            if (dest.X < 0 || dest.Y < 0) { return false; }

            // Can jump over other objects. Always eighter one or two places sideways and always eighter one or two places up/downwards. If sideways one, 
            //then up/down two places, if up/down one then sideways two places
            Point movementCoordinates = base.getMovementCoordinates(origin, dest);
            bool validMove = false;

            if (origin != dest)
            {
                if (base.validateDestination((Placeholder)spaces[(int)dest.X][(int)dest.Y]))
                {
                    if ((Math.Abs(movementCoordinates.X) == 1 && Math.Abs(movementCoordinates.Y) == 2) || (Math.Abs(movementCoordinates.X) == 2 && Math.Abs(movementCoordinates.Y) == 1))
                    {
                        // no need for piece collision, horse can jump over anything
                        validMove = true;
                    }
                }
            }
            
            return validMove;
        }
    }
}
