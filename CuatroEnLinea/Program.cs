using System;
using System.Linq;

namespace CuatroEnLinea
{
    class Program
    {
        static int _movementNumber = 1;
        static readonly Matrix _matrix = new Matrix();

        static void Main(string[] args)
        {
            Console.WriteLine("Probando cuatro en linea");
            var input = "";
            Mark currentMark = null;

            while (true)
            {
                var currentPlayer = Players.GetNextPlayer(_movementNumber);

                Console.WriteLine($"Movimiento {_movementNumber} --- {currentPlayer}  Is your turn. Entry Format: n,n. 0<=n<=2");

                var validInput = false;
                while (!validInput)
                {
                    input = Console.ReadLine();

                    if (!ValidateInput(input))
                        Console.WriteLine($"Wrong input!. {currentPlayer}  Is your turn. Entry Format: n,n. 0<=n<=2");
                    else
                    {
                        currentMark = new Mark(int.Parse(input.Split(",").First()),
                                               int.Parse(input.Split(",").Last()),
                                               currentPlayer);

                        if (ValidatePositionAlreadyInUse(currentMark))
                            Console.WriteLine($"Wrong input!. Already in use. {currentPlayer} Is your turn. Entry Format: n,n. 0<=n<=2");
                        else
                            validInput = true;
                    }
                }

                PutMarkInMatrix(currentMark);

                Console.WriteLine(_matrix.Draw());

                var winner = DetectWinner(currentPlayer);

                if (winner)
                {
                    Console.WriteLine($"Winner {currentPlayer}.");
                    break;
                }

                _movementNumber++;
            }
        }

        private static bool DetectWinner(Player player)
        {
            if (_movementNumber < 5)
                return false;

            var marks = _matrix.GetPlayerMarks(player);

            return _matrix.ComparePlayerMarksWithPossibleWinningCombination(player, marks);
        }

        private static void PutMarkInMatrix(Mark mark)
        {
            _matrix.PutMarkInMatrix(mark);
        }

        private static bool ValidatePositionAlreadyInUse(Mark currentMark)
        {
            return _matrix.ValidatePositionAlreadyInUse(currentMark);
        }

        private static bool ValidateInput(string input)
        {
            var splitted = input.Split(",");

            if (splitted.Count() == 0)
                return false;

            if (!int.TryParse(splitted.First(), out int x))
                return false;

            if (x < 0 || x > Matrix.MATRIX_COLUMN_LENGTH)
                return false;

            if (!int.TryParse(splitted.Last(), out int y))
                return false;

            if (y < 0 || y > Matrix.MATRIX_COLUMN_LENGTH)
                return false;

            return true;
        }
    }
}