using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceManager
{
    public override bool Move(int c, int r)
    {
        if (GetMovable(c, r))
        {
            SetPosition(c, r);
            MoveRule();
            return true;
        }
        return false;
    }

    public override bool GetMovable(int c, int r)
    {
        MoveRule();
        return zone[c, r];
    }

    public override void Initialize()
    {
        col = 3;
        row = 0;
        base.Initialize();
    }

    public override void MoveRule()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                zone[i, j] = false;
            }
        }

        // �������� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col + i, row) == 0)
            {
                zone[col + i, row] = true;
            }
            else if (board.GetBoard(col + i, row) == isPlayer)
            {
                zone[col + i, row] = false;
                break;
            }
            else if (board.GetBoard(col + i, row) == -isPlayer)
            {
                zone[col + i, row] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // �Ʒ������� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col - i, row) == 0)
            {
                zone[col - i, row] = true;
            }
            else if (board.GetBoard(col - i, row) == isPlayer)
            {
                zone[col - i, row] = false;
                break;
            }
            else if (board.GetBoard(col - i, row) == -isPlayer)
            {
                zone[col - i, row] = true;
                break;
            }
        }
        // ���������� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col, row + i) == 0)
            {
                zone[col, row + i] = true;
            }
            else if (board.GetBoard(col, row + i) == isPlayer)
            {
                zone[col, row + i] = false;
                break;
            }
            else if (board.GetBoard(col, row + i) == -isPlayer)
            {
                zone[col, row + i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // �������� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col, row - i) == 0)
            {
                zone[col, row - i] = true;
            }
            else if (board.GetBoard(col, row - i) == isPlayer)
            {
                zone[col, row - i] = false;
                break;
            }
            else if (board.GetBoard(col, row - i) == -isPlayer)
            {
                zone[col, row - i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // ������� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col + i, row + i) == 0)
            {
                zone[col + i, row + i] = true;
            }
            else if (board.GetBoard(col + i, row + i) == isPlayer)
            {
                zone[col + i, row + i] = false;
                break;
            }
            else if (board.GetBoard(col + i, row + i) == -isPlayer)
            {
                zone[col + i, row + i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // �»����� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col + i, row - i) == 0)
            {
                zone[col + i, row - i] = true;
            }
            else if (board.GetBoard(col + i, row - i) == isPlayer)
            {
                zone[col + i, row - i] = false;
                break;
            }
            else if (board.GetBoard(col + i, row - i) == -isPlayer)
            {
                zone[col + i, row - i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // ���Ϸ� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col - i, row + i) == 0)
            {
                zone[col - i, row + i] = true;
            }
            else if (board.GetBoard(col - i, row + i) == isPlayer)
            {
                zone[col - i, row + i] = false;
                break;
            }
            else if (board.GetBoard(col - i, row + i) == -isPlayer)
            {
                zone[col - i, row + i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // ���Ϸ� �̵�
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col - i, row - i) == 0)
            {
                zone[col - i, row - i] = true;
            }
            else if (board.GetBoard(col - i, row - i) == isPlayer)
            {
                zone[col - i, row - i] = false;
                break;
            }
            else if (board.GetBoard(col - i, row - i) == -isPlayer)
            {
                zone[col - i, row - i] = true;
                break;
            }
            else
            {
                break;
            }
        }
    }
}
