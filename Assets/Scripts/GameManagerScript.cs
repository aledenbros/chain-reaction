using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject pickHandButtons;

    private Player human;
    private Player computer;
    public bool isHumanTurn { get; private set; }
    public string laserDirection { get; private set; }

    // 0 for taking stage, 1 for attacking stage
    private string turnState;

    // Start is called before the first frame update
    void Start()
    {
        human = new Player();
        computer = new Player();
        StartTurn();
    }

    void OnPickLeft()
    {
        Debug.Log("picked left");
        laserDirection = isHumanTurn ? "down" : "up";
        turnState = "firing";
    }

    void OnPickRight()
    {
        Debug.Log("picked right");
        laserDirection = isHumanTurn ? "down" : "up";
        turnState = "firing";

    }

    void StartSim()
    {

    }

    public void FireLeft()
    {
        if (turnState == "picking")
        {
            
        } 
        else
        {
            
        }
    }

    public void FireRight()
    {
    }

    void StartTurn()
    {
        turnState = "picking";

        pickHandButtons.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHumanTurn)
        {

        }
    }
}
