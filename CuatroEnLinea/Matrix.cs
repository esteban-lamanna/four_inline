using System.Collections.Generic;
using System.Text;

namespace CuatroEnLinea
{
    public class Matrix
    {
        public static int MATRIX_COLUMN_LENGTH = 7;
        public static int CANTIDAD_CONSECUTIVA_EN_LINEA = 4;
        public Mark[,] Grid = new Mark[MATRIX_COLUMN_LENGTH + 1, MATRIX_COLUMN_LENGTH + 1];


        public string Draw()
        {
            var blder = new StringBuilder();

            blder.Append($"          ");

            for (int x = 0; x <= MATRIX_COLUMN_LENGTH; x++)
            {
                blder.Append($"Col {x} ");
            }

            for (int y = 0; y <= MATRIX_COLUMN_LENGTH; y++)
            {
                blder.AppendLine();

                blder.Append($"Line {y}  |  ");

                for (int x = 0; x <= MATRIX_COLUMN_LENGTH; x++)
                {
                    if (x > 0)
                        blder.Append($"   ");

                    var item = Grid[x, y];

                    if (item == null)
                    {
                        blder.Append($"   ");
                        continue;
                    }

                    if (item.Player == Players.PlayerOne)
                    {
                        blder.Append($" X ");
                        continue;
                    }

                    blder.Append($" O ");
                }
            }

            return blder.ToString();
        }

        internal int GetRow(int xNumber)
        {
            for (int y = MATRIX_COLUMN_LENGTH; y >= 0; y--)
            {
                if (!ElementHasValue(xNumber, y))
                    return y;
            }

            return MATRIX_COLUMN_LENGTH;
        }

        private bool ElementHasValue(int x, int y)
        {
            if (x > MATRIX_COLUMN_LENGTH || y > MATRIX_COLUMN_LENGTH || x < 0 || y < 0)
                return false;

            var item = Grid[x, y];

            return item != null;
        }

        public bool ValidatePositionAlreadyInUse(Mark currentMark)
        {
            var currentValue = Grid[currentMark.X, currentMark.Y];

            return currentValue != null;
        }

        public bool ComparePlayerMarksWithPossibleWinningCombination(Player currentPlayer, IList<Mark> marks)
        {
            if (DetectarHorizontales(currentPlayer))
                return true;

            if (DetectarVerticales(currentPlayer))
                return true;

            if (DetectarDiagonales(currentPlayer))
                return true;

            return false;
        }

        private bool DetectarVerticales(Player player)
        {
            int found = 0;
            for (int x = 0; x <= MATRIX_COLUMN_LENGTH; x++)
            {
                for (int y = 0; y <= MATRIX_COLUMN_LENGTH; y++)
                {
                    var item = Grid[x, y];

                    if (item == null)
                    {
                        found = 0;
                        continue;
                    }

                    if (item.Player != player)
                    {
                        found = 0;
                        continue;
                    }

                    found++;

                    if (found == CANTIDAD_CONSECUTIVA_EN_LINEA)
                        return true;
                }
            }

            return false;
        }

        private bool DetectarDiagonales(Player player)
        {
            for (int x = 0; x <= MATRIX_COLUMN_LENGTH; x++)
            {
                for (int y = 0; y <= MATRIX_COLUMN_LENGTH; y++)
                {
                    var inlineRigthDown = ElementHasValue(x, y, player) &&
                                 ElementHasValue(x + 1, y + 1, player) &&
                                 ElementHasValue(x + 2, y + 2, player) &&
                                 ElementHasValue(x + 3, y + 3, player);

                    if (inlineRigthDown)
                        return true;

                    var inlineDownLeft = ElementHasValue(x, y, player) &&
                                 ElementHasValue(x - 1, y - 1, player) &&
                                 ElementHasValue(x - 2, y - 2, player) &&
                                 ElementHasValue(x - 3, y - 3, player);

                    if (inlineDownLeft)
                        return true;
                }
            }

            return false;
        }


        private bool ElementHasValue(int x, int y, Player player)
        {
            if (x > MATRIX_COLUMN_LENGTH || y > MATRIX_COLUMN_LENGTH || x < 0 || y < 0)
                return false;

            var item = Grid[x, y];

            if (item == null)
                return false;

            return item.Player == player;
        }

        private bool DetectarHorizontales(Player player)
        {
            int found = 0;
            for (int y = 0; y <= MATRIX_COLUMN_LENGTH; y++)
            {
                for (int x = 0; x <= MATRIX_COLUMN_LENGTH; x++)
                {
                    var item = Grid[x, y];

                    if (item == null)
                    {
                        found = 0;
                        continue;
                    }

                    if (item.Player != player)
                    {
                        found = 0;
                        continue;
                    }

                    found++;

                    if (found == CANTIDAD_CONSECUTIVA_EN_LINEA)
                        return true;
                }
            }

            return false;
        }

        public void PutMarkInMatrix(Mark mark)
        {
            Grid[mark.X, mark.Y] = mark;
        }

        public IList<Mark> GetPlayerMarks(Player player)
        {
            var list = new List<Mark>();

            for (int x = 0; x <= MATRIX_COLUMN_LENGTH; x++)
            {
                for (int y = 0; y <= MATRIX_COLUMN_LENGTH; y++)
                {
                    var currentItem = Grid[x, y];
                    if (currentItem == null)
                        continue;

                    if (currentItem.Player != player)
                        continue;

                    list.Add(currentItem);
                }
            }

            return list;
        }
    }
}