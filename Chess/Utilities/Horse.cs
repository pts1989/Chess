using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chess.Utilities
{
    class Horse : Image
    {
        public int pieceId;
        public Point current;
        List<Point> allowed_targets;
        //void update AllowedTargets();
        public bool makenMove(Point source, Point target)
        {
            //Point movementCoordinates = _2_P_Game.getMovementCoordinates(source, target);

            if (allowed_targets.Contains(target))
            {
                current = target;
                return true;
            }

            return false;
        }
    }
}
