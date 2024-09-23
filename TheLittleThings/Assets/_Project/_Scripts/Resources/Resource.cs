using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Resource : IEqualityComparer<Resource>
{
    public ResourceData Data;
    public int Amount;

    public Resource(string subtypeID, int amount)
    {
        if (!ResourceManager.instance.ResourceData.TryGetValue(subtypeID, out Data))
        {
            throw new Exception($"Error: Invalid resource subtype ID: {subtypeID}");
        }

        Amount = amount;
    }
    private Resource(ResourceData data, int amount)
    {
        Data = data;
        Amount = amount;
    }

    public Resource(SerializableResource identifier) : this(identifier.Id, identifier.Amount) { }
    public SerializableResource GetIdentifier()
    {
        return new SerializableResource(Data.Id, Amount);
    }

    public bool IsTheSameResource(string id)
    {
        return Data.Id == id;
    }

    bool IEqualityComparer<Resource>.Equals(Resource a, Resource b)
    {
        return a.Equals(b);
    }

    int IEqualityComparer<Resource>.GetHashCode(Resource obj)
    {
        return obj.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is not Resource)
        {
            return false;
        }
        return ((Resource)obj).Data.Equals(Data);
    }

    public override int GetHashCode()
    {
        return Data.GetHashCode();
    }

    public override string ToString()
    {
        return $"Resource {{id: {Data.Id} amount: {Amount}}}";
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

    public static Resource operator +(Resource a, int b)
    {
        return new Resource(a.Data, a.Amount + b);
    }

    public static Resource operator +(Resource a, SerializableResource b)
    {
        if (a.Data.Id == b.Id)
        {
            return new Resource(a.Data, a.Amount + b.Amount);
        }
        throw new InvalidResourceOperation();
    }

    public static Resource operator +(SerializableResource a, Resource b)
    {
        if (a.Id == b.Data.Id)
        {
            return new Resource(b.Data, a.Amount + b.Amount);
        }
        throw new InvalidResourceOperation();
    }
    public static Resource operator -(Resource a, Resource b)
    {
        return a + (-b);
    }

    public static Resource operator -(Resource a, SerializableResource b)
    {
        return a + (-b);
    }

    public static Resource operator -(Resource a, int b)
    {
        return a + (-b);
    }
}


public class InvalidResourceOperation : Exception
{
}
