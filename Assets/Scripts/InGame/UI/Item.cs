using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject structObject; // 자신에게 할당된 설치물 프리팹, 인스펙터에서 설정

    private GameObject InspectorObject = null;

    public void OnClickItem() // item 버튼 클릭시 호출
    {
        if (!Day.day.IsNight)
        {
            UISound.uiSound.PlaySound(0);

            if(StructObject.SelectStruct != structObject)
                StructObject.SelectStruct = structObject;
            else
                StructObject.SelectStruct = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InspectorObject = Instantiate(BottomUI.bottomUI.inspectorPrefab);
        RectTransform rt = InspectorObject.GetComponent<RectTransform>();
        rt.SetParent(BottomUI.bottomUI.transform);
        rt.SetAsLastSibling();


        StructObject so = structObject.GetComponent<StructObject>();

        string content = structObject.name + " (" + so.Cost.ToString() + ")";

        if (so is Crops)
        {
            Crops crop = (Crops)so;
            content += "\n수확 수익 : ";
            content += crop.SaleCost.ToString();
            content += "\n요구 성장치 : ";
            content += crop.MaxGrowth.ToString();
            content += "\n봄 성장치 : ";
            content += crop.SeasonGrowth[0].ToString();
            content += "\n여름 성장치 : ";
            content += crop.SeasonGrowth[1].ToString();
            content += "\n가을 성장치 : ";
            content += crop.SeasonGrowth[2].ToString();
            content += "\n겨울 성장치 : ";
            content += crop.SeasonGrowth[3].ToString();
        }
        else
        {
            content += "\n체력 : ";
            content += so.MaxHP.ToString();
            content += "\n방어력 : ";
            content += so.Defence.ToString();
            content += "\n힐량 : ";
            content += so.HealAmount.ToString();

            if (so is Turret)
            { 
                Turret turret = (Turret)so;
                content += "\n공격력 : ";
                content += turret.AttackPower.ToString();
                content += "\n공격속도 : ";
                content += turret.AttackSpeed.ToString();
                content += "\n사거리 : ";
                content += turret.AttackRange.ToString();
            }
            else if(so is DeadBoomTrap)
            {
                DeadBoomTrap trap = (DeadBoomTrap)so;
                content += "\n공격력 : ";
                content += trap.AttackPower.ToString();
                content += "\n사거리 : ";
                content += trap.AttackRange.ToString();
            }
        }

        Text txt = InspectorObject.transform.Find("Text").GetComponent<Text>();
        txt.text = content;

        //rt.sizeDelta = txt.GetComponent<RectTransform>().sizeDelta;
        //txt.GetComponent<RectTransform>().sizeDelta = new Vector2(txt.preferredWidth, txt.preferredHeight);

        rt.position = new Vector3(2000,2000, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InspectorObject)
            Destroy(InspectorObject);

        InspectorObject = null;
    }

}
