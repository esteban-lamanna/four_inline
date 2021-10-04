namespace CuatroEnLinea
{
    public static class Players
    {
        public static Player PlayerOne { get; set; } = new Player(1);

        public static Player PlayerTwo { get; set; } = new Player(2);


        public static Player GetNextPlayer(int muvementNumber)
        {
            var resto = muvementNumber % 2;

            if (resto == 1)
                return PlayerOne;

            return PlayerTwo;
        }
    }

    public class Player
    {
        public Player(int numero)
        {
            Numero = numero;
        }
        public int Numero { get; set; }

        public override string ToString()
        {
            return $"Player {Numero}";
        }
    }
}