using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject structObject; // �ڽſ��� �Ҵ�� ��ġ�� ������, �ν����Ϳ��� ����

    public void OnClickItem() // item ��ư Ŭ���� ȣ��
    {
        StructObject.SelectStruct = structObject;
    }

}
