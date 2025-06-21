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
    private bool isSimulating;
    private float simulationClock;

    // Start is called before the first frame update
    void Start()
    {
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
        isSimulating = false;
        simulationClock = 0.1f;


        StartLevel();
    }

    public void DrawLaserHeads()
    {
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
                if (gameManagerScript.isHumanTurn)
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
        board.Initialize(levels[currentLevel]);
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
                if (board.IsFinished())
                {
                    isSimulating = false;
                    simulationClock = 0.1f;
                } 
                else
                {
                    board.Step();
                    DrawLaserHeads();

                    simulationClock = 0.1f;
                }

            }
        }
    }
}
