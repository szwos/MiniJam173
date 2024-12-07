#nullable enable
using DefaultNamespace;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate void DiggingEventHandler(object sender, Vector3 diggingDestination);

public class Dig : MonoBehaviour
{
    public Collider2D DrillCollider;
    public GameObject DrillSelector; //has to be before DrillCollider, so that it is actually inside colliding block, not a pixel next to
    public TerrainMananger Terrain;

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

        BlockBase? block = Terrain.GetBlock(DrillSelector.transform.position);

        if(block == null)
        {
            return;
        }

        if(block.CanDestroy())
        {
            Terrain.SetBlock(DrillSelector.transform.position, BlockRegistry.Air);
            Digging.Invoke(collision.gameObject, DrillSelector.transform.position);
        }

    }
}
