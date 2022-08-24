using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFSnake
{
    public class Letter
    {
        public int X;
        public int Y;
        private int Hoehe = 10;
        private int Breite = 10;
        public int Index = -1;
        public Color Colour = Colors.Red;
        public Random Random = new();

        public Letter(Canvas gamefield)
        {
            this.X = (int)gamefield.Width / 2;
            this.Y = (int)gamefield.Height / 2;
        }
        public Letter(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Colour = Colour;
        }

        public void Draw(Canvas gamefield)
        {
            Rectangle block = new();
            block.Height = Hoehe;
            block.Width = Breite;

            
            SolidColorBrush brush = new();
            brush.Color = Colour;
            block.Fill = brush;
            

            Canvas.SetLeft(block, X);
            Canvas.SetTop(block, Y);

            gamefield.Children.Add(block);
        }
    }
}
