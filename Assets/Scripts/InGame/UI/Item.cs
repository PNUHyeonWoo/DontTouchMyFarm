using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject structObject; // 자신에게 할당된 설치물 프리팹, 인스펙터에서 설정

    public void OnClickItem() // item 버튼 클릭시 호출
    {
        StructObject.SelectStruct = structObject;
    }

}
