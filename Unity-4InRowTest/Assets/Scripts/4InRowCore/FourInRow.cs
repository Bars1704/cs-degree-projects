using System;

namespace FourInRow
{
    public enum CellInfo : byte
    {
        Blank, Yellow, Red
    }
    public enum StepResult : byte
    {
        Nothing, YellowWin, RedWin, Draft, NotCorrectIndexError, ColumnIsBisyError
    }
    public class FourInRowGame
    {
        readonly WinChecker Checker;
        public CellInfo[,] Field;
        public (int X, int Y) FieldSize;
        public (int YellowScore, int RedScore) Score;
        public int StepCount;
        public int PlaysCount;
        public int MaxPlays;
        public int WinCondition;
        public (int x, int y)[] WinCoords;
        public CellInfo FirstStepPlayer;
        public I4InRow View;
        public CellInfo CurrentPlayer
        {
            get
            {
                bool isFirst = StepCount % 2 == 0;
                CellInfo notFirst = FirstStepPlayer == CellInfo.Yellow ?
                    CellInfo.Red : CellInfo.Yellow;
                return isFirst ? FirstStepPlayer : notFirst;
            }
        }
        public CellInfo TotalWinner
        {
            get
            {
                if (Score.RedScore == Score.YellowScore)
                {
                    return CellInfo.Blank;
                }
                else if (Score.RedScore > Score.YellowScore)
                {
                    return CellInfo.Red;
                }
                else return CellInfo.Yellow;
            }
        }
        public string GetScore()
        {
            string result = Score.YellowScore.ToString();
            result += ':';
            result += Score.RedScore.ToString();
            return result;
        }
        public FourInRowGame(int x, int y, int maxPlays, int winConditions, I4InRow view)
        {
            PlaysCount = 0;
            MaxPlays = maxPlays;
            WinCondition = winConditions;
            FirstStepPlayer = CellInfo.Yellow;
            FieldSize = (x, y);
            View = view;
            Checker = new WinChecker(this);
            MakeDefaultField();
        }
        void MakeDefaultField()
        {
            Field = new CellInfo[FieldSize.X, FieldSize.Y];
            StepCount = 0;
        }
        public void MakeOneStep(int xCoord)
        {
            var stepResult = TryMakeStep(xCoord);
            switch (stepResult)
            {
                case StepResult.ColumnIsBisyError:
                    View.ShowIncorrectIndexError(xCoord);
                    break;
                case StepResult.NotCorrectIndexError:
                    break;
                case StepResult.Nothing:
                    break;
                case StepResult.RedWin:
                    Score.RedScore++;
                    FinishMatch(CellInfo.Red);
                    return;
                case StepResult.YellowWin:
                    Score.YellowScore++;
                    FinishMatch(CellInfo.Yellow);
                    return;
                case StepResult.Draft:
                    FinishMatch(CellInfo.Blank);
                    return;
            }
        }
        StepResult TryMakeStep(int xCoord)
        {
            int Yindex = -1;
            if (xCoord >= FieldSize.X || xCoord < 0)
            {
                return StepResult.NotCorrectIndexError;
            }
            if (Field[xCoord, FieldSize.Y - 1] != CellInfo.Blank)
            {
                return StepResult.ColumnIsBisyError;
            }
            for (int i = FieldSize.Y - 1; i >= 0; i--)
            {
                if (i == 0 || Field[xCoord, i - 1] != CellInfo.Blank)
                {
                    Yindex = i;
                    Field[xCoord, i] = CurrentPlayer;
                    break;
                }
            }
            StepCount++;
            View.ShowMove(xCoord, Yindex, CurrentPlayer);
            return Checker.WinCheck();
        }
        void FinishMatch(CellInfo gameResult)
        {
            PlaysCount++;
            FirstStepPlayer = FirstStepPlayer == CellInfo.Red ?
                CellInfo.Yellow : CellInfo.Red;
            MakeDefaultField();

            View.ShowWinMessage(gameResult, Checker.WinCells);

            if (PlaysCount == MaxPlays)
                View.ShowFinalWinMessage(TotalWinner);
        }
    }
}
