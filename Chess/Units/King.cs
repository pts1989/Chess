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

        public King(int PIECE_COLOR, Point origin)
        {
            pieceType = "King";
            base.origin = origin;
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
            // make sure no invalid coordinates are given
            if (dest.X < 0 || dest.Y < 0 || dest.X > 7 || dest.Y > 7) { return false; }

            Point movementCoordinates = base.getMovementCoordinates(origin, dest);
            bool validMove = false;

            if (origin != dest)
            {
                if (base.validateDestination((Placeholder)spaces[(int)dest.X][(int)dest.Y]))
                {
                    if (Math.Abs(movementCoordinates.X) <= 1 && Math.Abs(movementCoordinates.Y) <= 1)
                    {
                        // see if the move creates a 'check' status, delegates to checkmate, which would
                        // be worse, any case, 'check' is not allowed
                        if (!check(dest, spaces))
                        {
                            validMove = true;
                        }
                    }
                }
            }
            //can move in every direction, but can not move more than one place. Also he can not make a move that would result in a checkmate or check. Also 
            // can switch places with tower once'castling'.
            return validMove;
        }

        /**
         * Returns a bool based on wether the king has 'check' status
         * can be called on attempting to move the king, or from chessboard
         * determining if a king is checked after the other player moved a piece
         * */
        public bool check(Point position, List<List<Placeholder>> spaces)
        {
            int pawn, bishop, tower, horse, queen, king;
            switch (pieceID)
            {
                case 6: // check against black pieces
                    pawn = 7;
                    tower = 8;
                    horse = 9;
                    bishop = 10;
                    queen = 11;
                    king = 12;
                    break;
                case 12: // check against white pieces
                    pawn = 1;
                    tower = 2;
                    horse = 3;
                    bishop = 4;
                    queen = 5;
                    king = 6;
                    break;
                default:
                    return false;
            }

            #region pawns
            // first check any pawns in close proximity, diagonally, only in front of the king
            int y = 0;
            if (pieceID == 6) // only check spaces > position.Y
            {
                y = 1;
            }else if(pieceID == 12){
                y = -1;
            }
            if (spaces[(int)position.X + 1][(int)position.Y + y].chessPiece != null)
            {
                Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y + y].chessPiece;
                if (piece.pieceID == pawn)
                {
                    return true;
                }
            }
            if (spaces[(int)position.X - 1][(int)position.Y + y].chessPiece != null)
            {
                Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y + y].chessPiece;
                if (piece.pieceID == pawn)
                {
                    return true;
                }
            }
