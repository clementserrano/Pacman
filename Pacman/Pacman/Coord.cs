﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public class Coord
    {
        public int X, Y;

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord(Vector2 pos)
        {
            X = (int)(pos.X / 20);
            Y = (int)(pos.Y / 20);
        }

        // on surcharge l’opérateur == pour l’égalité entre les coordonnées 
        public static Boolean operator ==(Coord c1, Coord c2)
        {
            return ((c1.X == c2.X) && (c1.Y == c2.Y));
        }
        // on surcharge l’opérateur == pour la différence entre les  coordonnées 
        public static Boolean operator !=(Coord c1, Coord c2)
        {
            return ((c1.X != c2.X) || (c1.Y != c2.Y));
        }
    }

}
