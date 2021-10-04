using System;

namespace CuatroEnLinea
{
    class Program
    {
        static int _movementNumber = 1;
        static readonly Matrix _matrix = new Matrix();

        static void Main(string[] args)
        {
            Console.WriteLine("Cuatro en linea");
            var columnNumber = "";
            Mark currentMark = null;

            while (true)
            {
                var currentPlayer = Players.GetNextPlayer(_movementNumber);
                Console.Clear();
                Console.WriteLine(_matrix.Draw());

                Console.WriteLine($"Movimiento {_movementNumber} --- {currentPlayer} is your turn. Enter column number:");

                var validInput = false;
                while (!validInput)
                {
                    columnNumber = Console.ReadLine();

                    if (!ValidateInput(columnNumber))
                        Console.WriteLine($"Wrong input!. {currentPlayer} is your turn. Enter column number:");
                    else
                    {
                        var row = _matrix.GetRow(int.Parse(columnNumber));

                        currentMark = new Mark(int.Parse(columnNumber),
                                               row,
                                               currentPlayer);

                        if (ValidatePositionAlreadyInUse(currentMark))
                            Console.WriteLine($"Wrong input!. Already in use. {currentPlayer} is your turn. Enter column number:");
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
            if (string.IsNullOrEmpty(input))
                return false;

            if (!int.TryParse(input, out int x))
                return false;

            if (x < 0 || x > Matrix.MATRIX_COLUMN_LENGTH)
                return false;

            return true;
        }
    }
}