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
        RaycastHit[] sightHit = Physics.SphereCastAll(transform.position, sight, Vector3.up, 0f, LayerMask.GetMask("Struct"));

        for (int i = 0; i < sightHit.Length; i++) {
            if (attackTarget == null) {
                attackTarget = sightHit[i].collider.gameObject;
            }

            else if (priority[(int)sightHit[i].collider.GetComponent<StructObject>().GetStructType()] < priority[(int)attackTarget.GetComponent<StructObject>().GetStructType()]) {
                attackTarget = sightHit[i].collider.gameObject;
            }
        }

        Vector3 destination = new Vector3();

        if (attackTarget != null || target != null) {
            if (attackTarget != null) {
                destination = attackTarget.transform.position;
            }
            else {
                destination = target.transform.position;
            }

            destination.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), rotationSpeed * Time.deltaTime);
        }
        else {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime);
        }
    }
}
