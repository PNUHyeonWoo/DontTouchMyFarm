using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NightTimer : MonoBehaviour
{
    private float remainTime = 0.0f;
    private TMP_Text timerText;

    public float RemainTime 
    {
        get { return remainTime; }
        set { remainTime = value; } 
    }

    public TMP_Text TimerText
    {
        set { timerText = value; }
    }

    private void Update()
    {
        if (remainTime > 0)
            remainTime -= Time.deltaTime;
        remainTime = remainTime < 0 ? 0 : remainTime;
        timerText.text = ((int)remainTime).ToString();
    }
}
