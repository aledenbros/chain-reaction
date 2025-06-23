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
    private Player human;
    private Player computer;
    public bool isHumanTurn { get; private set; }
    public int laserDirection { get; private set; }

    // 0 for taking stage, 1 for attacking stage
    private string turnState;

    // Start is called before the first frame update
    void Start()
    {
        boardManagerScript = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManagerScript>();
        human = new Player();
        computer = new Player();
        StartTurn();
    }

    void OnPickLeft()
    {
        Debug.Log("picked left");
        SetLaserDirection(isHumanTurn ? -1 : 1);

        turnState = "firing";
        pickButtons.SetActive(false);
        fireButtons.SetActive(true);
    }

    void OnPickRight()
    {
        Debug.Log("picked right");
        SetLaserDirection(isHumanTurn ? -1 : 1);

        turnState = "firing";
        pickButtons.SetActive(false);
        fireButtons.SetActive(true);

    }

    void StartSim()
    {

    }

    public void OnFireLeft()
    {
        
    }

    public void OnFireRight()
    {

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
