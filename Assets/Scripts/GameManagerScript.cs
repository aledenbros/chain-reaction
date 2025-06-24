using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject pickButtons;
    [SerializeField] private GameObject fireButtons;
    [SerializeField] private GameObject reallocateSubmit;

    [SerializeField] private Sprite one;
    [SerializeField] private Sprite two;
    [SerializeField] private Sprite three;
    [SerializeField] private Sprite four;
    [SerializeField] private Sprite five;
    [SerializeField] private Sprite six;
    [SerializeField] private Sprite seven;
    [SerializeField] private Sprite eight;
    [SerializeField] private Sprite nine;

    private BoardManagerScript boardManagerScript;
    private Player humanPlayer;
    private Player computer;
    public bool isHumanTurn { get; private set; }
    public int laserDirection { get; private set; }

    // 0 for taking stage, 1 for attacking stage
    private string turnState;
    private bool isReallocating;

    private int leftReallocate;
    private int rightReallocate;

    // Start is called before the first frame update
    void Start()
    {
        isReallocating = false; // Change to false
        boardManagerScript = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManagerScript>();
        humanPlayer = new Player();
        computer = new Player();
        StartTurn();
    }

    public void UpdateHands()
    {
        UpdateHand(humanPlayer.Hands.LeftHand, humanPlayer.left);
        UpdateHand(humanPlayer.Hands.RightHand, humanPlayer.right);
        UpdateHand(computer.Hands.LeftHand, computerPlayer.left);
        UpdateHand(computer.Hands.RightHand, computerPlayer.right);
    }

    public void UpdateHand(GameObject Hand, int value)
    {
        switch (value)
        {
            case 1:
                Hand.GetComponent<SpriteRenderer>().sprite = one;
                break;
            case 2:
                Hand.GetComponent<SpriteRenderer>().sprite = two;
                break;
            case 3:
                Hand.GetComponent<SpriteRenderer>().sprite = three;
                break;
            case 4:
                Hand.GetComponent<SpriteRenderer>().sprite = four;
                break;
            case 5:
                Hand.GetComponent<SpriteRenderer>().sprite = five;
                break;
            case 6:
                Hand.GetComponent<SpriteRenderer>().sprite = six;
                break;
            case 7:
                Hand.GetComponent<SpriteRenderer>().sprite = seven;
                break;
            case 8:
                Hand.GetComponent<SpriteRenderer>().sprite = eight;
                break;
            case 9:
                Hand.GetComponent<SpriteRenderer>().sprite = nine;
                break;
            default:
                Hand.GetComponent<SpriteRenderer>().sprite = one;
                break;
        }
    }

    void OnPickLeft()
    {
        Debug.Log("picked left");
        
        int value = isHumanTurn ? computer.left : humanPlayer.left;

        OnPick(value);
    }

    void OnPickRight()
    {
        Debug.Log("picked right");

        int value = isHumanTurn ? computer.right : humanPlayer.right;

        OnPick(value);
    }

    void OnPick(int value)
    {
        SetLaserDirection(isHumanTurn ? -1 : 1);
        boardManagerScript.AddStartingLaser(value);
        boardManagerScript.isSimulating = true;

        turnState = "firing";
        pickButtons.SetActive(false);
        fireButtons.SetActive(true);
        UpdateHands();
    }

    public void OnStartReallocate()
    {
        leftReallocate = humanPlayer.left;
        rightReallocate = humanPlayer.right;
        isReallocating = true;
        reallocateSubmit.SetActive(true);
        pickButtons.SetActive(false);
    }

    public void OnSubmitReallocate()
    {
        humanPlayer.left = leftReallocate;
        humanPlayer.right = rightReallocate;
        isReallocating = false;
        reallocateSubmit.SetActive(false);
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
        if (value <= 5)
        {
            return;
        }
        if (value == 6)
        {
            if (isHumanTurn && hand == 0 && humanPlayer.right > 0)
            {
                --humanPlayer.right;
            }
            else if (isHumanTurn && hand == 1 && humanPlayer.left > 0)
            {
                --humanPlayer.left;
            }
            else if (!isHumanTurn && hand == 0 && computer.right > 0)
            {
                --computer.right;
            }
            else if (!isHumanTurn && hand == 1 && computer.left > 0)
            {
                --computer.left;
            }
            else
            {
                return;
            }
        }
        else if (value == 7 || value == 8)
        {
            if (isHumanTurn && hand == 0 && humanPlayer.rightAmmo > 0)
            {
                --humanPlayer.rightAmmo;
            }
            else if (isHumanTurn && hand == 1 && humanPlayer.leftAmmo > 0)
            {
                --humanPlayer.leftAmmo;
            }
            else if (!isHumanTurn && hand == 0 && computer.rightAmmo > 0)
            {
                --computer.rightAmmo;
            }
            else if (!isHumanTurn && hand == 1 && computer.leftAmmo > 0)
            {
                --computer.leftAmmo;
            }
            else
            {
                return;
            }
        }

        boardManagerScript.AddStartingLaser(value);
        boardManagerScript.isSimulating = true;
        fireButtons.SetActive(false);
        isHumanTurn = !isHumanTurn;
        UpdateHands();
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

        if (isReallocating)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                int totalValue = humanPlayer.right + humanPlayer.left;
                leftReallocate = (int)(Input.mousePosition.x / Screen.width * (totalValue + 1));
                rightReallocate = totalValue - leftReallocate;

                Debug.Log(leftReallocate + " " + rightReallocate);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnSubmitReallocate();
                Debug.Log("Submitted: " + humanPlayer.left + " " + humanPlayer.right);
            }

            UpdateHands();
        }
    }

    private void SetLaserDirection(int direction)
    {
        laserDirection = direction;
        boardManagerScript.SetLaserDirection(direction);
    }
}
