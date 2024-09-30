using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Data")]
public class ResourceData : ScriptableObject, IEqualityComparer<ResourceData>
{
    public string Id;
    public string DisplayName;
    public string DisplayDescription;

    public Sprite Sprite;
    public override bool Equals(object other)
    {
        if (other is not ResourceData)
        {
            return false;
        }

        return Id == ((ResourceData)other).Id;
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }

    bool IEqualityComparer<ResourceData>.Equals(ResourceData x, ResourceData y) => x.Id == y.Id;

    int IEqualityComparer<ResourceData>.GetHashCode(ResourceData obj) => obj.Id.GetHashCode();
}