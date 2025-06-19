using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private Player human;
    private Player computer;
    private bool isHumanTurn;

    // 0 for taking stage, 1 for attacking stage
    private int turnState;

    // Start is called before the first frame update
    void Start()
    {
        human = new Player();
        computer = new Player();
        StartTurn();
    }

    public void FireLeft()
    {
        if (turnState == 0)
        {
            Debug.Log("Fired left! state 0");
            ++turnState;
        } 
        else
        {
            Debug.Log("Fired left! state 1");
            turnState = 0;
        }
    }

    public void FireRight()
    {
        if (turnState == 0)
        {
            Debug.Log("Fired right! state 0");
            ++turnState;
        }
        else
        {
            Debug.Log("Fired right! state 1");
            turnState = 0;
        }
    }

    void StartTurn()
    {
        turnState = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
