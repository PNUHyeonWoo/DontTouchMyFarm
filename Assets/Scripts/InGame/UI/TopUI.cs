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

    public void OnClickPause() //일시정지 버튼 클릭 시 호출
    {
        isPauseByButton = !isPauseByButton;
        GameManager.gameManager.SetPause(isPauseByButton);
    }

    public bool PlusMoney(long money) //돈을 추가또는 감소, 감소 시 마이너스가 되면 감소를 취소하고 false를 리턴
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
