using UnityEngine;

public class Laser
{
    public Vector3Int position;
    public int state;
    public Vector3Int direction;


    public Laser()
    {
        position = new Vector3Int(3, 0, 0);
        state = 1;
        direction = Vector3Int.up;
    }

    public bool Confine()
    {
        if (position.y > Board.ROWS - 1)
        {
            position.y = Board.ROWS - 1;
            return true;
        }
        else if (position.y < 0)
        {
            position.y = 0;
            return true;
        }

        if (position.x > Board.COLS - 1)
        {
            position.x = Board.COLS - 1;
            return true;
        }
        else if (position.x < 0)
        {
            position.x = 0;
            return true;
        }
        return false;
    }
}
