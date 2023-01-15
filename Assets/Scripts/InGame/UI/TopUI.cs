using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : MonoBehaviour
{
    public static TopUI topUI = null;

    private long money = 0;
    private bool isPauseByButton = false;
    private Text timerText;
    public long Money
    {
        get { return money; }
    }

    private void Awake()
    {
        if (topUI)
            return;
        topUI = this;
    }

    private void Start()
    {
        //Text find;
    }

    public void OnClickPause() //�Ͻ����� ��ư Ŭ�� �� ȣ��
    {
        isPauseByButton = !isPauseByButton;
        GameManager.gameManager.SetPause(isPauseByButton);
    }

    public bool PlusMoney(long money) //���� �߰��Ǵ� ����, ���� �� ���̳ʽ��� �Ǹ� ���Ҹ� ����ϰ� false�� ����
    {
        if (this.money + money >= 0)
        {
            this.money += money;
            return true;
        }    
        else
            return false;
    }

    public void RenderTimer(float time) 
    { 
        timerText.text = time.ToString();
    }

}
