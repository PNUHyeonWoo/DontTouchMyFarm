using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructObject : MonoBehaviour
{
    public enum StructType 
    { 
        Props = 0,
        Turret = 1,
        Wall = 2,
        Trap = 3,
        House = 4
    }

    private static GameObject selectStruct; // Item 버튼으로 선택된 설치물 프리팹
    [SerializeField]
    private long cost;
    [SerializeField]
    private int size;
    private int[] installGrid;

    [SerializeField]
    private float maxHP;
    private float HP;
    [SerializeField]
    private float defence;
    [SerializeField]
    private float healAmount;
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


    public static StructObject GetStructComponent() 
    {
        return selectStruct.GetComponent<StructObject>();
    }

    public static void CreateStruct(Transform parent, Vector3 position, int[] installGrid) // selectStruct 프리팹으로 설치물 생성
    {
        GameObject newObj = Instantiate(selectStruct);
        newObj.transform.position = position;
        newObj.transform.SetParent(parent);
        newObj.GetComponent<StructObject>().InstallGrid = installGrid;
        newObj.layer = LayerMask.NameToLayer("Struct");
    }
    public abstract StructType GetStructType();
    protected virtual void Dead() 
    {
        Ground.ground.DestroyStruct(this);
    }

    public float AddHP(float value)
    {
        if (value < 0)
        {
            value += defence;
            value = value > 0 ? 0 : value;
        }

        HP += value;
        if (HP <= 0)
            Dead();
        return HP;
    }
    public virtual void UpdateDay(int day) 
    {
        HP += healAmount;
        HP = HP > maxHP ? maxHP: HP;
    } //다음날로 넘어갈 때 Day에서 각 설치물에서 호출해주는 메소드

    protected bool PlusMoney(long money) 
    {
        return TopUI.topUI.PlusMoney(money);
    }

    protected void Start()
    {
        HP = maxHP;
    }

}
