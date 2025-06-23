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
        Debug.Log("picked left");

    }

    public void OnPickRight()
    {
        Debug.Log("picked right");
        
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
        int value = isHumanTurn ? humanPlayer.left : computer.left;
        
        OnFire(0, value);
    }

    public void OnFireRight()
    {
        int value = isHumanTurn ? humanPlayer.right : computer.right;
        
        OnFire(1, value);
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
