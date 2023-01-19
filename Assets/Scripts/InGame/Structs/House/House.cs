using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : StructObject
{
    public override StructType GetStructType()
    {
        return StructType.House;
    }

    protected override void Dead()
    {
        base.Dead();
        Application.Quit(); // ���� ����
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }
}
