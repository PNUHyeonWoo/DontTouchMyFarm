using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBoomTrap : Trap
{
    [SerializeField]
    private float attackPower;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private GameObject ExplosionEffect;

    /* ���� �׽�Ʈ��
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Dead();
        }
    }
    */

    protected override void Dead() // SphereCast�� attackRange���� �� ��� ������ �ְ� ���� ����Ʈ ����
    {
        int layerMask = 1 << LayerMask.NameToLayer("Monster");
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange,layerMask);
        foreach (Collider hit in hits)
        {
            Monster mHit = hit.transform.GetComponent<Monster>();
            if (mHit)
                mHit.AddHP(-attackPower);
        }

        GameObject effectObejct = Instantiate(ExplosionEffect);
        effectObejct.transform.position = transform.position;

        base.Dead();
    }
}
