using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health < 0) health = 0;
            print(health);
        }
    }
}
