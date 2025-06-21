using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Board
{
    // grid displays the state of each tile on the board
    private int[,] grid;
    // head of the laser
    public Laser[] laserHeads;


    public static readonly int WIDTH = 7;
    public static readonly int LENGTH = 6;

    public Board()
    {
        grid = new int[6, 7];
        laserHeads = new Laser[3];
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

        Board.laserHeads[0] = new Laser();
    }

    public Board()
    {
        grid = new int[6, 7];
        laserHeads = new Laser[3];
    }

    public void Step()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (laserHeads[i] == null)
            {
                continue;
            }

            laserHeads[i].position = laserHeads[i].position + laserHeads[i].direction;
            laserHeads[i].Confine();
        }
    }



}
