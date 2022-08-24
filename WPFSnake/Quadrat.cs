using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFSnake
{
    public class Quadrat
    {

        public int X ;
        public int Y ;
        private int Hoehe= 10;
        private int Breite=  10;
        public int Index = -1;
        public Color Colour = Colors.Black;
        public Random Random = new();

        public Quadrat()
        {

        }
        public Quadrat(Canvas gamefield)
        {
            this.X = (int)gamefield.Width/2;
            this.Y = (int)gamefield.Height/2;
        }
        public Quadrat(int x, int y, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Colour = color;
        }

        public void Draw(Canvas gamefield)
        {
            Rectangle block = new();
            block.Height = Hoehe;
            block.Width = Breite;

            SolidColorBrush brush = new();
            brush.Color = Colour;
            block.Fill = brush;

            Canvas.SetLeft(block,X);
            Canvas.SetTop(block,Y);

            gamefield.Children.Add(block);
        }

        public void MoveTo(string direction, List<Quadrat> snakeBody)
        {
            for (int i = snakeBody.Count-1; i >= 0 ; i--)
            {
                if(i==0)
                {
                    switch (direction)
                    {
                        case ("oben"):
                            Y -= 10; ; break;
                        case ("unten"):
                            Y += 10; break;
                        case ("links"):
                            X -= 10; break;
                        case ("rechts"):
                            X += 10; break;
                        default:
                            break;
                    }
                }
                else
                {
                    snakeBody[i].X = snakeBody[i - 1].X;
                    snakeBody[i].Y = snakeBody[i - 1].Y;
                }
            }
        }
        public bool BorderCheck(Canvas gamefield, bool border)
        {
            if (border)
            {
                if (this.X + 10 > gamefield.Width)
                    return true;
                else if (this.Y + 10 > gamefield.Height)
                    return true;
                else if (this.X < 0)
                    return true;
                else if (this.Y < 0)
                    return true;
                return false;
            }
            else
            {
                if (this.X + 10 > gamefield.Width)
                    this.X = 0;
                else if (this.Y + 10 > gamefield.Height)
                    this.Y = 0;
                else if (this.X < 0)
                    this.X = (int)gamefield.Width - 10;
                else if (this.Y < 0)
                    this.Y = (int)gamefield.Height - 10;

                return false;
            }

        }

        public bool HitCheck(string direction, Letter letter)
        {
          if (this.Y == letter.Y && this.X == letter.X)
            {
                return true;
            }

            return false;
        }

    }

}