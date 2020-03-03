using RPG.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour
    {
        CombatTarget Target;
        float AttackRange => 2.0f;

        private void Update()
        {
            if (Target != null)
            {
                var position = GetAttackPosition();
                var mover = this.GetComponent<Mover>();
                mover.MoveTo(position);
                if (IsInAttackRange(position))
                {
                    mover.StopMoving();
                }
            }
        }

        private bool IsInAttackRange(Vector3 position) => Vector3.Distance(this.gameObject.transform.position, position) <= AttackRange;
        private Vector3 GetAttackPosition() => Target.gameObject.transform.position;


        public void Attack(CombatTarget target)
        {
            print($"Take that you {target.name}!");
            Target = target;
        }
    }
}
