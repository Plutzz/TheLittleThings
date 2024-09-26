using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Manager")]
public class ResourceManager : ScriptableSingleton<ResourceManager>
{
    public List<ResourceData> ResourceDataList;

    public Dictionary<string, ResourceData> ResourceData
    {
        get;
        private set;
    }


    private void OnEnable()
    {
        ResourceData = new Dictionary<string, ResourceData>();

        foreach (var item in ResourceDataList)
        {
            ResourceData.Add(item.Id, item);
        }
    }
    private void OnDisable()
    {
        ResourceData = null;
    }
    /// <summary>
    /// Converts a regular resource list to a serialized one.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<SerializedResource> ToSerializableList(List<Resource> list)
    {
        return list.ConvertAll((Resource r) => r.GetIdentifier());
    }

    /// <summary>
    /// Converts a serialized resource list to a regular one.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<Resource> ToResourceList(List<SerializedResource> list)
    {
        return list.ConvertAll((SerializedResource r) => r.GetResource());
    }
    /// <summary>
    /// Adds two resource lists together, combining their amount if two have the same id.
    /// </summary>
    /// <param name="a">1st list</param>
    /// <param name="b">2nd list</param>
    /// <returns>Combined list</returns>
    public static List<Resource> AddList(List<Resource> a, List<Resource> b)
    {
        List<Resource> ret = new List<Resource>();

        foreach (var item in a)
        {
            if (!b.Contains(item))
            {
                ret.Add(item);
            }
        }

        foreach (Resource resource in b)
        {
            int count = GetResourceCountInList(a, resource.Data.Id);

            ret.Add(resource + count);
        }

        return ret;
    }
    /// <summary>
    /// Subtracts two resource lists together, combining their amount if two have the same id.
    /// Same as a + (-b) where the negation negates all the amounts in b.
    /// </summary>
    /// <param name="a">1st list</param>
    /// <param name="b">2nd list</param>
    /// <returns>Combined list</returns>
    public static List<Resource> SubtractList(List<Resource> a, List<Resource> b)
    {
        List<Resource> ret = new List<Resource>();

        foreach (var item in a)
        {
            if (!b.Contains(item))
            {
                ret.Add(item);
            }
        }

        foreach (Resource resource in b)
        {
            int count = GetResourceCountInList(a, resource.Data.Id);

            ret.Add(-resource + count);
        }

        return ret;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resources">List of resources. Should only contain one type of resource max.</param>
    /// <param name="id"></param>
    /// <returns>Amount of the first resource with the same id the list has, if applicable. Otherwise returns 0.</returns>
    public static int GetResourceCountInList(List<Resource> resources, string id)
    {
        foreach (var resource in resources)
        {
            if (resource.Data.Id == id)
            {
                return resource.Amount;
            }
        }
        return 0;
    }
}