using RPG.Combat;
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
        public GameObject Target = null;
        private Vector3 homePoint;
        private void Start()
        {
            homePoint = this.transform.position;
        }
        private void Update()
        {
            if (Target == null && CheckForEnemies(out var target))
            {
                Target = target;
            }
            if(Target != null)
            {
                Attack();
            }
            if (!TargetInRange())
            {
                this.gameObject.GetComponent<Fighter>().Cancel();
                this.gameObject.GetComponent<Mover>().StartMoveAction(homePoint);
            }
        }

        private bool TargetInRange()
        {
            if (Target == null) return false;
            return Vector3.Distance(this.gameObject.transform.position, Target.transform.position) < chaseDistance;
        }

        private void Attack()
        {
            this.gameObject.GetComponent<Fighter>().Attack(Target);
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
    }
}