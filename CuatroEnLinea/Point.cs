namespace CuatroEnLinea
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int[,] ToBidimensionalArray()
        {
            return new int[X, Y];
        }

        public override string ToString() => $"({X}, {Y})";
    }
}