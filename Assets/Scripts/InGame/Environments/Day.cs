using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    [SerializeField]
    private float NIGHT_TIME = 120.0f; //�� ���� �ð� ���
    [SerializeField]
    static private int[] SEASON_DAYS = {5,5,5,5}; //���� ���� �� ��� ��, ����, ����, �ܿ�

    enum Season
    {
        Spring = 0,
        Summer = 1,
        Fall = 2,
        Winter = 3
    }

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

    static Season GetSeason(int days) 
    {
        days = (days - 1) % SEASON_DAYS.Sum();
        int tmpSum = 0;
        for (int i = 0; i < 4; i++)
        {
            tmpSum += SEASON_DAYS[i];
            if (days < tmpSum)
                return (Season)i;
        }
        return (Season)0;
    } 


}
