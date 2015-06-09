using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Tower : Unit
    {
        public Tower(int PIECE_COLOR, Point origin)
        {
            pieceType = "Tower";
            base.origin = origin;
            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 2;
                chessImage.Source = Utilities.Chesspieces.white_tower;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 8;
                chessImage.Source = Utilities.Chesspieces.black_tower;
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

            Point movementCoordinates = base.getMovementCoordinates(origin, dest);
            bool validMove = false;

            if (origin != dest)
            {
                if ((movementCoordinates.X != 0 && movementCoordinates.Y == 0) || (movementCoordinates.X == 0 && movementCoordinates.Y != 0))
                {
                    // movement is allowed, now check if the target destination contains a piece of your own
                    // if so, the action is not allowed
                    if (base.validateDestination((Placeholder)spaces[(int)dest.X][(int)dest.Y]))
                    {
                        if (!Chess.Controls.Chessboard.pieceCollision(origin, dest, movementCoordinates))
                        {
                            validMove = true;
                        }
                    }
                }
            }
            return validMove;
            //Only horizontal and vertical movement allowed. If piece between start and end, invalid move. 
        }
    }
}
