using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest Manager")]
public class QuestManager : ScriptableSingleton<Resources>
{
    public List<QuestData> QuestList;

    public Dictionary<string, QuestData> AllQuests;

    public Dictionary<string, QuestData> ActiveQuests;
    public Dictionary<string, QuestData> CompletedQuests;

    private void Awake()
    {
        AllQuests = new Dictionary<string, QuestData>();
        ActiveQuests = new Dictionary<string, QuestData>();
        CompletedQuests = new Dictionary<string, QuestData>();

        foreach (var item in QuestList)
        {
            AllQuests.Add(item.TypeID, item);

            if (item.IsActive)
            {
                ActiveQuests.Add(item.TypeID, item);
            }
            else if (item.IsCompleted)
            {
                CompletedQuests.Add(item.TypeID, item);
            }
        }
    }
    /// <summary>
    /// Sets designated quest to active.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>List of resource requirements.</returns>
    /// <exception cref="System.Exception"></exception>
    public List<Resource> TakeQuest(string id)
    {
        if (AllQuests.TryGetValue(id, out QuestData data))
        {
            data.IsActive = true;

            ActiveQuests.Add(id, data);

            return data.Requirements;
        }
        else
        {
            throw new System.Exception("Error: invalid quest ID.");
        }
    }

    /// <summary>
    /// Sets designated quest to completed.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>List of rewards.</returns>
    /// <exception cref="System.Exception"></exception>
    public List<Resource> CompleteQuest(string id)
    {
        if (ActiveQuests.TryGetValue(id, out QuestData data))
        {
            data.IsActive = false;
            data.IsCompleted = true;

            CompletedQuests.Add(id, data);
            ActiveQuests.Remove(id);

            return data.Rewards;
        }
        else
        {
            throw new System.Exception("Error: quest ID was not active.");
        }
    }
}