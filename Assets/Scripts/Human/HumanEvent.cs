using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEvent : MonoBehaviour
{

    public delegate void DieEvent(Human h);
    public delegate void FallEvent(Human h);
    //public delegate void MissionEvent(Mission m);
}


