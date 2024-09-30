using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Resource : IEqualityComparer<Resource>
{
    private string Id;
    public ResourceData Data 
    {
        get
        {
            if (ResourceManager.Instance.ResourceData.TryGetValue(Id, out var d))
            {
                return d;
            }
            throw new Exception($"Error: Invalid resource subtype ID: {Id}");
        }
    }
    public int Amount;

    public Resource(string subtypeID, int amount)
    {
        Id = subtypeID;
        Amount = amount;
    }
    private Resource(ResourceData data, int amount)
    {
        Id = data.Id;
        Amount = amount;
    }

    /// <summary>
    /// Checks if the resource id and amount are the same.
    /// </summary>
    /// <param name="other"></param>
    /// <returns>If a's id equals b's and if a's amount equals b's</returns>
    public bool StrictEquals(Resource other) => Data.Equals(other.Data) && other.Amount == Amount;
    /// <summary>
    /// Checks if the resource id is the same.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>If a's id equals b's id</returns>
    bool IEqualityComparer<Resource>.Equals(Resource a, Resource b) => a.Data.Equals(b.Data);

    int IEqualityComparer<Resource>.GetHashCode(Resource obj) => obj.GetHashCode();

    /// <summary>
    /// Checks if the resource id is the same.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>If a's id equals b's id</returns>
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
    public static Resource operator -(Resource a, Resource b)
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
