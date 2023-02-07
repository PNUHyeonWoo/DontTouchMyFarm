using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : Monster
{
 [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;

    private Rigidbody rigid;

    protected override void Start() {
        base.Start();
        rigid = GetComponent<Rigidbody>();
    }

    protected override void AttackCheck() {
        if (attackTarget != null) {
            if (Vector3.Distance(transform.position, new Vector3(attackTarget.transform.position.x, transform.position.y, attackTarget.transform.position.z)) <= attackRange) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTarget.transform.position - transform.position), rotationSpeed * Time.deltaTime);
                AttackStart();
            }
            else {
                AttackEnd();
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
}
