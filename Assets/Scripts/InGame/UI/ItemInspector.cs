using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{

    void Update()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 mosPos = Input.mousePosition;
        rt.position = new Vector3(mosPos.x - rt.sizeDelta.x/2 - 3,mosPos.y + rt.sizeDelta.y/2, 0);

    }
}
