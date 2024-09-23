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

    private Resource(ResourceData data, int amount)
    {
        Data = data;
        Amount = amount;
    }
    public static Resource operator +(Resource a)
    {
        return a;
    }
    public static Resource operator -(Resource a)
    {
        return new Resource(a.Data, -a.Amount);
    }
    public static Resource operator +(Resource a, Resource b)
    {
        if (a.Data == b.Data)
        {
            return new Resource(a.Data, a.Amount + b.Amount);
        }
        throw new InvalidResourceOperation();
    }
    public static Resource operator -(Resource a, Resource b)
    {
        if (a.Data == b.Data)
        {
            return new Resource(a.Data, a.Amount - b.Amount);
        }
        throw new InvalidResourceOperation();
    }

    public class InvalidResourceOperation : System.Exception { }
}
