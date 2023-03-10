using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        topUI = this;
    }

    private void Start()
    {
        moneyText.text = money.ToString();
    }

    public void OnClickPause() //일시정지 버튼 클릭 시 호출
    {
        isPauseByButton = !isPauseByButton;
        GameManager.gameManager.SetPause(isPauseByButton);
        pauseMenu.SetActive(isPauseByButton);
    }

    public bool PlusMoney(long money) //돈을 추가또는 감소, 감소 시 마이너스가 되면 감소를 취소하고 false를 리턴
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

    public void ToMenu() 
    {
        SceneManager.LoadScene("MenuScene");
    }

}
