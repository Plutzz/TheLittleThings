using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string DisplayDescription;

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
    public List<SerializedResource> Requirements;

    /// <summary>
    /// Rewards gained from the quest.
    /// </summary>
    public List<SerializedResource> Rewards;

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
