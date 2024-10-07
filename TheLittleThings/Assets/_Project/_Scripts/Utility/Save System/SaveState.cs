using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveState
{
    public string SaveName;
    public int hp, damage, movementSpeed;

    public List<Resource> resources;

    public SaveState(string saveName)
    {
        SaveName = saveName;
        hp = 0;
        resources = new List<Resource>();
    }
}