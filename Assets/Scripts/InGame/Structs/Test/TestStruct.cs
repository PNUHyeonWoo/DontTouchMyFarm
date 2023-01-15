using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStruct : StructObject
{
    public override void UpdateDay(int day)
    {
        return;
    }

    public override StructType GetStructType()
    {
        return StructType.Turret;
    }
}
