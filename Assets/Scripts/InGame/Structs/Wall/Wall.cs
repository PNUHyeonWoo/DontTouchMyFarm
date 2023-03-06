using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : StructObject
{
    public override StructType GetStructType()
    {
        return StructType.Wall;
    }
    
    new void Start()
    {
        base.Start();
    }
}
