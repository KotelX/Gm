using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PytGame2000
{
    public class SpacePosition : Position
    {
        public static void SetNewSpacePosition(Position position)
        {
            Position = position;
        }
    public static Position Position { get; private set; }
    }
}
