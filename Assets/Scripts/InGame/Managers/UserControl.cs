using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UserControl : MonoBehaviour
{
    const float sellRatio = 0.5f; 

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
                LeftClickAction();

            if(Input.GetMouseButtonDown(1))
                RightClickAction();
        }
    }


    void LeftClickAction() 
    {
        if (!StructInterface.SelectStruct)
            return; // ���� ��ġ�� ���� �� ����

        if (!TopUI.topUI.PlusMoney(-StructInterface.GetStructInterface().Cost))//�� ����
            return; //�� ������ �� ����

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray); //ī�޶󿡼� ���콺�� ����ĳ��Ʈ

        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<Ground>())
            {
                Ground.ground.InstallSelectStruct(new Vector2(hit.point.x, hit.point.z)); //ground�� ���߽� �ش� ��ġ�� ��ġ�� ��ġ
                break;
            }
    }

    void RightClickAction()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray); //ī�޶󿡼� ���콺�� ����ĳ��Ʈ

        
        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<StructInterface>()) // ó�� ������ ��ġ�� �Ǹ� �� �ı�
            {
                StructInterface si = hit.transform.GetComponent<StructInterface>();
                TopUI.topUI.PlusMoney((int)(si.Cost * sellRatio));
                Ground.ground.DestroyStruct(si);
            }
    }
}
