using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Data")]
public class ResourceData : ScriptableObject
{
    public string TypeID;
    public string DisplayName;
    public string DisplayDescription;

    public Sprite Sprite;

    public override bool Equals(object other)
    {
        if (other is not ResourceData)
        {
            return false;
        }

        return TypeID == ((ResourceData)other).TypeID;
    }

    public override int GetHashCode()
    {
        return TypeID.GetHashCode();
    }
}