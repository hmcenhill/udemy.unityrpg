using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform Target;

    // Update is called once per frame
    void Update()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = Target.position;

        
    }
}
