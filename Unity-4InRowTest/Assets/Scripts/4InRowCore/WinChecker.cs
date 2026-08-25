using System;

namespace FourInRow
{
    public class WinChecker
    {
        public FourInRowGame Field;
        public (int x, int y)[] WinCells;
        public WinChecker(FourInRowGame fourIn)
        {
            Field = fourIn;
            WinCells = new (int x, int y)[Field.WinCondition];
            Field = fourIn;
        }
        StepResult CellToResult(CellInfo cell)
        {
            if (cell == CellInfo.Red)
            {
                return StepResult.RedWin;
            }
            else if (cell == CellInfo.Yellow)
            {
                return StepResult.YellowWin;
            }
            else
            {
                throw new InvalidCastException("Not red or yellow!");
            }
        }
        public StepResult WinCheck()
        {
            var check = WinCheckHorisontal();
            if (check != CellInfo.Blank)
                return CellToResult(check);
            check = WinCheckVertical();
            if (check != CellInfo.Blank)
                return CellToResult(check);
            check = WinCheckDiagonalLeft();
            if (check != CellInfo.Blank)
                return CellToResult(check);
            check = WinCheckDiagonalRight();
            if (check != CellInfo.Blank)
                return CellToResult(check);
            if (Field.StepCount >= Field.FieldSize.Y * Field.FieldSize.X)
                return StepResult.Draft;
            return StepResult.Nothing;
        }
        CellInfo WinCheckHorisontal()
        {
            int similarInRow;
            CellInfo currentCell;
            for (int y = 0; y < Field.FieldSize.Y; y++)
            {
                currentCell = Field.Field[0, y];
                similarInRow = 0;
                for (int x = 0; x < Field.FieldSize.X; x++)
                {
                    if (Field.Field[x, y] != CellInfo.Blank && currentCell == Field.Field[x, y])
                    {
                        similarInRow++;
                        if (similarInRow == Field.WinCondition)
                        {
                            FormWinCellsHorisontal(x, y);
                            return currentCell;
                        }
                    }
                    else
                    {
                        similarInRow = 1;
                        currentCell = Field.Field[x, y];
                    }
                }
            }
            return CellInfo.Blank;
        }
        private void FormWinCellsHorisontal(int x, int y)
        {
            for (int i = 0; i < Field.WinCondition; i++)
            {
                WinCells[i] = (x: x - i, y);
            }
        }
        private void FormWinCellsVertical(int x, int y)
        {
            for (int i = 0; i < Field.WinCondition; i++)
            {
                WinCells[i] = (x, y - i);
            }
        }
        private void FormWinCellsDiagonalLeft(int x, int y)
        {
            for (int i = 0; i < Field.WinCondition; i++)
            {
                WinCells[i] = (x - i, y + i);
            }
        }
        private void FormWinCellsDiagonalRight(int x, int y)
        {
            for (int i = 0; i < Field.WinCondition; i++)
            {
                WinCells[i] = (x + i, y + i);
            }
        }
        CellInfo WinCheckVertical()
        {
            int similarInRow;
            CellInfo currentCell;
            for (int x = 0; x < Field.FieldSize.X; x++)
            {
                similarInRow = 0;
                currentCell = Field.Field[x, 0];
                for (int y = 0; y < Field.FieldSize.Y; y++)
                {
                    if (Field.Field[x, y] != CellInfo.Blank && currentCell == Field.Field[x, y])
                    {
                        similarInRow++;
                        if (similarInRow == Field.WinCondition)
                        {
                            FormWinCellsVertical(x, y);
                            return currentCell;
                        }
                    }
                    else
                    {
                        similarInRow = 1;
                        currentCell = Field.Field[x, y];
                    }
                }
            }
            return CellInfo.Blank;
        }
        CellInfo WinCheckDiagonalLeft()
        {
            for (int i = 0; i < Field.FieldSize.X; i++)
            {
                for (int j = 0; j < Field.FieldSize.Y; j++)
                {
                    var current = DiagonalLeftCheck(i, j);
                    if (current != CellInfo.Blank)
                    {
                        FormWinCellsDiagonalLeft(i, j);
                        return current;
                    }
                }
            }
            return CellInfo.Blank;
        }
        CellInfo DiagonalLeftCheck(int x, int y)
        {
            if (x - Field.WinCondition + 1 < 0 ||
                y + Field.WinCondition - 1 >= Field.FieldSize.Y)
            {
                return CellInfo.Blank;
            }
            CellInfo rootCelll = Field.Field[x, y];
            if (rootCelll == CellInfo.Blank)
            {
                return rootCelll;
            }
            for (int i = 0; i < Field.WinCondition; i++)
            {
                if (Field.Field[x - i, y + i] != rootCelll)
                {
                    return CellInfo.Blank;
                }
            }
            return rootCelll;
        }
        CellInfo DiagonalRightCheck(int x, int y)
        {
            if (x + Field.WinCondition - 1 >= Field.FieldSize.X || y + Field.WinCondition - 1 >= Field.FieldSize.Y)
            {
                return CellInfo.Blank;
            }
            CellInfo rootCelll = Field.Field[x, y];
            if (rootCelll == CellInfo.Blank)
            {
                return rootCelll;
            }
            for (int i = 0; i < Field.WinCondition; i++)
            {
                if (Field.Field[x + i, y + i] != rootCelll)
                {
                    return CellInfo.Blank;
                }
            }
            return rootCelll;
        }
        CellInfo WinCheckDiagonalRight()
        {
            for (int i = 0; i < Field.FieldSize.X; i++)
            {
                for (int j = 0; j < Field.FieldSize.Y; j++)
                {
                    var current = DiagonalRightCheck(i, j);
                    if (current != CellInfo.Blank)
                    {
                        FormWinCellsDiagonalRight(i, j);
                        return current;
                    }
                }
            }
            return CellInfo.Blank;
        }
    }
}

