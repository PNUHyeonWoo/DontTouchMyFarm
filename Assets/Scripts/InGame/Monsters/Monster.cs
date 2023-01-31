using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private bool isAttack = false;
    private float initAttackDelay = 100;
    private float attackDelay;
    
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected GameObject attackTarget = null;

    [SerializeField]
    protected int[] priority;
    [SerializeField]
    protected float sight;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float maxHP;
    protected float HP;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float attackDamage;

    /*
    private NightTimer timer;

    public NightTimer Timer {
        set { timer = value; }
    }
    */

    protected virtual void Start() {
        HP = maxHP;
        attackDelay = initAttackDelay;
    }

    private void Update() {
        /*
        if(timer.RemainTime <= 0) {
            Dead();
        }
        */

        AttackCheck();

        if (isAttack) {
            Attack();
        }
        else {
            Move();
        }
    }

    protected abstract void AttackCheck();

    protected abstract void Move();

    protected void Attack() {
        attackDelay += Time.deltaTime;

        if (attackDelay > attackSpeed) {
            attackDelay = 0;

            if (attackTarget == null) {
                AttackEnd();
            }
            else {
                attackTarget.GetComponent<StructObject>().AddHP(-attackDamage);
            }
        }
    }

    protected virtual void AttackEnd() {
        isAttack = false;
        attackDelay = initAttackDelay;
    }

    protected virtual void AttackStart() {
        isAttack = true;
    }

    public float AddHP(float value) {
        HP += value;
        if (HP <= 0) {
            Dead();
        }
        return HP;
    }

    protected virtual void Dead() { 
        Destroy(gameObject);
    }
}
