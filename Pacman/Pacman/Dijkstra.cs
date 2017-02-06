using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Dijkstra
    {
        public static string Direction(Coord dep, Coord arr, int[,] map)
        {
            int rows = map.GetLength(0);
            int columns = map.GetLength(1);

            Sommet[,] sommets = new Sommet[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (map[i, j] != 0)
                        sommets[i, j] = new Sommet();
                }
            }

            sommets[arr.X, arr.Y].Potentiel = 0;
            Coord current = arr;

            //Algo de recherche
            while (current != dep)
            {
                Sommet z = sommets[current.X, current.Y];
                z.Marque = true;

                //Haut
                if (current.Y > 0)
                {
                    if (sommets[current.X, current.Y - 1] != null)
                    {
                        Sommet s = sommets[current.X, current.Y - 1];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                //Bas
                if (current.Y + 1 < columns)
                {
                    if (sommets[current.X, current.Y + 1] != null)
                    {
                        Sommet s = sommets[current.X, current.Y + 1];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                //Gauche
                if (current.X > 0)
                {
                    if (sommets[current.X - 1, current.Y] != null)
                    {
                        Sommet s = sommets[current.X - 1, current.Y];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                //Droite
                if (current.X + 1 < rows)
                {
                    if (sommets[current.X + 1, current.Y] != null)
                    {
                        Sommet s = sommets[current.X + 1, current.Y];
                        if (s.Potentiel > z.Potentiel + 1)
                        {
                            s.Potentiel = z.Potentiel + 1;
                            s.Pred = current;
                        }

                    }
                }

                int min = Sommet.INFINI;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (sommets[i, j] != null)
                        {
                            if (!sommets[i, j].Marque && sommets[i, j].Potentiel < min)
                            {
                                min = sommets[i, j].Potentiel;
                                current = new Coord(i, j);
                            }
                        }
                    }
                }
            }
            Coord next = sommets[current.X, current.Y].Pred;
            string res = "";
            if (next.X != dep.X)
                if (next.X > dep.X)
                    res = "right";
                else
                    res = "left";
            else
                if (next.Y > dep.Y)
                res = "down";
            else
                res = "up";

            return res;
        }

    }
}
