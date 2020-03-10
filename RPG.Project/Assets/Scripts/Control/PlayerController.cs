using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nossing!");
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetRay());
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<CombatTarget>(out var target))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        this.GetComponent<Fighter>().Attack(target.gameObject);
                        if (!this.GetComponent<Fighter>().CanAttack()) continue;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetRay(), out var hit))
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
