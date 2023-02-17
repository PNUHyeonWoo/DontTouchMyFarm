using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMonster : Monster
{
    protected NavMeshAgent agent;
    protected NavMeshPath path;

    [SerializeField]
    private GameObject childrenObject;

    protected override void Start() {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
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
            else if (isAttack) {
                AttackEnd();
            }
            else if (agent.velocity == Vector3.zero) {
                attackTarget = null;
            }
        }
    }

    protected override void Move() {
        childrenObject.transform.localRotation = Quaternion.Lerp(childrenObject.transform.localRotation, Quaternion.identity, rotationSpeed * Time.deltaTime);

        if (attackTarget == null) {
            for (int i = 0; i < priority.Length; i++) {
                RaycastHit[] sightHit = Physics.SphereCastAll(transform.position, sight, Vector3.up, 0f, LayerMask.GetMask(priority[i]));

                for (int j = 0; j < sightHit.Length; j++) {
                    agent.CalculatePath(sightHit[j].transform.position, path);
                    agent.SetPath(path);

                    float range = attackRange + (sightHit[j].collider.GetComponent<StructObject>().Size * rangeOffset);

                    if (Mathf.Abs(sightHit[j].collider.transform.position.x - agent.pathEndPosition.x) < range
                        && Mathf.Abs(sightHit[j].collider.transform.position.z - agent.pathEndPosition.z) < range) 
                    {
                        attackTarget = sightHit[j].collider.gameObject;
                        break;
                    }
                }

                if (attackTarget != null) {
                    break;
                }
            }

            if (attackTarget == null) {
                agent.CalculatePath(new Vector3(0, transform.position.y, 0), path);
                agent.SetPath(path);
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if (attackTarget != null) {
            Vector3 rotationTarget = attackTarget.transform.position - transform.position;
            rotationTarget.y = 0;
            childrenObject.transform.rotation = Quaternion.Lerp(childrenObject.transform.rotation, Quaternion.LookRotation(rotationTarget), rotationSpeed * Time.deltaTime);
        }
    }

    protected override void AttackStart() {
        base.AttackStart();
        agent.isStopped = true;
        agent.avoidancePriority = 1;
        agent.velocity = Vector3.zero;
    }

    protected override void AttackEnd() {
        base.AttackEnd();
        agent.isStopped = false;
        agent.avoidancePriority = 50;
    }
}
