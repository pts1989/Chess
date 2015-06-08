using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Queen : Unit
    {
        public Queen(int PIECE_COLOR, Point origin)
        {
            pieceType = "Queen";
            base.origin = origin;
            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 5;
                chessImage.Source = Utilities.Chesspieces.white_queen;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 11;
                chessImage.Source = Utilities.Chesspieces.black_queen;
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

            if (origin != dest)
            {
                if (base.validateDestination((Placeholder)spaces[(int)dest.X][(int)dest.Y]))
                {
                    // can move in every direction. If piece between start and end, invalid move. 
                    if ((Math.Abs(movementCoordinates.X) <= 1 && Math.Abs(movementCoordinates.Y) <= 1)
                        || (Math.Abs(movementCoordinates.X) == Math.Abs(movementCoordinates.Y))
                        || ((movementCoordinates.X != 0 && movementCoordinates.Y == 0) || (movementCoordinates.X == 0 && movementCoordinates.Y != 0)))
                    {
                        if (!Chess.Controls.Chessboard.pieceCollision(origin, dest, movementCoordinates))
                        {
                            validMove = true;
                        }
                    }
                }
            }
            return validMove;
        }
    }
}
