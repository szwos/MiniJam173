using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMovement : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;
    public Rigidbody2D SelfRb;
    public Dig DigBehaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DigBehaviour.Digging += OnDigging;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //TODO: use fuel
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SelfRb.AddForce(new Vector2(250f, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SelfRb.AddForce(new Vector2(-250f, 0));
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            SelfRb.AddForce(new Vector2(0, 500f));
        }
    }

    private void OnDigging(object sender, Vector3Int diggingDestination)
    {
        transform.position = Tilemap.CellToWorld(diggingDestination);
    }

    /*void Move(Vector2Int direction)
    {
        var cellPos = tilemap.WorldToCell(transform.position);

        transform.position = tilemap.CellToWorld(cellPos + (Vector3Int)direction);
    }*/
}
