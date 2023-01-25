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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange, Vector2.up, 0);
        foreach (RaycastHit hit in hits)
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
