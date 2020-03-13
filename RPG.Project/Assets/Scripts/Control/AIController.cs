using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 3f;
        [SerializeField] float patrolDwellTime = 2f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float patrolCloseEnough = 1f;

        public GameObject Target = null;
        private Health health;
        private Mover mover;

        private Vector3 currentPatrolPoint;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeDwelling = 0;
        private Vector3 lastSeen;
        private int currentPatrolWaypointIndex = 0;

        private void Start()
        {
            currentPatrolPoint = this.transform.position;
            lastSeen = currentPatrolPoint;
            health = this.gameObject.GetComponent<Health>();
            mover = this.gameObject.GetComponent<Mover>();
        }
        private void Update()
        {
            if (health.IsDead) return;
            if (Target == null && CheckForEnemies(out var target))
            {
                Target = target;
            }
            if (TargetInRange())
            {
                Attack();
                timeSinceLastSawPlayer = 0;
                lastSeen = Target.transform.position;
            }
            else if (Suspicious())
            {
                this.gameObject.GetComponent<Fighter>().Cancel();
                mover.StartMoveAction(lastSeen);
            }
            else
            {
                PatrolBehavior();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeDwelling += Time.deltaTime;
                    if (timeDwelling > patrolDwellTime)
                    {
                        CycleWaypoint();
                        timeDwelling = 0;
                    }
                }
                currentPatrolPoint = GetCurrentWayPoint();
            }
            mover.StartMoveAction(currentPatrolPoint);
        }

        private bool AtWayPoint() => Vector3.Distance(this.transform.position, GetCurrentWayPoint()) < patrolCloseEnough;

        private void CycleWaypoint()
        {
            currentPatrolWaypointIndex = (currentPatrolWaypointIndex + 1) % patrolPath.transform.childCount;
        }

        private Vector3 GetCurrentWayPoint() => patrolPath.transform.GetChild(currentPatrolWaypointIndex).position;

        private bool Suspicious() => timeSinceLastSawPlayer < suspiciousTime;

        private bool TargetInRange()
        {
            if (Target == null) return false;
            return Vector3.Distance(this.gameObject.transform.position, Target.transform.position) < chaseDistance;
        }

        private void Attack()
        {
            this.gameObject.GetComponent<Fighter>().Attack(Target);
            if (Target.GetComponent<Health>().IsDead)
            {
                Target = null;
            }
        }

        private bool CheckForEnemies(out GameObject target)
        {
            var player = GameObject.FindWithTag("Player");
            if (!player.GetComponent<Health>().IsDead && Vector3.Distance(this.gameObject.transform.position, player.transform.position) < chaseDistance)
            {
                target = player;
                print($"{this.name} says: \"Die, {target.name}!\"");
                return true;
            }
            target = null;
            return false;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.gameObject.transform.position, chaseDistance);
        }
    }
}