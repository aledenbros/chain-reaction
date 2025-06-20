using System.Collections.Generic;
using UnityEngine;



public class Board
{
    // grid displays the state of each tile on the board
    private int[,] grid;
    // head of the laser
    public List<Laser> laserHeads { get; }
    public bool isHumanTurn { get; set; }
    public static readonly int COLS = 7;
    public static readonly int ROWS = 6;
    

    public Board()
    {
        grid = new int[6, 7];
        laserHeads = new List<Laser>(35);
    }

    public void Initialize(int[,] obstacles)
    {
        for (int x = 0; x < 6; ++x)
        {
            for (int y = 0; y < 7; ++y)
            {
                grid[x, y] = obstacles[x, y];
            }
        }

        laserHeads[0] = new Laser();
    }


    public void Step()
    {
        Laser[] toStep = new Laser[35];
        laserHeads.CopyTo(toStep);
        foreach (Laser i in toStep)
        {
            StepLaser(i);
        }
    }

    public void StepLaser(Laser laserHead)
    {  
        switch (grid[laserHead.position.x, laserHead.position.y])
        {
            case 0:
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 3:
                laserHead.state = (laserHead.state + 1) % 10;
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 5:
                laserHead.direction = Vector3Int.left;
                Laser right = new Laser();
                right.position = laserHead.position + (Vector3Int.right * 2);
                right.direction = Vector3Int.right;
                right.state = laserHead.state;
                laserHead.state /= 2;
                right.state -= laserHead.state;
                laserHeads.Add(right);
                break;
            case 8:
                Laser second = laserHeads.Find(laser => ((laser.position == 
                laserHead.position && (laser.direction != laserHead.direction))));
                if (second != null)
                {
                    laserHead.state = (laserHead.state + second.state) % 10;
                    laserHeads.RemoveAt(laserHeads.FindIndex(laser => ((laser.position == 
                                        laserHead.position && (laser.direction != laserHead.direction)))));
                    laserHead.direction = Vector3Int.up;
                    laserHead.position = laserHead.position + laserHead.direction;
                }
                break;
            case 11:
                if (laserHead.direction == Vector3Int.up)
                {
                    laserHead.direction = Vector3Int.right;
                } 
                else if (laserHead.direction == Vector3Int.right)
                {
                    laserHead.direction = Vector3Int.up;
                }
                else if (laserHead.direction == Vector3Int.down)
                {
                    laserHead.direction = Vector3Int.left;
                }
                else if (laserHead.direction == Vector3Int.left)
                {
                    laserHead.direction = Vector3Int.down;
                }
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 13:
                if (laserHead.direction == Vector3Int.up)
                {
                    laserHead.direction = Vector3Int.left;
                } 
                else if (laserHead.direction == Vector3Int.left)
                {
                    laserHead.direction = Vector3Int.up;
                }
                else if (laserHead.direction == Vector3Int.down)
                {
                    laserHead.direction = Vector3Int.right;
                }
                else if (laserHead.direction == Vector3Int.right)
                {
                    laserHead.direction = Vector3Int.down;
                }
                laserHead.position = laserHead.position + laserHead.direction;
                break;
        }
    }

    public bool IsFinished()
    {
        if (isHumanTurn)
        {
            for (int i = 0; i < laserHeads.Count; ++i)
            {
                if (laserHeads[i].position.y != ROWS - 1)
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            for (int i = 0; i < laserHeads.Count; ++i)
            {
                if (laserHeads[i].position.y != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public int GetSquareAt(int row, int col)
    {
        return grid[row, col];
    }

    public int GetSquareAt(Vector3Int position)
    {
        return GetSquareAt(position.x, position.y);
    }

    public void SetSquareAt(int row, int col, int square)
    {
        grid[row, col] = square;
    }

}
