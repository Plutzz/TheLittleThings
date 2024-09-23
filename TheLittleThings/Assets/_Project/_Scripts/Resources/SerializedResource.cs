using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Serializable version of resource. Used in unity editor.
/// </summary>
[Serializable]
public struct SerializedResource : IEqualityComparer<SerializedResource>
{
    public string Id;
    public int Amount;

    public SerializedResource(string id, int amount)
    {
        Id = id;

        Amount = amount;
    }

    public bool IsTheSameResource(string id)
    {
        return Id == id;
    }
    /// <summary>
    /// Checks if the resource id and amount are the same.
    /// </summary>
    /// <param name="other"></param>
    /// <returns>If a's id equals b's and if a's amount equals b's</returns>
    public bool StrictEquality(SerializedResource other) => other.Id == Id && other.Amount == Amount;
    /// <summary>
    /// Checks if the resource id is the same.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>If a's id equals b's id</returns>
    bool IEqualityComparer<SerializedResource>.Equals(SerializedResource a, SerializedResource b) => a.Equals(b);

    int IEqualityComparer<SerializedResource>.GetHashCode(SerializedResource obj) => obj.GetHashCode();

    /// <summary>
    /// Checks if the resource id is the same.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>If a's id equals b's id</returns>
    public override bool Equals(object obj)
    {
        if (obj is not SerializedResource)
        {
            return false;
        }
        return ((SerializedResource)obj).Id.Equals(Id);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"Serialized Resource {{id: {Id} amount: {Amount}}}";

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The non serialized version of the resource.</returns>
    public Resource GetResource()
    {
        return new Resource(this);
    }

    public static SerializedResource operator +(SerializedResource a)
    {
        return a;
    }
    public static SerializedResource operator -(SerializedResource a)
    {
        return new SerializedResource(a.Id, -a.Amount);
    }

    public static SerializedResource operator +(SerializedResource a, SerializedResource b)
    {
        if (a.Id == b.Id)
        {
            return new SerializedResource(a.Id, a.Amount + b.Amount);
        }
        throw new InvalidResourceOperation();
    }

    public static SerializedResource operator +(SerializedResource a, int b)
    {
        return new SerializedResource(a.Id, a.Amount + b);
    }

    public static SerializedResource operator -(SerializedResource a, SerializedResource b)
    {
        return a + (-b);
    }
    public static SerializedResource operator -(SerializedResource a, int b)
    {
        return a + (-b);
    }
}
