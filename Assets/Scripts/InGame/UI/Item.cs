using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject structObject; // �ڽſ��� �Ҵ�� ��ġ�� ������, �ν����Ϳ��� ����

    private GameObject InspectorObject = null;

    public void OnClickItem() // item ��ư Ŭ���� ȣ��
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
            content += "\n��Ȯ ���� : ";
            content += crop.SaleCost.ToString();
            content += "\n�䱸 ����ġ : ";
            content += crop.MaxGrowth.ToString();
            content += "\n�� ����ġ : ";
            content += crop.SeasonGrowth[0].ToString();
            content += "\n���� ����ġ : ";
            content += crop.SeasonGrowth[1].ToString();
            content += "\n���� ����ġ : ";
            content += crop.SeasonGrowth[2].ToString();
            content += "\n�ܿ� ����ġ : ";
            content += crop.SeasonGrowth[3].ToString();
        }
        else
        {
            content += "\nü�� : ";
            content += so.MaxHP.ToString();
            content += "\n���� : ";
            content += so.Defence.ToString();
            content += "\n���� : ";
            content += so.HealAmount.ToString();

            if (so is Turret)
            { 
                Turret turret = (Turret)so;
                content += "\n���ݷ� : ";
                content += turret.AttackPower.ToString();
                content += "\n���ݼӵ� : ";
                content += turret.AttackSpeed.ToString();
                content += "\n��Ÿ� : ";
                content += turret.AttackRange.ToString();
            }
            else if(so is DeadBoomTrap)
            {
                DeadBoomTrap trap = (DeadBoomTrap)so;
                content += "\n���ݷ� : ";
                content += trap.AttackPower.ToString();
                content += "\n��Ÿ� : ";
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
