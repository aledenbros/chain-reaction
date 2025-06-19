using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Board
{
    // grid displays the state of each tile on the board
    private int[] grid;
    // don't know what this is
    private int[] nextGrid;
    // will become true if the laser head hits the edge of the board
    private bool finished;
    // head of the laser
    public Laser laserHead;


    const int width = 7;
    const int length = 6;
    const int NORTH = 1;
    const int EAST = 2;
    const int SOUTH = 3;
    const int WEST = 4;

    public void Initialize()
    {
        grid = new int[35];
        finished = false;
        laserHead = new Laser();
    }

    public void Step()
    {
        laserHead.position = laserHead.position + laserHead.direction;

        bool finished = laserHead.Confine();
    }

    private int Final()
    {
        if (finished == false) 
        {
            Debug.Log("Not Finished");
            return -1;
        }

        return laserHead.state;
    }

}
