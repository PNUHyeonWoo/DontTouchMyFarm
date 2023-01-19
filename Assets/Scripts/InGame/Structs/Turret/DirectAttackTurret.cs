using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttackTurret : Turret
{
    [SerializeField]
    private GameObject AttackEffect;
    protected override void Attack()
    {
        Vector3 lookPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(lookPos);
        target.AddHP(-attackPower);
        GameObject attackObejct = Instantiate(AttackEffect);
        attackObejct.transform.position = target.transform.position;
    }
}
