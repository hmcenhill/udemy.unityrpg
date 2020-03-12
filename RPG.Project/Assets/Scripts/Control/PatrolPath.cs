using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.25f;
        private void OnDrawGizmos()
        {
            for (var i = 0; i < this.transform.childCount; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint((i + 1) % this.transform.childCount));
            }
        }

        private Vector3 GetWaypoint(int waypointIndex)
        {
            return transform.GetChild(waypointIndex).position;
        }
    }
}