using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        List<Resource> a = new List<Resource>()
        {
            new Resource("test_resource_1", 1),
            new Resource("test_resource_2", 1),
            
        };
        List<Resource> b = new List<Resource>()
        {
            new Resource("test_resource_1", 1),
            new Resource("test_resource_2", 1),
            new Resource("test_resource_3", 1),
        };

        foreach (var awagga in ResourceManager.AddList(a, b))
        {
            Debug.Log(awagga);
        }
    }
}
