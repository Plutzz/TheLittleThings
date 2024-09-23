using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Serializable version of resource. Used in unity editor.
/// </summary>
[Serializable]
public struct SerializableResource : IEqualityComparer<SerializableResource>
{
    public string Id;
    public int Amount;

    public SerializableResource(string id, int amount)
    {
        Id = id;

        Amount = amount;
    }

    public bool IsTheSameResource(string id)
    {
        return Id == id;
    }

    bool IEqualityComparer<SerializableResource>.Equals(SerializableResource a, SerializableResource b)
    {
        return a.Equals(b);
    }

    int IEqualityComparer<SerializableResource>.GetHashCode(SerializableResource obj)
    {
        return obj.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is not SerializableResource)
        {
            return false;
        }
        return ((SerializableResource)obj).Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"Serialized Resource {{id: {Id} amount: {Amount}}}";
    }

    public Resource GetResource()
    {
        return new Resource(this);
    }

    public static SerializableResource operator +(SerializableResource a)
    {
        return a;
    }
    public static SerializableResource operator -(SerializableResource a)
    {
        return new SerializableResource(a.Id, -a.Amount);
    }

    public static SerializableResource operator +(SerializableResource a, SerializableResource b)
    {
        if (a.Id == b.Id)
        {
            return new SerializableResource(a.Id, a.Amount + b.Amount);
        }
        throw new InvalidResourceOperation();
    }

    public static SerializableResource operator +(SerializableResource a, int b)
    {
        return new SerializableResource(a.Id, a.Amount + b);
    }

    public static SerializableResource operator -(SerializableResource a, SerializableResource b)
    {
        return a + (-b);
    }
    public static SerializableResource operator -(SerializableResource a, int b)
    {
        return a + (-b);
    }
}
