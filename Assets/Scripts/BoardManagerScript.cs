using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[] {
            blank,               // 0
            verticalLaser,       // 1
            horizontalLaser,     // 2
            oneInOneOut,         // 3
            oneInOneOutOn,       // 4
            oneInTwoOut,         // 5
            oneInTwoOutOnTop,    // 6
            oneInTwoOutOnBottom, // 7
            twoInOneOut,         // 8
            twoInOneOutOnTop,    // 9
            twoInOneOutOnBottom, // 10
            mirrorPositive,      // 11
            mirrorPositiveOn,    // 12
            mirrorNegative,      // 13
            mirrorNegativeOn     // 14
        };

        levels.Add(new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        });
    }
    public void UpdateBoard()
    {
        
    }


    public void StartLevel()
    {
        board = new Board();
        board.Initialize(levels[currentLevel]);
    }

    public void SetTile(int tileIndex, int row, int col)
    {
        tilemap.SetTile(new Vector3Int(row, col, 0), tiles[tileIndex]);
    }

    public void SetBoard()
    {
        for (int i = 0; i < Board.ROWS; i++)
        {
            for (int j = 0; j < Board.COLS; j++)
            {
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
