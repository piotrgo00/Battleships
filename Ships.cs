using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Ships
    {
    }
    class Carrier : Ships
    {
        public const int size = 5;
    }
    class Battleship : Ships
    {
        public const int size = 4;
    }
    class Cruiser : Ships
    {
        public const int size = 3;
    }
    class Submarine : Ships
    {
        public const int size = 2;
    }
    class Destroyer : Ships
    {
        public const int size = 1;
    }
}
