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
        float x = Mathf.Max(mosPos.x - rt.sizeDelta.x / 2, rt.sizeDelta.x / 2);

        rt.position = new Vector3(x,mosPos.y + rt.sizeDelta.y/2, 0);

    }
}
