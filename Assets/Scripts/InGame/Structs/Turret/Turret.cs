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

    protected abstract void Attack();
    private void FindTarget() 
    {
        if (target == null || Vector2.Distance(
                        new Vector2(target.transform.position.x, target.transform.position.z),
                        new Vector2(transform.position.x, transform.position.z)) > attackRange)
        {
            target = null;
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange,Vector2.up,0);
            foreach (RaycastHit hit in hits)
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
        }
    }
}
