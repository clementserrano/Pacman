using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Ghost : Sprite
    {
        private int[,] map;
        private int rows;
        private int columns;

        private Pacman pacman;

        private readonly float defaultSpeed = 0.09f;
        private Vector2 prevPos;

        private int prevRow;
        private int prevColumn;
        static Random rnd = new Random();
        private int iter;
        private Dictionary<Keys, Vector2> directions;

        private Game game;

        public int State
        {
            get { return state; }
            set { state = value; }
        }
        private int state;

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }
        private int _row;

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }
        private int _column;

        public Ghost(int[,] map, Pacman pacman, Game game)
        {
            this.map = map;
            rows = map.GetLength(0);
            columns = map.GetLength(1);

            this.pacman = pacman;
            prevRow = 0;
            prevColumn = 0;
            iter = 0;
            state = 0;

            this.game = game;

            directions = new Dictionary<Keys, Vector2>();
            directions[Keys.Down] = new Vector2(0, 1);
            directions[Keys.Up] = new Vector2(0, -1);
            directions[Keys.Left] = new Vector2(-1, 0);
            directions[Keys.Right] = new Vector2(1, 0);
        }

        public override void Initialize()
        {
            base.Initialize();
            Direction = new Vector2(1, 0);
            Speed = defaultSpeed;
            prevPos = Position;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
        }

        public override void Update(GameTime gameTime)
        {
            // Check if the ghost has caught the pacman
            if (pacman.Row == Row && pacman.Column == Column)
            {
                if (state == 0)
                {
                    pacman.Position = Util.Vector(17, 13);
                    game.loseLife();
                }
                else
                {
                    Position = Util.Vector(14, 13);
                }
            }

            Vector2 upLeftPosition = new Vector2(Position.X + 3, Position.Y + 3);
            Vector2 upRightPosition = new Vector2(Position.X + 17, Position.Y + 3);
            Vector2 downLeftPosition = new Vector2(Position.X + 3, Position.Y + 17);
            Vector2 downRightPosition = new Vector2(Position.X + 17, Position.Y + 17);

            int row1 = 0;
            int column1 = 0;
            int row2 = 0;
            int column2 = 0;

            if (Direction == directions[Keys.Down])
            {
                row1 = Util.Row(downLeftPosition);
                column1 = Util.Column(downLeftPosition);
                row2 = Util.Row(downRightPosition);
                column2 = Util.Column(downRightPosition);
            }
            else if (Direction == directions[Keys.Up])
            {
                row1 = Util.Row(upLeftPosition);
                column1 = Util.Column(upLeftPosition);
                row2 = Util.Row(upRightPosition);
                column2 = Util.Column(upRightPosition);
            }
            else if (Direction == directions[Keys.Right])
            {
                row1 = Util.Row(upRightPosition);
                column1 = Util.Column(upRightPosition);
                row2 = Util.Row(downRightPosition);
                column2 = Util.Column(downRightPosition);
            }
            else if (Direction == directions[Keys.Left])
            {
                row1 = Util.Row(upLeftPosition);
                column1 = Util.Column(upLeftPosition);
                row2 = Util.Row(downLeftPosition);
                column2 = Util.Column(downLeftPosition);
            }

            if (row1 >= rows) row1--;
            if (column1 >= columns) column1--;
            if (row2 >= rows) row2--;
            if (column2 >= columns) column2--;

            if (map[row1, column1] == 0 || map[row2, column2] == 0)
            {
                Speed = 0;
                Position = prevPos;
            }
            else
            {
                Speed = defaultSpeed;
                prevPos = Position;
                Row = row1;
                Column = column1;
            }
            
            /*// Dijkstra direction
            if (prevRow != Row || prevColumn != Column)
            {
                string direction = Dijkstra.Direction(new Coord(Row, Column), new Coord(pacman.Row, pacman.Column), map);
                Vector2 newDirection = Direction;
                switch (direction)
                {
                    case "down":
                        newDirection = new Vector2(0, 1);
                        break;
                    case "up":
                        newDirection = new Vector2(0, -1);
                        break;
                    case "right":
                        newDirection = new Vector2(1, 0);
                        break;
                    case "left":
                        newDirection = new Vector2(-1, 0);
                        break;
                }

                if (newDirection != Direction) Direction = newDirection;
            }*/

            // random direction
            if (iter % 20 == 0) {
                Vector2 newDirection = Direction;
                int r = rnd.Next(directions.Values.Count);
                newDirection = directions.Values.ElementAt(r);
                if (newDirection != Direction) Direction = newDirection;
            }
            iter++;

            prevRow = Row;
            prevColumn = Column;

            if (Position.X < -8)
            {
                Position = new Vector2(columns * 20 - 12, Position.Y);
                Column += columns - 1;
            }
            else if (Position.X > columns * 20 - 12)
            {
                Position = new Vector2(-8, Position.Y);
                Column = 0;
            }

            base.Update(gameTime);
        }

        public override void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {
            base.HandleInput(keyboardState, mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
    }
}
