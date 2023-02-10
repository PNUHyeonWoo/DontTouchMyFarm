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
        GameManager.gameManager.GameOver(); // 게임 종료
    }

    new void Start()
    {
        base.Start();
    }
}
