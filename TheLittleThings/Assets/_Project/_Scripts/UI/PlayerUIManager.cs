using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    public void DisableUI()
    {
        gameObject.SetActive(false);
    }

    public void EnableUI()
    {
        gameObject.SetActive(true);
    }
}
