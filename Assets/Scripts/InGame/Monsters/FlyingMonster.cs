using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : Monster
{
    [SerializeField]
    private float moveSpeed;
    
    private Rigidbody rigid;

    protected override void Start() {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        Vector3 ranPos = Random.insideUnitSphere * 5;
        ranPos.y = transform.position.y;
        transform.position += ranPos;
    }

    protected override void AttackCheck() {
        if (attackTarget != null) {
            float range = attackRange + (attackTarget.GetComponent<StructObject>().Size * rangeOffset);
            if (Mathf.Abs(transform.position.x - attackTarget.transform.position.x) < range 
                && Mathf.Abs(transform.position.z - attackTarget.transform.position.z) < range) 
            {
                if (!isAttack) {
                    AttackStart();
                }
            }
        }
    }

    protected override void Move()
    {
        for (int i = 0; i < priority.Length; i++) {
            RaycastHit[] sightHit = Physics.SphereCastAll(transform.position, sight, Vector3.up, 0f, LayerMask.GetMask(priority[i]));

            if (sightHit.Length != 0) {
                attackTarget = sightHit[0].collider.gameObject;
                break;
            }
        }

        Vector3 destination = new Vector3();

        if (attackTarget != null) {
            destination = attackTarget.transform.position;
        }
        else {
            destination = Vector3.zero;
        }

        destination.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), rotationSpeed * Time.deltaTime);
    }

    protected override void Attack()
    {
        base.Attack();
        
        if (attackTarget != null) {
            Vector3 destination = attackTarget.transform.position - transform.position;
            destination.y = -1f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination), rotationSpeed * Time.deltaTime);
        }
    }
}
