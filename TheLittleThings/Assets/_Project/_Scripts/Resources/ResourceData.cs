using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Data")]
public class ResourceData : ScriptableObject
{
    public string Id;
    public string DisplayName;
    public string DisplayDescription;

    public Sprite Sprite;

    private void Awake()
    {
        Debug.Log(Id ?? "awagga2");


    }
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
}