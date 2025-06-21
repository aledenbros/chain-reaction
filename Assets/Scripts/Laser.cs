using UnityEngine;

public class Laser
{
    public Vector3Int position;
    public int state;
    public Vector3Int direction;

    const int width = 7;
    const int length = 6;

    public Laser()
    {
        position = new Vector3Int(3, 0, 0);
        state = 1;
        direction = Vector3Int.up;
    }

    public bool Confine()
    {
        if (position.y > length - 1)
        {
            position.y = length - 1;
            return true;
        }
        else if (position.y < 0)
        {
            position.y = 0;
            return true;
        }

        if (position.x > width - 1)
        {
            position.x = width - 1;
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
