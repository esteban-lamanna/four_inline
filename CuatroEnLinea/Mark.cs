namespace CuatroEnLinea
{
    public class Mark : Point
    {
        public Mark(int x, int y, Player player) : base(x, y)
        {
            Player = player;
        }

        public Player Player { get; set; }
    }
}