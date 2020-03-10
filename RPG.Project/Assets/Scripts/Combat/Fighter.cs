using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        public GameObject Target;
        public float AttackRange => 2.0f;
        public float AttackSpeed => 2.0f;
        public float AttackDamage => 5.0f;
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
            this.transform.LookAt(Target.transform);
            if (Target.GetComponent<Health>().IsDead)
            {
                Cancel();
                return;
            }
            if (timeSinceLastAttack >= AttackSpeed)
            {
                timeSinceLastAttack = 0;
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack"); // Triggers Hit() event
            }
        }

        // Animation Event
        private void Hit()
        {
            if (Target == null) return;
            Target.GetComponent<Health>().TakeDamage(AttackDamage);
        }

        private bool IsInAttackRange(Vector3 position) => Vector3.Distance(this.gameObject.transform.position, position) <= AttackRange;
        private Vector3 GetAttackPosition() => Target.transform.position;


        public void Attack(GameObject target)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            Target = target;
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            CancelAttack();
        }
        public void CancelAttack() => Target = null;

        public bool CanAttack() => Target != null && !Target.GetComponent<Health>().IsDead;
    }
}
