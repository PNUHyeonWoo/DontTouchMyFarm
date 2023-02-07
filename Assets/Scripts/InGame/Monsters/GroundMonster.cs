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
        if (attackTarget == null) {
            for (int i = 0; i < priority.Length; i++) {
                RaycastHit[] sightHit = Physics.SphereCastAll(transform.position, sight, Vector3.up, 0f, LayerMask.GetMask(priority[i]));

                for (int j = 0; j < sightHit.Length; j++) {
                    agent.CalculatePath(sightHit[j].transform.position, path);
                    agent.SetPath(path);

                    if (Vector3.Distance(sightHit[j].collider.transform.position, agent.pathEndPosition) < attackRange + (sightHit[j].collider.transform.lossyScale.x * rangeOffset)) {
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
