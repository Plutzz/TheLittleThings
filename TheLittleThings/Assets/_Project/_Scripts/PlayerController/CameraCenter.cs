using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);

        else if (gameObject.transform.rotation.eulerAngles.z > 359)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 1 / 10f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
