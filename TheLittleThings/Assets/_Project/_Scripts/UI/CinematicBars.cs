using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CinematicBars : MonoBehaviour
{
    public bool barsActive {get; private set;}
    [SerializeField] private RectTransform topBar, bottomBar;
    [SerializeField] private float topBarInactivePos;
    [SerializeField] private float topBarActivePos, bottomBarInactivePos, bottomBarActivePos;
    [SerializeField] private float tweenTime;
    [SerializeField] private Ease ease;


    private void Update()
    {
        // Debug for now
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleBars();       
        }
    }

    public void ToggleBars()
    {
        if (barsActive)
        {
            DeactivateBars();
        }
        else
        {
            ActivateBars();
        }
    }

    public void ActivateBars()
    {
        barsActive = true;
        topBar.DOAnchorPosY(topBarActivePos, tweenTime).SetEase(ease);
        bottomBar.DOAnchorPosY(bottomBarActivePos, tweenTime).SetEase(ease);
        PlayerUIManager.Instance?.SetInGameUI(false);
    }

    public void DeactivateBars()
    {
        barsActive = false;
        topBar.DOAnchorPosY(topBarInactivePos, tweenTime).SetEase(ease);
        bottomBar.DOAnchorPosY(bottomBarInactivePos, tweenTime).SetEase(ease);
        PlayerUIManager.Instance?.SetInGameUI(true);
    }
    
    
}
