using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private Vector3 target;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Health health;

        private void Start()
        {
            target = this.transform.position;
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            animator = this.GetComponent<Animator>();
            health = this.GetComponent<Health>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead;
            AnimateCharacter();
        }

        public void StartMoveAction(Vector3 destination)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }

        public void StopMoving()
        {
            navMeshAgent.destination = this.transform.position;
        }

        public void Cancel()
        {
            StopMoving();
        }

        private void AnimateCharacter()
        {
            var globalVelocity = navMeshAgent.velocity;
            var localVelocity = transform.InverseTransformDirection(globalVelocity);
            animator.SetFloat("forwardSpeed", localVelocity.z);
        }

    }
}