using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

[CreateAssetMenu(menuName = "Quest Manager")]
public class QuestManager : ScriptableSingleton<ResourceManager>
{
    public List<QuestData> QuestList;

    public Dictionary<string, QuestData> AllQuests;

    public Dictionary<string, QuestData> ActiveQuests;
    public Dictionary<string, QuestData> CompletedQuests;

    private void OnEnable()
    {
        AllQuests = new Dictionary<string, QuestData>();
        ActiveQuests = new Dictionary<string, QuestData>();
        CompletedQuests = new Dictionary<string, QuestData>();

        foreach (var item in QuestList)
        {
            AllQuests.Add(item.Id, item);

            if (item.IsActive)
            {
                ActiveQuests.Add(item.Id, item);
            }
            else if (item.IsCompleted)
            {
                CompletedQuests.Add(item.Id, item);
            }
        }
    }

    private void OnDisable()
    {
        AllQuests = null;
        ActiveQuests = null;
        CompletedQuests = null;
    }
    /// <summary>
    /// Returns if the quest is valid to be taken.
    /// </summary>
    /// <param name="id">Quest id</param>
    /// <returns>If the quest can be set to active.</returns>
    /// <exception cref="System.Exception"></exception>
    public bool CanQuestBeTaken(string id, List<Resource> playerResources)
    {
        if (IsQuestActive(id) || IsQuestComplete(id))
        {
            return false;
        }


        if (AllQuests.TryGetValue(id, out QuestData quest))
        {
            foreach (var q in quest.Prerequisites)
            {
                if (!IsQuestComplete(q))
                {
                    return false;
                }
            }

            foreach (var item in quest.Requirements)
            {
                if (ResourceManager.GetResourceCountInList(playerResources, item.Id) < item.Amount)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            throw new System.Exception("Error: quest ID was not active.");
        }

        
    }
    /// <summary>
    /// Returns if the quest is active.
    /// </summary>
    /// <param name="id">Quest id</param>
    /// <returns>If the quest inputted is active.</returns>
    /// <exception cref="System.Exception"></exception>
    public bool IsQuestActive(string id)
    {
        return ActiveQuests.ContainsKey(id);
    }
    /// <summary>
    /// Returns if the quest is completed.
    /// </summary>
    /// <param name="id">Quest id</param>
    /// <returns>If the quest inputted is completed.</returns>
    /// <exception cref="System.Exception"></exception>
    public bool IsQuestComplete(string id)
    {
        return CompletedQuests.ContainsKey(id);
    }
    /// <summary>
    /// Sets designated quest to active.
    /// </summary>
    /// <param name="id">Quest id</param>
    /// <returns>List of resource requirements.</returns>
    /// <exception cref="System.Exception"></exception>
    public List<SerializedResource> TakeQuest(string id)
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
    /// <param name="id">Quest id</param>
    /// <returns>List of rewards.</returns>
    /// <exception cref="System.Exception"></exception>
    public List<SerializedResource> CompleteQuest(string id)
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