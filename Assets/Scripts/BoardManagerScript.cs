using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManagerScript : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile verticalLaser;
    [SerializeField] private Tile horizontalLaser;

    private Board board;

    public void UpdateBoard()
    {
        
    }

    public void UpdateHead()
    {
        tilemap.SetTile(board.laserHead.position, verticalLaser);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
