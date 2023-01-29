using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopUI : MonoBehaviour
{
    public static TopUI topUI = null;
    [SerializeField]
    private long money = 0;
    [SerializeField]
    private TMP_Text moneyText;

    [SerializeField]
    private GameObject pauseMenu;
    private bool isPauseByButton = false;
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
        moneyText.text = money.ToString();
    }

    public void OnClickPause() //�Ͻ����� ��ư Ŭ�� �� ȣ��
    {
        isPauseByButton = !isPauseByButton;
        GameManager.gameManager.SetPause(isPauseByButton);
        pauseMenu.SetActive(isPauseByButton);
    }

    public bool PlusMoney(long money) //���� �߰��Ǵ� ����, ���� �� ���̳ʽ��� �Ǹ� ���Ҹ� ����ϰ� false�� ����
    {
        if (this.money + money >= 0)
        {
            this.money += money;
            moneyText.text = this.money.ToString();
            return true;
        }    
        else
            return false;
    }

}
