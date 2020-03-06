using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private Vector3 _target;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private void Start()
        {
            _target = this.transform.position;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            AnimateCharacter();
        }

        public void StartMoveAction(Vector3 destination)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
        }

        public void StopMoving()
        {
            _navMeshAgent.destination = this.transform.position;
        }

        public void Cancel()
        {
            StopMoving();
        }

        private void AnimateCharacter()
        {
            var globalVelocity = _navMeshAgent.velocity;
            var localVelocity = transform.InverseTransformDirection(globalVelocity);
            _animator.SetFloat("forwardSpeed", localVelocity.z);
        }

    }
}