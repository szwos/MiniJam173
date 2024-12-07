#nullable enable
using DefaultNamespace;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate void DiggingEventHandler(object sender, Vector3 diggingDestination);

public class Dig : MonoBehaviour
{
    public GameObject DrillSelectorHorizontal;
    public GameObject DrillSelectorVertical;
    public TerrainMananger Terrain;
    public TilemapMovement PlayerMovement;

    public event DiggingEventHandler Digging;

    //direction:
    //Vector2.right - horizontally
    //Vector2.down - vertically
    //returns Dig destination if can dig, null if cannot
    public Vector3? TryDig(Vector2 direction)
    {

        GameObject selector;
        if(direction == Vector2.right)
        {
            selector = DrillSelectorHorizontal;
        } else if (direction == Vector2.down)
        {
            selector = DrillSelectorVertical;
        } else
        {
            return null;
        }

        BlockBase? block = Terrain.GetBlock(selector.transform.position);

        if (block == null)
        {
            return null;
        }

        if (block.CanDestroy())
        {
            DoBlockActions(block);
            Terrain.SetBlock(selector.transform.position, BlockRegistry.Air);
            return selector.transform.position;

        }
        return null;
    }

    private void DoBlockActions(BlockBase block)
    {


        if (block.Id.Equals(BlockRegistry.Air))
        {
            //Do nothing, won't happen anyway
        } else if (block.Id.Equals(BlockRegistry.Ore))
        {
            PlayerStats.Instance.Money += 100;
        }

    }

}
