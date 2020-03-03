using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;
    private Vector3 targetPosition;
    [SerializeField] 
    //[Range(0,1f)]
    private float moveSpeed;
    private int wayPointIndex;

    void Start()
    {
        targetPosition = wayPoints[0].position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < .25f)
        {
            if (wayPointIndex >= wayPoints.Length - 1)
            {
                wayPointIndex = 0;
            }
            else
            {
                wayPointIndex++;
            }
            targetPosition = wayPoints[wayPointIndex].position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
