using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI.Table;

public class BoardManagerScript : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile blank;
    [SerializeField] private Tile verticalLaser;
    [SerializeField] private Tile horizontalLaser;
    [SerializeField] private Tile oneInOneOut;
    [SerializeField] private Tile oneInTwoOut;
    [SerializeField] private Tile twoInOneOut;
    [SerializeField] private Tile mirrorPositive;
    [SerializeField] private Tile mirrorNegative;
    [SerializeField] private Tile oneInOneOutOn;
    [SerializeField] private Tile oneInTwoOutOnTop;
    [SerializeField] private Tile oneInTwoOutOnBottom;
    [SerializeField] private Tile twoInOneOutOnTop;
    [SerializeField] private Tile twoInOneOutOnBottom;
    [SerializeField] private Tile mirrorPositiveOn;
    [SerializeField] private Tile mirrorNegativeOn;

    private Tile[] tiles;
    private Board board;
    private List<int[,]> levels = new List<int[,]>();
    private int currentLevel = 0;
    private GameManagerScript gameManagerScript;
    private bool isSimulating = true;
    private float simulationClock;
    private float testClock;
    private int count = 1;


    private static readonly float SIM_RATE = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        tiles = new Tile[] {
            blank,               // 0
            verticalLaser,       // 1
            horizontalLaser,     // 2
            oneInOneOut,         // 3
            oneInOneOutOn,       // 4
            oneInTwoOut,         // 5
            oneInTwoOutOnBottom, // 6
            oneInTwoOutOnTop,    // 7
            twoInOneOut,         // 8
            twoInOneOutOnTop,    // 9
            twoInOneOutOnBottom, // 10
            mirrorPositive,      // 11
            mirrorPositiveOn,    // 12
            mirrorNegative,      // 13
            mirrorNegativeOn     // 14
        };

        levels.Add(new int[,] {
            { 3, 0, 0, 0, 0, 0, 0 },
            { 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 3 },
        });

        currentLevel = 0;
        isSimulating = true;
        simulationClock = SIM_RATE;


        StartLevel();
    }

    public void DrawLaserHeads()
    {
        Debug.Log("Drawing laser heads");
        for (int i = 0; i < board.laserHeads.Count; ++i)
        {
            if (board.GetSquareAt(board.laserHeads[i].position) == 0 && board.laserHeads[i].direction.x == 0)
            {
                SetTile(1, board.laserHeads[i].position);
            }
            else if (board.GetSquareAt(board.laserHeads[i].position) == 0 && board.laserHeads[i].direction.y == 0)
            {
                SetTile(2, board.laserHeads[i].position);
            }
            else if (board.GetSquareAt(board.laserHeads[i].position) == 3 || board.GetSquareAt(board.laserHeads[i].position) == 11 || board.GetSquareAt(board.laserHeads[i].position) == 13)
            {
                SetTile(board.GetSquareAt(board.laserHeads[i].position) + 1, board.laserHeads[i].position);
            }
            else if (board.GetSquareAt(board.laserHeads[i].position) == 5 || board.GetSquareAt(board.laserHeads[i].position) == 8)
            {
                if (gameManagerScript.laserDirection == 1)
                {
                    SetTile(board.GetSquareAt(board.laserHeads[i].position) + 1, board.laserHeads[i].position);
                }
                else
                {
                    SetTile(board.GetSquareAt(board.laserHeads[i].position) + 2, board.laserHeads[i].position);
                }
            }
        }
    }

    public void StartLevel()
    {
        board = new Board();
        Debug.Log("initializing");
        board.Initialize(levels[currentLevel]);

        Debug.Log("drawing board 1");
        DrawBoard();
    }

    public void SetTile(int tileIndex, int row, int col)
    {
        tilemap.SetTile(new Vector3Int(row, col, 0), tiles[tileIndex]);
    }

    public void SetTile(int tileIndex, Vector3Int position)
    {
        tilemap.SetTile(new Vector3Int(position.x, position.y, 0), tiles[tileIndex]);
    }

    public void DrawBoard()
    {
        Debug.Log("drawing board...");
        for (int i = 0; i < Board.ROWS; ++i)
        {
            for (int j = 0; j < Board.COLS; ++j)
            {
                Debug.Log("Set " + board.GetSquareAt(i, j) + " at " + i + ", "+ j);
                SetTile(board.GetSquareAt(i, j), i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSimulating)
        {
            simulationClock -= Time.deltaTime;

            if (simulationClock < 0)
            {
                Debug.Log("Board update " + count++);
                if (board.IsFinished())
                {
                    Debug.Log("done " + board.laserHeads[0].position);
                    isSimulating = false;
                } 
                else
                {
                    Debug.Log("Stepping");
                    board.Step();
                    DrawLaserHeads();
                }

                simulationClock = SIM_RATE;
            }
        }

    }


    public void SetLaserDirection(int direction)
    {
        board.laserDirection = direction;
    }
}
