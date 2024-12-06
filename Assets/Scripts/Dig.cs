using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate void DiggingEventHandler(object sender, Vector3Int diggingDestination);

public class Dig : MonoBehaviour
{
    public Collider2D DrillCollider;
    public Tilemap Tilemap;

    public event DiggingEventHandler Digging;

    private void Start()
    {
        DrillCollider.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            DrillCollider.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DrillCollider.enabled = false;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Input.GetKey(KeyCode.LeftControl))
        {
            return;
        }

        Debug.Log(collision.gameObject);
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "diggable")
        {
            Vector3Int targetCellCoords = Tilemap.WorldToCell(transform.position);
            Digging.Invoke(collision.gameObject, targetCellCoords);
            Tilemap.SetTile(targetCellCoords, null);
            //Tilemap.DeleteCells(targetCellCoords, new Vector3Int(1, 1, 0));
        }
    }
}
