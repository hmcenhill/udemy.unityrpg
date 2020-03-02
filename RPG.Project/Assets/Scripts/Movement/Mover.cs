using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private Vector3 _target;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    void Start()
    {
        _target = this.transform.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveCharacter();
        AnimateCharacter();
    }

    private void MoveCharacter()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToTarget();
        }
    }

    private void AnimateCharacter()
    {
        var globalVelocity = _navMeshAgent.velocity;
        var localVelocity = transform.InverseTransformDirection(globalVelocity);
        _animator.SetFloat("forwardSpeed", localVelocity.z);
    }

    private void MoveToTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            _target = hit.point;
            _navMeshAgent.destination = _target;
        }
    }
}
