using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : StructObject
{
    [SerializeField]
    private StructType fakeType;

    public override StructType GetStructType()
    {
        return fakeType;
    }
}
