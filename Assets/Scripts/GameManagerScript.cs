using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject pickButtons;
    [SerializeField] private GameObject fireButtons;

    private BoardManagerScript boardManagerScript;
    private Player humanPlayer;
    private Player computer;
    public bool isHumanTurn { get; private set; }
    public int laserDirection { get; private set; }

    // 0 for taking stage, 1 for attacking stage
    private string turnState;

    // Start is called before the first frame update
    void Start()
    {
        boardManagerScript = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManagerScript>();
        humanPlayer = new Player();
        computer = new Player();
        StartTurn();


    }

    public void OnPickLeft()
    {
        if (!boardManagerScript.isSimulating)
        {
        Debug.Log("picked left");
        
        int value = isHumanTurn ? computer.left : humanPlayer.left;

        OnPick(value);
        }
    }

    public void OnPickRight()
    {
        if (!boardManagerScript.isSimulating)
        {
        Debug.Log("picked right");

        int value = isHumanTurn ? computer.right : humanPlayer.right;

        OnPick(value);
        }
        
    }

    void OnPick(int value)
    {
        SetLaserDirection(isHumanTurn ? -1 : 1);
        boardManagerScript.AddStartingLaser(value);
        boardManagerScript.isSimulating = true;

        turnState = "firing";
        pickButtons.SetActive(false);
        fireButtons.SetActive(true);
    }



    void StartSim()
    {

    }

    public void OnFireLeft()
    {
        if (!boardManagerScript.isSimulating)
        {
        int value = isHumanTurn ? humanPlayer.left : computer.left;
        
        OnFire(0, value);
        }
    }

    public void OnFireRight()
    {
        if (!boardManagerScript.isSimulating)
        {
        int value = isHumanTurn ? humanPlayer.right : computer.right;
        
        OnFire(1, value);
        }
    }

    // 0 is left, 1 is right
    private void OnFire(int hand, int value)
    {
        SetLaserDirection(isHumanTurn ? 1 : -1);
        
        if (value == 6)
        {
            if (isHumanTurn && hand == 0)
            {
                --humanPlayer.right;
            }
            else if (isHumanTurn && hand == 1)
            {
                --humanPlayer.left;
            }
        }
        if (value == 7)
        {

        }

        boardManagerScript.AddStartingLaser(value);
        boardManagerScript.isSimulating = true;
        fireButtons.SetActive(false);
        isHumanTurn = !isHumanTurn;
    }

    void StartTurn()
    {
        turnState = "picking";

        pickButtons.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHumanTurn)
        {

        }
    }

    private void SetLaserDirection(int direction)
    {
        laserDirection = direction;
        boardManagerScript.SetLaserDirection(direction);
    }
}
