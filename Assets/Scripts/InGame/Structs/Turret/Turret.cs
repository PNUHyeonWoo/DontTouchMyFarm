using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret :StructObject
{
    [SerializeField]
    protected float attackPower;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float attackRange;

    protected float attackMaxCoolTime;
    protected float attackCoolTime=0;

    protected Monster target = null;

    private static float findMaxCoolTime = 0.1f;
    private float findCoolTime = 0.1f;
    new void Start()
    {
        base.Start();
        attackMaxCoolTime = 1.0f / attackSpeed;
    }

    void Update()
    {
        FindTarget();
        attackCoolTime += Time.deltaTime;
        if (target && attackCoolTime >= attackMaxCoolTime)
        {
            Attack();
            attackCoolTime = 0;
        }   
    }
    public override StructType GetStructType()
    {
        return StructType.Turret;
    }

    protected override void Dead()
    {
        base.Dead();
    }

    protected abstract void Attack(); // �� �ͷ����� ���� ��� ����
    private void FindTarget() //���� ��Ÿ� ���� ���͸� ã�� Ÿ������ ����
    {
        if(target && Vector2.Distance(
                        new Vector2(target.transform.position.x, target.transform.position.z),
                        new Vector2(transform.position.x, transform.position.z)) > attackRange)
            target = null;


        if(findCoolTime > 0)
            findCoolTime -= Time.deltaTime;

        if (findCoolTime <= 0 && target == null)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Monster");
            Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, layerMask);
            foreach (Collider hit in hits)
            {
                Monster mHit = hit.transform.GetComponent<Monster>();
                if (mHit && 
                    Vector2.Distance(
                        new Vector2(mHit.transform.position.x, mHit.transform.position.z),
                        new Vector2(transform.position.x, transform.position.z)) <= attackRange)
                {
                    target = mHit;
                    return;
                }
            }
            findCoolTime = findMaxCoolTime;
        }
    }
}
