using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



public class Board
{
    // grid displays the state of each tile on the board
    private int[,] grid;
    // head of the laser
    private List<Laser> laserHeads;



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

        laserHeads.Item[0] = new Laser();
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

    public int GetSquareAt(int row, int col)
    {
        return grid[row, col];
    }

    public void SetSquareAt(int row, int col, int square)
    {
        grid[row, col] = square;
    }

}
