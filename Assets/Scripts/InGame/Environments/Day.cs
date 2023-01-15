using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    private const float NIGHT_TIME = 120.0f; //밤 지속 시간 상수

    private NightTimer nightTimer;
    private GameObject bottomUI;
    private GameObject ground;
    private bool isNight = false;
    private int days = 1; // 현재 일수
    void Start()
    {
        nightTimer = gameObject.GetComponent<NightTimer>();
        bottomUI = GameObject.Find("BottomUI");
        ground = GameObject.Find("Ground");
    }


    void Update()
    {
        if (isNight && nightTimer.RemainTime <= 0)
        {
            isNight = false;
            UpdateDay(); 
        }
    }

    void OnClickNight() // 낮 스킵 버튼 클릭 시 호출
    {
        isNight = true;
        nightTimer.RemainTime = NIGHT_TIME;
        //몬스터 소환
    }

    void UpdateDay() //밤이 끝나고 다음날이 될때 호출
    {
        days++;
        GameObject[] structs = GameObject.FindGameObjectsWithTag("Struct");
        foreach (GameObject st in structs)
            st.GetComponent<StructObject>().UpdateDay(days);
    }

}
