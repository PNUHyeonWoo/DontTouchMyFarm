using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Day : MonoBehaviour
{

    public static Day day;
    [SerializeField]
    private float NIGHT_TIME = 5.0f; //밤 지속 시간 상수
    [SerializeField]
    static private int[] SEASON_DAYS = {5, 5, 5, 5}; //계절 지속 일 상수 봄, 여름, 가을, 겨울

    [SerializeField]
    private GameObject skipButton;
    [SerializeField]
    private GameObject timerUI;
    [SerializeField]
    private TMP_Text dayText;

    [SerializeField]
    private Light sunLight;

    private Color[] groundColor = new Color[4];
    private Color[] lightColor = new Color[4];

    private MonsterSpawner monsterSpawner;

    public enum Seasons
    {
        Spring = 0,
        Summer = 1,
        Fall = 2,
        Winter = 3
    }
    int season;

    private NightTimer nightTimer;
    private GameObject bottomUI;
    private GameObject ground;
    private bool isNight = false;

    public bool IsNight 
    {
        get { return isNight; }
    }

    public int Season
    {
        get { return season; }
    }

    private int days = 1; // 현재 일수

    private void Awake()
    {
        day = this;
    }

    void Start()
    {
        nightTimer = gameObject.GetComponent<NightTimer>();
        monsterSpawner = gameObject.GetComponent<MonsterSpawner>();
        nightTimer.TimerText = timerUI.transform.GetComponentInChildren<TMP_Text>();
        bottomUI = GameObject.Find("BottomUI");
        ground = GameObject.Find("Ground");

        season = 0;

        groundColor[0] = new Color(147 / 255f, 202 / 255f, 47 / 255f);
        groundColor[1] = new Color(49 / 255f, 147 / 255f, 41 / 255f);
        groundColor[2] = new Color(191 / 255f, 144 / 255f, 45 / 255f);
        groundColor[3] = new Color(223 / 255f, 230 / 255f, 234 / 255f);

        lightColor[0] = new Color(255 / 255f, 244 / 255f, 214 / 255f);
        lightColor[1] = new Color(214 / 255f, 255 / 255f, 244 / 255f);
        lightColor[2] = new Color(255 / 255f, 219 / 255f, 214 / 255f);
        lightColor[3] = Color.white;

        ground.GetComponent<MeshRenderer>().material.color = groundColor[season];
        sunLight.color = lightColor[season];
    }


    void Update()
    {
        if (isNight && Monster.totalAmount <= 0)
            nightTimer.RemainTime = 0;

        if (isNight && nightTimer.RemainTime <= 0)
        {
            KillAllMonster();
            isNight = false;
            UpdateDay(); 
        }
    }

    public void OnClickNight() // 낮 스킵 버튼 클릭 시 호출
    {
        UISound.uiSound.PlaySound(0);

        isNight = true;
        nightTimer.RemainTime = NIGHT_TIME;

        skipButton.SetActive(false);
        timerUI.SetActive(true);

        sunLight.intensity = 0.2f;

        monsterSpawner.SpawnMonster(days);
        StructObject.SelectStruct = null;
    }

    void UpdateDay() //밤이 끝나고 다음날이 될때 호출
    {
        days++;
        dayText.text = days.ToString();

        skipButton.SetActive(true);
        timerUI.SetActive(false);

        sunLight.intensity = 1.0f;

        GameObject[] structs = GameObject.FindGameObjectsWithTag("Struct");
        foreach (GameObject st in structs)
            st.GetComponent<StructObject>().UpdateDay(days);

        season = (int)GetSeason(days);
        ground.GetComponent<MeshRenderer>().material.color = groundColor[season];
        Debug.Log(season);
    }

    public static Seasons GetSeason(int days) 
    {
        days = (days - 1) % SEASON_DAYS.Sum();
        int tmpSum = 0;
        for (int i = 0; i < 4; i++)
        {
            tmpSum += SEASON_DAYS[i];
            if (days < tmpSum)
                return (Seasons)i;
        }
        return (Seasons)0;
    }

    private void KillAllMonster() 
    {
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
            monster.GetComponent<Monster>().Dead();

        Monster.totalAmount = 0;
    }


}
