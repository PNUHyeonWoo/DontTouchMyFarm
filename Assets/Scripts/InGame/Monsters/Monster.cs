using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public static int totalAmount = 0;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackAnimationDelay;
    private float attackDelay;
    private float checkSpeed = 0.5f;
    private float checkDelay;
    
    [SerializeField]
    protected GameObject attackTarget = null;
    [SerializeField]
    protected GameObject attackEffect;
    [SerializeField]
    protected Animator anim;

    protected bool isAttack = false;
    protected bool isAttackAnimation = false;
    [SerializeField]
    protected string[] priority;
    [SerializeField]
    protected float sight;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float rangeOffset;
    [SerializeField]
    protected float maxHP;
    protected float HP;
    [SerializeField]
    protected float attackDamage;
    [SerializeField]
    protected float rotationSpeed;

    protected virtual void Start() {
        HP = maxHP;
        anim.SetBool("isMoving", true);
    }

    private void Update() {
        checkDelay += Time.deltaTime;
        if (checkDelay > checkSpeed) {
            checkDelay = 0;
            AttackCheck();
        }

        if (isAttack) {
            Attack();
        }
        else {
            Move();
        }
    }

    protected abstract void AttackCheck();

    protected abstract void Move();

    protected virtual void Attack() {
        attackDelay += Time.deltaTime;

        if (!isAttackAnimation && attackDelay > (attackSpeed - attackAnimationDelay)) {
            if (attackTarget != null) anim.SetTrigger("doAttack");
            isAttackAnimation = true;
        }

        if (attackDelay > attackSpeed) {
            attackDelay = 0;
            isAttackAnimation = false;

            if (attackTarget == null) {
                AttackEnd();
                anim.SetBool("isMoving", true);
            }
            else {
                attackTarget.GetComponent<StructObject>().AddHP(-attackDamage);
                GameObject attackObejct = Instantiate(attackEffect);
                attackObejct.transform.position = attackTarget.transform.position;
            }
        }
    }

    protected virtual void AttackEnd() {
        isAttack = false;
    }

    protected virtual void AttackStart() {
        isAttack = true;
        anim.SetBool("isMoving", false);
    }

    public float AddHP(float value) {
        HP += value;
        if (HP <= 0) {
            Dead();
        }
        if(HP > maxHP) {
            HP = maxHP;
        }
        return HP;
    }

    public virtual void Dead() {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        totalAmount--;
    }
}
