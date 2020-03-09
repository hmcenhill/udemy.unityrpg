using UnityEngine;

namespace RPG.Combat
{
    class Health : MonoBehaviour
    {
        [SerializeField] float health = 15f;
        public bool IsDead { get; set; } = false;
        public void TakeDamage(float damage)
        {
            if (!IsDead)
            {
                health -= damage;
                if (health <= 0)
                {
                    health = 0;
                    IsDead = true;
                    GetComponent<Animator>().SetTrigger("die");
                }
            }
            print(health);
        }
    }
}
