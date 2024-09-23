using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class QuestData : ScriptableObject
{
    public string TypeID;
    public string DisplayName;
    public string DisplayDescription;

    public Sprite Sprite;

    public Vector2 Location;
    public float Radius;

    public List<Resource> Requirements;

    public List<Resource> Rewards;

    public List<QuestData> Prerequisites;

    public bool IsCompleted;
    public bool IsActive;
}
