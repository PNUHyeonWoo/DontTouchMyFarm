using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private NightTimer timer;

    public NightTimer Timer 
    {
        set { timer = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.RemainTime <= 0)
            Dead();
    }

    abstract protected void Dead();
}
