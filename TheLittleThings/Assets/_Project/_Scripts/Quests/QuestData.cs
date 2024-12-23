using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Quest")]
public class QuestData : ScriptableObject
{
    /// <summary>
    /// ID of the quest.
    /// </summary>
    public string Id;
    /// <summary>
    /// Display name of the quest.
    /// </summary>
    public string DisplayName;
    /// <summary>
    /// display description of the quest.
    /// </summary>
    [TextArea(2, 5)] public string DisplayDescription;

    /// <summary>
    /// Quest icon.
    /// </summary>
    public Sprite Sprite;

    /// <summary>
    /// Map quest location.
    /// </summary>
    public Vector2 Location;

    /// <summary>
    /// Requirements to start the quest.
    /// </summary>
    public List<Resource> Requirements;

    /// <summary>
    /// Rewards gained from the quest.
    /// </summary>
    public List<Resource> Rewards;

    /// <summary>
    /// Prerequisite Quest IDs.
    /// </summary>
    public List<string> Prerequisites;

    /// <summary>
    /// Is the quest completed.
    /// </summary>
    public bool IsCompleted;

    /// <summary>
    /// Is the quest active.
    /// </summary>
    public bool IsActive;
}
#endif