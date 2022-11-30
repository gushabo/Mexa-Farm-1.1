using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Tree2,
    Tree3,
    Rock,
    Rock2,
    Rock3,
    Bush,
    Bush2,
    Bush3

}

[CreateAssetMenu(menuName = "Data/Tool Action/ Gather Resource Node")]
public class GatherResourceNode : ToolAction
{

    [SerializeField] float SizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;

    public override bool OnApply(Vector2 worldPoint)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, SizeOfInteractableArea);

        foreach (Collider2D item in colliders)
        {
            ToolHit hit = item.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canHitNodesOfType) == true)
                {
                    hit.Golpe();
                    return true;
                }
            }
        }

        return false;
    }

}
