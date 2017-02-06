using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    class Pacman : Sprite
    {
        public int[,] Map
        {
            set { map = value; }
        }
        private int[,] map;
        private int rows;
        private int columns;

        private int state = 0;
        private int iterations = 0;
        private readonly String pacman = "pacman";
        private String direction;

        private Vector2 prevPos;

        private readonly float defaultSpeed = 0.07f;

        private Dictionary<Keys, Vector2> directions;
        private Keys keyDown;

        SoundEffect pacmanState1;
        SoundEffect pacmanState2;

        private ContentManager content;

        private Game game;

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

        public Pacman(int[,] map, Game game)
        {
            this.map = map;
            rows = map.GetLength(0);
            columns = map.GetLength(1);

            Row = 17;
            Column = 13;

            this.game = game;

            directions = new Dictionary<Keys, Vector2>();
            directions[Keys.Down] = new Vector2(0, 1);
            directions[Keys.Up] = new Vector2(0, -1);
            directions[Keys.Left] = new Vector2(-1, 0);
            directions[Keys.Right] = new Vector2(1, 0);

            direction = "Droite";
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = Util.Vector(Row, Column);
            Direction = new Vector2(1, 0);
            Speed = defaultSpeed;
            prevPos = Position;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            this.content = content;
            //pacmanState1 = content.Load<SoundEffect>("PelletEat1");
            //pacmanState2 = content.Load<SoundEffect>("PelletEat2");
            base.LoadContent(content, assetName);
        }

        public override void Update(GameTime gameTime)
        {
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

                if (map[Row, Column] == 1)
                {
                    map[Row, Column]++;
                    game.scoreUp();
                }else if (map[Row, Column] == 3)
                {
                    map[Row, Column]--;
                    game.scoreUp();
                    game.scoreUp();
                    game.ghostEdible();
                }
            }

            if (directions.ContainsKey(keyDown) && directions[keyDown] != Direction)
            {
                Direction = directions[keyDown];
                switch (keyDown)
                {
                    case Keys.Down:
                        direction = "Bas";
                        break;
                    case Keys.Up:
                        direction = "Haut";
                        break;
                    case Keys.Right:
                        direction = "Droite";
                        break;
                    case Keys.Left:
                        direction = "Gauche";
                        break;
                }

            }

            Texture = content.Load<Texture2D>(pacman + direction + state);

            iterations++;
            if (iterations > 10)
            {
                state = 1;
                //pacmanState1.Play();
            }

            if (iterations > 20)
            {
                state = 0;
                iterations = 0;
                //pacmanState2.Play();
            }
            
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
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                keyDown = Keys.Down;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                keyDown = Keys.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                keyDown = Keys.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                keyDown = Keys.Left;
            }

            base.HandleInput(keyboardState, mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
    }
}
