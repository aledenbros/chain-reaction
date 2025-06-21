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
    [SerializeField] private Tile oneInTwoOutOn;
    [SerializeField] private Tile twoInOneOutOn;
    [SerializeField] private Tile mirrorPositiveOn;
    [SerializeField] private Tile mirrorNegativeOn;

    private Tile[] tiles;

    private Board board;
    private List<int[,]> levels = new List<int[,]>();


    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[] {
            blank,            // 0
            verticalLaser,    // 1
            horizontalLaser,  // 2
            oneInOneOut,      // 3
            oneInOneOutOn,    // 4
            twoInOneOut,      // 5
            twoInOneOutOn,    // 6
            mirrorPositive,   // 7
            mirrorPositiveOn, // 8
            mirrorNegative,   // 9
            mirrorNegativeOn  // 10
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

    public void UpdateLaser()
    {
        // tilemap.SetTile(board.laserHead.position, verticalLaser);
    }

    public void StartLevel(int levelIndex)
    {
        board = new Board();
        board.Initialize(levels[levelIndex]);
    }

    public void SetTile(int tileIndex, Vector3Int position)
    {
        tilemap.SetTile(position, tiles[tileIndex]);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
