using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTimer : MonoBehaviour
{
    private float remainTime = 0.0f;

    public float RemainTime 
    {
        get { return remainTime; }
        set { remainTime = value; } 
    }

    private void Update()
    {
        if (remainTime > 0)
            remainTime -= Time.deltaTime;
        remainTime = remainTime < 0 ? 0 : remainTime;
    }
}