#endregion

            #region tower/queen
            // next, check for towers/queen
            // no towers/queen should be in a straight line with the king horizontally or 
            // vertically, without any other pieces in between
            for (int i = (int)position.X+1; i < spaces.Count; i++)
            {
                Unit piece = (Unit)spaces[i][(int)position.Y].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break; // any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else 
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.X-1; i >= 0; i--)
            {
                Unit piece = (Unit)spaces[i][(int)position.Y].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.Y+1; i < spaces[0].Count; i++)
            {
                Unit piece = (Unit)spaces[(int)position.X][i].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.Y-1; i >= 0; i--)
            {
                Unit piece = (Unit)spaces[(int)position.X][i].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
#endregion

            #region horse
            // Next, check for horse
            // start by checking wether there's any possibility of horses being in range
            if ((position.X + 2) < spaces.Count)
            { // check the RIGHT side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 2][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1)  >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 2][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            if ((position.X - 2) >= 0)
            { // check the LEFT side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 2][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 2][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            if ((position.Y + 2) < spaces[0].Count)
            { // check the UPPER side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y + 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y + 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            if ((position.Y - 2) > 0)
            { // check the LOWER side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y - 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y - 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            #endregion

            #region bishop/queen
            // next, check for bishop/queen
            // Look at all diagonal spaces
            int posY = (int)position.Y+1;
            for (int i = (int)position.X+1; i < spaces.Count; i++)
            {   // check north-east first
                if (posY < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY++;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
            }
            posY = (int)position.Y + 1;
            for (int i = (int)position.X - 1; i >= 0; i--)
            {   // check north-west
                if (posY < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY++;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
                
            }
            posY = (int)position.Y - 1;
            for (int i = (int)position.X - 1; i >= 0; i--)
            {   // check south-west
                if (posY >= 0)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY--;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
                
            }
            posY = (int)position.Y - 1;
            for (int i = (int)position.X + 1; i < spaces.Count; i++)
            {   // check south-east
                if (posY >= 0)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY--;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
            }
            #endregion

            #region king
            // now check the king, validate all spaces around it
            for (int i = -1; i <= 1; i++)
            {
                Unit piece;
                if ((int)position.X - 1 >= 0) // check middle
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y].chessPiece;
                    if (piece != null && piece.pieceID == king) { return true; }
                }
                if ((int)position.Y + 1 < spaces[0].Count) // check top
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == king) { return true; }
                }
                if ((int)position.Y - 1 >= 0) // check bottom
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == king) { return true; }
                }
            }

            #endregion
            return false;
        }

        /**
         * Returns a List containing the attacking units
         * can be called when you need to know what enemy units pose a threat
         * */
        public List<Unit> getCurrentAttackingUnits(Point position, List<List<Placeholder>> spaces)
        {
            int pawn, bishop, tower, horse, queen, king;
            List<Unit> danger_Pieces = new List<Unit>();
            switch (pieceID)
            {
                case 6: // check against black pieces
                    pawn = 7;
                    tower = 8;
                    horse = 9;
                    bishop = 10;
                    queen = 11;
                    king = 12;
                    break;
                case 12: // check against white pieces
                    pawn = 1;
                    tower = 2;
                    horse = 3;
                    bishop = 4;
                    queen = 5;
                    king = 6;
                    break;
                default:
                    return new List<Unit>();
            }

            #region pawns
            // first check any pawns in close proximity, diagonally, only in front of the king
            int y = 0;
            if (pieceID == 6) // only check spaces > position.Y
            {
                y = 1;
            }
            else if (pieceID == 12)
            {
                y = -1;
            }
            if (spaces[(int)position.X + 1][(int)position.Y + y].chessPiece != null)
            {
                Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y + y].chessPiece;
                if (piece.pieceID == pawn)
                {
                    danger_Pieces.Add(piece);
                }
            }
            if (spaces[(int)position.X - 1][(int)position.Y + y].chessPiece != null)
            {
                Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y + y].chessPiece;
                if (piece.pieceID == pawn)
                {
                    danger_Pieces.Add(piece);
                }
            }
            #endregion

            #region tower/queen
            // next, check for towers/queen
            // no towers/queen should be in a straight line with the king horizontally or 
            // vertically, without any other pieces in between
            for (int i = (int)position.X + 1; i < spaces.Count; i++)
            {
                Unit piece = (Unit)spaces[i][(int)position.Y].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break; // any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    danger_Pieces.Add(piece); // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.X - 1; i >= 0; i--)
            {
                Unit piece = (Unit)spaces[i][(int)position.Y].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    danger_Pieces.Add(piece); // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.Y + 1; i < spaces[0].Count; i++)
            {
                Unit piece = (Unit)spaces[(int)position.X][i].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    danger_Pieces.Add(piece); // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.Y - 1; i >= 0; i--)
            {
                Unit piece = (Unit)spaces[(int)position.X][i].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    danger_Pieces.Add(piece); // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            #endregion

            #region horse
            // Next, check for horse
            // start by checking wether there's any possibility of horses being in range
            if ((position.X + 2) < spaces.Count)
            { // check the RIGHT side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 2][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 2][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
            }
            if ((position.X - 2) >= 0)
            { // check the LEFT side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 2][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 2][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
            }
            if ((position.Y + 2) < spaces[0].Count)
            { // check the UPPER side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y + 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y + 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
            }
            if ((position.Y - 2) > 0)
            { // check the LOWER side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y - 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y - 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        danger_Pieces.Add(piece);
                    }
                }
            }
            #endregion

            #region bishop/queen
            // next, check for bishop/queen
            // Look at all diagonal spaces
            int posY = (int)position.Y + 1;
            for (int i = (int)position.X + 1; i < spaces.Count; i++)
            {   // check north-east first
                if (posY < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        danger_Pieces.Add(piece); // queen or bishop
                    }
                    else
                    {
                        posY++;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
            }
            posY = (int)position.Y + 1;
            for (int i = (int)position.X - 1; i >= 0; i--)
            {   // check north-west
                if (posY < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        danger_Pieces.Add(piece); // queen or bishop
                    }
                    else
                    {
                        posY++;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }

            }
            posY = (int)position.Y - 1;
            for (int i = (int)position.X - 1; i >= 0; i--)
            {   // check south-west
                if (posY >= 0)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        danger_Pieces.Add(piece); // queen or bishop
                    }
                    else
                    {
                        posY--;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }

            }
            posY = (int)position.Y - 1;
            for (int i = (int)position.X + 1; i < spaces.Count; i++)
            {   // check south-east
                if (posY >= 0)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        danger_Pieces.Add(piece); // queen or bishop
                    }
                    else
                    {
                        posY--;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
            }
            #endregion

            #region king
            // now check the king, validate all spaces around it
            for (int i = -1; i <= 1; i++)
            {
                Unit piece;
                if ((int)position.X - 1 >= 0) // check middle
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y].chessPiece;
                    if (piece != null && piece.pieceID == king) { danger_Pieces.Add(piece); }
                }
                if ((int)position.Y + 1 < spaces[0].Count) // check top
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == king) { danger_Pieces.Add(piece); }
                }
                if ((int)position.Y - 1 >= 0) // check bottom
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == king) { danger_Pieces.Add(piece); }
                }
            }

            #endregion
            return danger_Pieces;
        }


        /**
         * Returns a bool based on wether the king is in checkmate status.
         * Should be called after every turn to check if the game has ended.
         * */
        public bool checkMate(Point position, List<List<Placeholder>> spaces)
        {
            List<int> units = new List<int>();
            switch (pieceID)// check for friendly units
            {
                case 6: 
                    units.Add(1);
                    units.Add(2);
                    units.Add(3);
                    units.Add(4);
                    units.Add(5);
                    units.Add(6);
                    break;
                case 12: 
                    
                    units.Add(7);
                    units.Add(8);
                    units.Add(9);
                    units.Add(10);
                    units.Add(11);
                    units.Add(12);
                    break;
                default:
                    return false;
            }

            List<bool> availableMoves = new List<bool>();
            List<Unit> attacking_units = getCurrentAttackingUnits(position, spaces);
            if (attacking_units.Count > 0)
            {
                // confirmed that this king is 'check'
                // now verify every move this king kan do and see if still checked.
                // now check the king, validate all spaces around it
                for (int i = -1; i <= 1; i++) // check spaces from x -1 till x + 1
                {
                    availableMoves.Add(validateMove(new Point((int)position.X + i, (int)position.Y + 1),spaces));   // top
                    availableMoves.Add(validateMove(new Point((int)position.X + i, (int)position.Y), spaces));      // middle
                    availableMoves.Add(validateMove(new Point((int)position.X + i, (int)position.Y -1), spaces));   // bottom
                }

                if (!availableMoves.Any(t => t == true)) { 
                    // if that doesn't solve the problem, check if other pieces can block
                    // first get the spaces that need to be blocked in order to resolve
                    // a piece being able to attack the king.
                    // try, for each available piece, to move to these positions. if any of them
                    // are legal moves, it's not checkmate

                    // first get a list of Points that correspond with all the spaces that need a either a take-over
                    // or a block(unit between enemy and king)
                    List<Point> availableSpaces = new List<Point>();
                    foreach (Unit unit in attacking_units) // loop through all attacking units and register the points they need to travel to the king
                    {
                        availableSpaces.Add(unit.origin); // add space of unit itself

                        if (unit.pieceID == 1 || unit.pieceID == 7) // Pawn
                        {
                            // the origin of this piece is enough
                        }


                        if (unit.pieceID == 2 || unit.pieceID == 8 || unit.pieceID == 5 || unit.pieceID == 11) // Tower & queen
                        {
                            bool hasFound = false;
                            if (unit.origin.X == this.origin.X) // vertically
                            {
                                for (int i = 0; i < spaces.Count; i++)
                                {
                                    Unit piece = (Unit)spaces[(int)origin.X][i].chessPiece;
                                    if (piece == this)
                                    {   // our king
                                        hasFound = !hasFound;
                                    }
                                    else if (piece == unit)
                                    { // enemy unit
                                        hasFound = !hasFound;
                                    }
                                    else if (hasFound)
                                    {
                                        availableSpaces.Add(new Point(origin.X, i));
                                    }
                                }
                            }
                            else if (unit.origin.Y == this.origin.Y) // horizontally
                            {
                                for (int i = 0; i < spaces.Count; i++)
                                {
                                    Unit piece = (Unit)spaces[i][(int)origin.Y].chessPiece;
                                    if (piece == this)
                                    {   // our king
                                        hasFound = !hasFound;
                                    }
                                    else if (piece == unit)
                                    { // enemy unit
                                        hasFound = !hasFound;
                                    }
                                    else if (hasFound)
                                    {
                                        availableSpaces.Add(new Point(i, origin.Y));
                                    }
                                }
                            }
                        }


                        if (unit.pieceID == 3 || unit.pieceID == 9) // Horse
                        {
                            // horse origin destination has already been added
                            // this is also the only way to stop a horse from beating the king
                        }


                        if (unit.pieceID == 4 || unit.pieceID == 10 || unit.pieceID == 5 || unit.pieceID == 11) // Bishop & queen
                        {
                            if ((unit.origin.X < this.origin.X && unit.origin.Y > this.origin.Y) || (unit.origin.X > this.origin.X && unit.origin.Y < this.origin.Y))
                            {
                                // determine top left
                                Point topleft_pos = this.origin;
                                while ((topleft_pos.X > 0 && topleft_pos.Y < 8))
                                {
                                    topleft_pos = new Point(topleft_pos.X - 1, topleft_pos.Y + 1);
                                }

                                bool hasFound = false;
                                while (topleft_pos.X < 8 && topleft_pos.Y > 0)
                                {
                                    Unit piece = (Unit)spaces[(int)topleft_pos.X][(int)topleft_pos.Y].chessPiece;
                                    if (piece == unit)
                                    { // found the unit
                                        hasFound = !hasFound;
                                    }
                                    else if (piece == this)
                                    {   // found the king
                                        hasFound = !hasFound;
                                    }
                                    else if (hasFound)
                                    {
                                        availableSpaces.Add(new Point(topleft_pos.X, topleft_pos.Y));
                                    }
                                    topleft_pos = new Point(topleft_pos.X + 1, topleft_pos.Y - 1);
                                }

                            }
                            else if ((unit.origin.X > this.origin.X && unit.origin.Y > this.origin.Y) || (unit.origin.X < this.origin.X && unit.origin.Y < this.origin.Y))
                            {
                                // determine bottom left
                                Point bottom_left = this.origin;
                                while ((bottom_left.X > 0 && bottom_left.Y > 0))
                                {
                                    bottom_left = new Point(bottom_left.X - 1, bottom_left.Y - 1);
                                }

                                bool hasFound = false;
                                while (bottom_left.X < 8 && bottom_left.Y < 8)
                                {
                                    Unit piece = (Unit)spaces[(int)bottom_left.X][(int)bottom_left.Y].chessPiece;
                                    if (piece == unit)
                                    { // found the unit
                                        hasFound = !hasFound;
                                    }
                                    else if (piece == this)
                                    {   // found the king
                                        hasFound = !hasFound;
                                    }
                                    else if (hasFound)
                                    {
                                        availableSpaces.Add(new Point(bottom_left.X, bottom_left.Y));
                                    }
                                    bottom_left = new Point(bottom_left.X + 1, bottom_left.Y + 1);
                                }
                            }
                        }


                        if (unit.pieceID == 6 || unit.pieceID == 12) // King
                        {
                            // should not happen, as placing a king near a king will result
                            // in a check for both players. SHOULD be filtered out as soon as one player
                            // tries to place his king next to the other
                        }
                    }



                    // now the easy part, for each friendly unit, check if they can make any of the moves to the availableSpaces we just calculated
                    foreach (List<Placeholder> control in spaces) 
                    {
                        foreach (Placeholder space in control) // loop through every space
                        {
                            Unit piece = (Unit)space.chessPiece;

                            if (piece != null && units.Contains(piece.pieceID)) // it's a friendly piece
                            {
                                foreach (Point p in availableSpaces)
                                {
                                    if (piece.validateMove(p, spaces))
                                    {
                                        return false;   // this move is validated, so king is NOT checkmate
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else { return false; } // king not even checked, so certainly not checkmate

            return !availableMoves.Any(t => t == true);
        }
    }
}
