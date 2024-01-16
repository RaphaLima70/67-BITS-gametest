using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadEnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform targetDeadPosition, playerPos;

    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
    }

    void Update()
    {
        InertiaMovement();
        transform.LookAt(targetDeadPosition);
    }

    void InertiaMovement()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3
        (targetDeadPosition.position.x, targetDeadPosition.position.y + 0.3f, targetDeadPosition.position.z),
         10 * Time.deltaTime);
    }

    public void SetDeadTargetPosition(Transform targetTransform)
    {
        targetDeadPosition = targetTransform;
    }
}
