using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int left { get; set; }
    public int right { get; set; }
    public bool crossBowLoaded;
    public int ammo;


    public Player()
    {
        left = 1;
        right = 1;
        crossBowLoaded = false;
        ammo = 0;
    }


}
