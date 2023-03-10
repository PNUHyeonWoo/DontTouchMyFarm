using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class UserControl : MonoBehaviour
{
    private const float sellRatio = 0.5f;
    private GameObject installFloor;

    private const float zoomSpeed = 30;
    private const float rotateSpeed = 1;

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

        if (Input.GetKey(KeyCode.LeftArrow))
            RotateCamera(-Time.deltaTime * rotateSpeed);
        if (Input.GetKey(KeyCode.RightArrow))
            RotateCamera(Time.deltaTime * rotateSpeed);
        if (Input.GetKey(KeyCode.UpArrow))
            ZoomCamera(-Time.deltaTime * zoomSpeed);
        if (Input.GetKey(KeyCode.DownArrow))
            ZoomCamera(Time.deltaTime * zoomSpeed);

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
                if (!Ground.ground.InstallSelectStruct(new Vector2(hit.point.x, hit.point.z)))//ground에 적중시 해당 위치에 설치물 설치
                    TopUI.topUI.PlusMoney(StructObject.GetStructComponent().Cost);
                else
                    UISound.uiSound.PlaySound(2);
                return;
            }

        TopUI.topUI.PlusMoney(StructObject.GetStructComponent().Cost);
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
                if (si is House)
                    return;

                UISound.uiSound.PlaySound(1);
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

    void RotateCamera(float r) 
    {
        Vector3 lookAt = Camera.main.transform.rotation * Vector3.forward + Camera.main.transform.position;
        float x = Camera.main.transform.position.x;
        float y = Camera.main.transform.position.y;
        float z = Camera.main.transform.position.z;
        Camera.main.transform.position = new Vector3((float)(x * Math.Cos(r) - z * Math.Sin(r)), y ,(float)(x * Math.Sin(r) + z * Math.Cos(r)));
        x = lookAt.x;
        y = lookAt.y;
        z = lookAt.z;
        lookAt = new Vector3((float)(x * Math.Cos(r) - z * Math.Sin(r)), y,(float)(x * Math.Sin(r) + z * Math.Cos(r)));
        Camera.main.transform.LookAt(lookAt);
    }

    void ZoomCamera(float d)
    {
        Vector3 lookAt = Camera.main.transform.rotation * Vector3.forward;
        Vector3 result = Camera.main.transform.position - lookAt * d;
        if (result.y > 35 || result.y < 10)
            return;
        Camera.main.transform.position = result;
    }
}
