using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Resource
{
    public ResourceData Data;
    public int Amount;

    public Resource(string subtypeID, int amount)
    {
        if (!Resources.instance.ResourceData.TryGetValue(subtypeID, out Data))
        {
            throw new System.Exception($"Error: Invalid resource subtype ID: {subtypeID}");
        }

        Amount = amount;
    }
}
