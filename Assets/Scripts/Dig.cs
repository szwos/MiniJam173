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

    public float digFuelConsumption = 1f;

    public event DiggingEventHandler Digging;

    //direction:
    //Vector2.right - horizontally
    //Vector2.down - vertically
    //returns Dig destination if can dig, null if cannot
    public (Vector3, IDestroyableBlock)? TryDig(Vector2 direction)
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

        if (block is IDestroyableBlock destroyableBlock)
        {
            //If player doesn't have right drill, return null
            if(destroyableBlock.Hardness > PlayerStats.Instance.DrillHardness)
                return null;
            
            
            DoBlockActions(destroyableBlock);
            Terrain.SetBlock(selector.transform.position, BlockRegistry.Air);

            PlayerStats.Instance.Fuel -= digFuelConsumption;
            
            return (selector.transform.position, destroyableBlock);

        }
        return null;
    }

    private void DoBlockActions(IDestroyableBlock block)
    {


        // if (block.Id.Equals(BlockRegistry.Air))
        // {
        //     //Do nothing, won't happen anyway
        // } else if (block.Id.Equals(BlockRegistry.Copper))
        // {
        //     PlayerStats.Instance.Money += 100;
        // }

        if (block is IGiveMoneyBlock giveMoneyBlock)
        {
            PlayerStats.Instance.Money += giveMoneyBlock.Money;
        }
    }

}
