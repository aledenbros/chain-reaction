using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int left { get; set; }
    public int right { get; set; }
    public int leftAmmo;
    public int rightAmmo;


    public Player()
    {
        left = 1;
        right = 1;
        leftAmmo = 0;
        rightAmmo = 0;
    }


}
