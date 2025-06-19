using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int leftPoints { get; set; }
    public int rightPoints { get; set; }
    public bool crossBowLoaded;
    public int ammo;


    public Player()
    {
        leftPoints = 1;
        rightPoints = 1;
        crossBowLoaded = false;
        ammo = 0;
    }


}
