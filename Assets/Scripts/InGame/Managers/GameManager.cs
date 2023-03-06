using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    [SerializeField]
    private GameObject gameOverObject;
    private int isPause = 0; // 0이면 진행, 1이상이면 일시정지 상태
    private float gameSpeed = 1.0f;
    private void Awake()
    {
        gameManager = this;
        Time.timeScale = gameSpeed;
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
            Time.timeScale = gameSpeed;
            return;
        }

        if (isPause > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = gameSpeed;

    }

    public void GameOver() 
    {
        SetPause(true);
        gameOverObject.SetActive(true);
    }
    public void ToMenu() 
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void changeSpeed() 
    {
        if (gameSpeed == 1.0f)
        {
            gameSpeed = 3.0f;
            Time.timeScale = gameSpeed;
        }
        else if (gameSpeed == 3.0f)
        {
            gameSpeed = 1.0f;
            Time.timeScale = gameSpeed;
        }
    }

}
