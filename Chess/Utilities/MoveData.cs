using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utilities
{
    class MoveData
    {
        public readonly string moveInfo;
        public readonly int turn;

        public MoveData(string info, int counter)
        {
            moveInfo = info;
            turn = counter;
        }
    }
}
