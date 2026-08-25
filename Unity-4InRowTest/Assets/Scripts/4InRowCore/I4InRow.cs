using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInRow
{
    public interface I4InRow
    {
        void ShowWinMessage(CellInfo winner, (int x, int y)[] winCells);
        void ShowFinalWinMessage(CellInfo winner);
        void ShowMove(int x, int y, CellInfo cell);
        void ShowIncorrectIndexError(int x);
        void ClearField();
    }
}
