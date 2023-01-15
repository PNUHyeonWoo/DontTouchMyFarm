using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    private int isPause = 0; // 0이면 진행, 1이상이면 일시정지 상태
    private void Awake()
    {
        if (gameManager)
            return;
        gameManager = this;
    }

    public bool GetPause() 
    {
        return isPause > 0;
    }

    public void SetPause(bool pause)
    {
        isPause += pause ? 1 : -1;

        if (isPause < 0) // 일시정지 횟수보다 일시정지 해제 횟수가 많은 경우 에러 로그 발생
        {
            Debug.Log("invaild Pause");
            isPause = 0;
            Time.timeScale = 1;
            return;
        }

        if (isPause > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }

}
