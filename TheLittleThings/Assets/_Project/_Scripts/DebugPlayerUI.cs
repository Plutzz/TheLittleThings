using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugPlayerUI : MonoBehaviour
{
    public Rigidbody player;
    public TextMeshProUGUI speedText;
    //public Vector3 flatVel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 flatVel = new Vector3(player.velocity.x, 0, player.velocity.z);
        speedText.text = "Speed: " + flatVel.magnitude + "\n(" + flatVel.x + ", " + flatVel.z + ")";
    }
}
