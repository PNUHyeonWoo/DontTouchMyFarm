using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;
    private int isPause = 0; // 0�̸� ����, 1�̻��̸� �Ͻ����� ����
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

        if (isPause < 0) // �Ͻ����� Ƚ������ �Ͻ����� ���� Ƚ���� ���� ��� ���� �α� �߻�
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