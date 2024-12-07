#nullable enable
using DefaultNamespace;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate void DiggingEventHandler(object sender, Vector3 diggingDestination);

public class Dig : MonoBehaviour
{
    public Collider2D DrillColliderHorizontal;
    public Collider2D DrillColliderVertical;
    public GameObject DrillSelectorHorizontal; //has to be before DrillCollider, so that it is actually inside colliding block, not a pixel next to
    public GameObject DrillSelectorVertical;
    public TerrainMananger Terrain;
    public TilemapMovement PlayerMovement;

    public event DiggingEventHandler Digging;

    private void Start()
    {
        DrillColliderHorizontal.enabled = false;
        DrillColliderVertical.enabled = false;    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            DrillColliderHorizontal.enabled = true;
            TryDig();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            DrillColliderHorizontal.enabled = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            TryDig();
            DrillColliderVertical.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DrillColliderVertical.enabled = false;
        }


    }

    private void TryDig()
    {
        Debug.Log($"IsGrounded: {PlayerMovement.IsGrounded}\n IsDigging: {PlayerMovement.IsDigging}");
        if (!PlayerMovement.IsGrounded || PlayerMovement.IsDigging == 0)
        {
            return;
        }

        GameObject selector = PlayerMovement.IsDigging switch
        {
            1 => DrillSelectorHorizontal,
            2 => DrillSelectorVertical
        };

        BlockBase? block = Terrain.GetBlock(selector.transform.position);

        if (block == null)
        {
            return;
        }

        if (block.CanDestroy())
        {
            Terrain.SetBlock(selector.transform.position, BlockRegistry.Air);
            //Digging.Invoke(collision.gameObject, selector.transform.position);
            PlayerMovement.DigToPosition(selector.transform.position);

        }
    }


    // Update is called once per frame
    private void OnTrigger2D(Collider2D collision)
    {
        Debug.Log($"IsGrounded: {PlayerMovement.IsGrounded}\n IsDigging: {PlayerMovement.IsDigging}");
        if (!PlayerMovement.IsGrounded || PlayerMovement.IsDigging == 0) 
        {
            return;
        }

        GameObject selector = PlayerMovement.IsDigging switch
        {
            1 => DrillSelectorHorizontal,
            2 => DrillSelectorVertical
        };

        BlockBase? block = Terrain.GetBlock(selector.transform.position);

        if(block == null)
        {
            return;
        }

        if(block.CanDestroy())
        {
            Terrain.SetBlock(selector.transform.position, BlockRegistry.Air);
            Digging.Invoke(collision.gameObject, selector.transform.position);
        }

    }
}
