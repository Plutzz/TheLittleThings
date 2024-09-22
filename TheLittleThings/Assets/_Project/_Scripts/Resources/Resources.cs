using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Manager")]
public class Resources : ScriptableSingleton<Resources>
{
    public List<ResourceData> ResourceDataList;

    public Dictionary<string, ResourceData> ResourceData;

    private void Awake()
    {
        ResourceData = new Dictionary<string, ResourceData>();

        foreach (var item in ResourceDataList)
        {
            ResourceData.Add(item.TypeID, item);
        }
    }
}