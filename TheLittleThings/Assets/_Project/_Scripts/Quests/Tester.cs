using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
[ExecuteInEditMode]
public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        List<Resource> presources = new List<Resource>()
        {
            new Resource("test_resource_1", 3),
            new Resource("test_resource_2", 3),
            new Resource("test_resource_3", 3),

        };
        QuestManager.instance.TakeQuest("quest_1", ref presources);

        foreach (var resource in presources)
        {
            Debug.Log(resource);
        }
        Debug.Log("before /\\ after \\/");
        QuestManager.instance.CompleteQuest("quest_1", ref presources);

        foreach (var resource in presources)
        {
            Debug.Log(resource);
        }
    }
}
#endif