using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructInterface : MonoBehaviour
{
    private static GameObject selectStruct; // Item ��ư���� ���õ� ��ġ�� ������

    private long cost;
    private int size;
    private int[] installGrid;
    public long Cost
    {
        get
        {
            return cost;
        }
    }

    public int Size
    {
        get
        {
            return size;
        }
    }

    public int[] InstallGrid
    {
        get
        {
            return installGrid;
        }
        set
        {
            installGrid = value;
        }
    }

    public static GameObject SelectStruct
    {
        get { return selectStruct; }
        set { selectStruct = value; }
    }


    public static StructInterface GetStructInterface() 
    {
        return selectStruct.GetComponent<StructInterface>();
    }

    public static void CreateStruct(Transform parent, Vector3 position, int[] installGrid) // selectStruct ���������� ��ġ�� ����
    {
        GameObject newObj = Instantiate(selectStruct);
        newObj.transform.position = position;
        newObj.transform.SetParent(parent);
        newObj.GetComponent<StructObject>().InstallGrid = installGrid;
    }

    public abstract void UpdateDay(int day); //�������� �Ѿ �� Day���� �� ��ġ������ ȣ�����ִ� �޼ҵ�

    protected bool PlusMoney(long money) 
    {
        return TopUI.topUI.PlusMoney(money);
    }

}
