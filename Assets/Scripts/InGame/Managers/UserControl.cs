using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class UserControl : MonoBehaviour
{
    private const float sellRatio = 0.5f;
    private GameObject installFloor;

    private void Start()
    {
        installFloor = GameObject.Find("InstallFloor");
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
                LeftClickAction();

            if (Input.GetMouseButtonDown(1))
                RightClickAction();

            UpdateInstallFloor();

        }
        else
        { 
            installFloor.SetActive(false);
        }
    }


    void LeftClickAction() 
    {
        if (!StructObject.SelectStruct)
            return; // 선택 설치물 없을 시 리턴

        if (!TopUI.topUI.PlusMoney(-StructObject.GetStructComponent().Cost))//돈 감소
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
        Array.Sort(hits, (a, b) => a.distance < b.distance ? -1 : 1);

        
        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<StructObject>()) // 처음 적중한 설치물 판매 및 파괴
            {
                StructObject si = hit.transform.GetComponent<StructObject>();
                TopUI.topUI.PlusMoney((int)(si.Cost * sellRatio));
                Ground.ground.DestroyStruct(si);
                return;
            }
    }

    void UpdateInstallFloor()
    {
        if (!StructObject.SelectStruct)
        { 
            installFloor.SetActive(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray); //카메라에서 마우스로 레이캐스트

        foreach (RaycastHit hit in hits)
            if (hit.transform.GetComponent<Ground>())
            {
                installFloor.SetActive(true);
                installFloor.transform.localScale = new Vector3(
                    StructObject.GetStructComponent().Size,
                    installFloor.transform.localScale.y,
                    StructObject.GetStructComponent().Size);
                Vector4 result = Ground.ground.GetInstallFloorPosition(new Vector2(hit.point.x, hit.point.z));
                installFloor.transform.position = result;
                installFloor.GetComponent<MeshRenderer>().material.color = result.w == 1 ? Color.green : Color.red;
                return;
            }

        installFloor.SetActive(false);
    }
}
