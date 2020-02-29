using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform Target;

    void LateUpdate()
    {
        this.transform.position = Target.position;
    }
}
