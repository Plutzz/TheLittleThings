using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [field: SerializeField] public CinematicBars cinematicBars {get; private set;}
}
