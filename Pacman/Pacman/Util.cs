using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Util
    {
        public static Vector2 Vector(int row, int column)
        {
            return new Vector2(column * 20, row * 20);
        }

        public static int Row(Vector2 position)
        {
            return (int)(position.Y / 20);
        }

        public static int Column(Vector2 position)
        {
            return (int)(position.X / 20);
        }
    }
}
