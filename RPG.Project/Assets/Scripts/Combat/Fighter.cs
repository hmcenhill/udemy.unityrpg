using Assets.Scripts.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        public CombatTarget Target;
        public float AttackRange => 2.0f;
        public float AttackSpeed => 2.0f;
        private float timeSinceLastAttack = 0;

        private Mover _mover;

        private void Start()
        {
            _mover = this.GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (Target == null) return;

            var position = GetAttackPosition();
            if (IsInAttackRange(position))
            {
                _mover.Cancel();
                AttackBehavior();
            }
            else
            {
                _mover.MoveTo(position);
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack >= AttackSpeed)
            {
                timeSinceLastAttack = 0;
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        private bool IsInAttackRange(Vector3 position) => Vector3.Distance(this.gameObject.transform.position, position) <= AttackRange;
        private Vector3 GetAttackPosition() => Target.gameObject.transform.position;


        public void Attack(CombatTarget target)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            print($"Take that you {target.name}!");
            Target = target;
        }

        public void Cancel()
        {
            CancelAttack();
        }
        public void CancelAttack() => Target = null;

        // Animation Event
        private void Hit()
        {

        }
    }
}
