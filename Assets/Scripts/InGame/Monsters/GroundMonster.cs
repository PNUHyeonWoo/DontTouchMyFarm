using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundMonster : Monster
{
    protected NavMeshAgent agent;
    protected NavMeshPath path;

    [SerializeField]
    protected int attackPriority;
    [SerializeField]
    protected int movePriority;
    [SerializeField]
    protected float rangeOffset;

    protected override void Start() {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    protected override void AttackCheck() {
        if (attackTarget != null) {
            if (Vector3.Distance(transform.position, attackTarget.transform.position) <= attackRange + (attackTarget.transform.lossyScale.x * rangeOffset)) {
                AttackStart();
            }
            else {
                AttackEnd();
            }
        }
    }

    protected override void Move() {
        RaycastHit[] sightHit = Physics.SphereCastAll(transform.position, sight, Vector3.up, 0f, LayerMask.GetMask("Struct"));

        for (int i = 0; i < sightHit.Length; i++) {
            agent.CalculatePath(sightHit[i].transform.position, path);
            agent.SetPath(path);

            if (Vector3.Distance(sightHit[i].collider.transform.position, agent.pathEndPosition) < attackRange + (sightHit[i].collider.transform.lossyScale.x * rangeOffset)) {
                if (attackTarget == null) {
                    attackTarget = sightHit[i].collider.gameObject;
                }
    
                else if (priority[(int)sightHit[i].collider.GetComponent<StructObject>().GetStructType()] < priority[(int)attackTarget.GetComponent<StructObject>().GetStructType()]) {
                    attackTarget = sightHit[i].collider.gameObject;
                }
            }
        }

        if (attackTarget == null && target != null) {
            agent.CalculatePath(target.transform.position, path);
            agent.SetPath(path);
        }
        else if (attackTarget != null) {
            agent.CalculatePath(attackTarget.transform.position, path);
            agent.SetPath(path);
            Debug.Log(path.status);
        }
    }

    protected override void AttackStart() {
        base.AttackStart();
        agent.isStopped = true;
        agent.avoidancePriority = attackPriority;
        agent.velocity = Vector3.zero;
    }

    protected override void AttackEnd() {
        base.AttackEnd();
        agent.isStopped = false;
        agent.avoidancePriority = movePriority;
    }
}
