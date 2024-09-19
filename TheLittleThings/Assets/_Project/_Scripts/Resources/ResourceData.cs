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
}