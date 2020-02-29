using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] int Speed = 10;

    // Update is called once per frame
    void Update()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = Speed;
        navMeshAgent.destination = Target.position;

        
    }
}
