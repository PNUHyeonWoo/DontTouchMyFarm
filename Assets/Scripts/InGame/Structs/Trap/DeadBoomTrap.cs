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

    /* 폭팔 테스트용
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Dead();
        }
    }
    */

    protected override void Dead() // SphereCast로 attackRange내의 적 모두 데미지 주고 폭팔 이펙트 생성
    {
        int layerMask = 1 << LayerMask.NameToLayer("Monster");
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange, Vector2.up, 0,layerMask);
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
