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
        public GameObject Target = null;
        private Health health;
        private Mover mover;

        private Vector3 homePoint;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private Vector3 lastSeen;
        private void Start()
        {
            homePoint = this.transform.position;
            lastSeen = homePoint;
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
            if(TargetInRange())
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
                mover.StartMoveAction(homePoint);
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private bool Suspicious()
        {
            return timeSinceLastSawPlayer < suspiciousTime;
        }

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