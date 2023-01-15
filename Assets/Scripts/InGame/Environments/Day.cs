using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    private const float NIGHT_TIME = 120.0f; //�� ���� �ð� ���

    private NightTimer nightTimer;
    private GameObject bottomUI;
    private GameObject ground;
    private bool isNight = false;
    private int days = 1; // ���� �ϼ�
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

    void OnClickNight() // �� ��ŵ ��ư Ŭ�� �� ȣ��
    {
        isNight = true;
        nightTimer.RemainTime = NIGHT_TIME;
        //���� ��ȯ
    }

    void UpdateDay() //���� ������ �������� �ɶ� ȣ��
    {
        days++;
        GameObject[] structs = GameObject.FindGameObjectsWithTag("Struct");
        foreach (GameObject st in structs)
            st.GetComponent<StructObject>().UpdateDay(days);
    }

}
