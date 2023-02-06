using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] StructLists = {null, null, null, null};

    public GameObject inspectorPrefab;

    public static BottomUI bottomUI;

    public void Awake()
    {
        bottomUI = this;
    }

    public void SetActivityList(int index) 
    {
        for(int i = 0;i<StructLists.Length;i++)
            StructLists[i].SetActive(i==index);
    } 

}
