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
        for (int i = 0; i < 3; ++i)
        {
            Steplaser(laser);
        }
    }

    public void StepLaser(Laser laserHead)
    {  
        switch (grid[laserHead.position.x, laserHead.position.y])
        {
            case 0:
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 1:
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 2:
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 3:
                laserHead.state = (laserHead.state + 1) % 10;
                laserHead.position = laserHead.position + laserHead.direction;
                break;
            case 4:
                laserHead.direction = Vector3Int.left;
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
