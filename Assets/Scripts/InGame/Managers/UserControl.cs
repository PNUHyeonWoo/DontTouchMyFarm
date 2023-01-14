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
            return; // 선택 설치물 없을 시 리턴

        if (!TopUI.topUI.PlusMoney(-StructInterface.GetStructInterface().Cost))//돈 감소
            return; //돈 부족할 시 리턴

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray); //카메라에서 마우스로 레이캐스트

        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<Ground>())
            {
                Ground.ground.InstallSelectStruct(new Vector2(hit.point.x, hit.point.z)); //ground에 적중시 해당 위치에 설치물 설치
                break;
            }
    }

    void RightClickAction()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray); //카메라에서 마우스로 레이캐스트

        
        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<StructInterface>()) // 처음 적중한 설치물 판매 및 파괴
            {
                StructInterface si = hit.transform.GetComponent<StructInterface>();
                TopUI.topUI.PlusMoney((int)(si.Cost * sellRatio));
                Ground.ground.DestroyStruct(si);
            }
    }
}
