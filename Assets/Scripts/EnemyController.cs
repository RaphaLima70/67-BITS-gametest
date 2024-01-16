using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CapsuleCollider defaultCol;
    private Rigidbody rigid;
    private Animator animator;
    [SerializeField]
    private Transform playerAim;
    private PlayerController player;
    [SerializeField]
    private GameObject deadPrefab;
    private bool isDead;
    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;

    void Start()
    {
        isDead = false;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        defaultCol = GetComponent<CapsuleCollider>();
        GetRagdoll();
        DisableRAgdoll();
    }

    private void Update()
    {
        if (!isDead)
        {
            Vector3 forceDirection = player.transform.position - playerAim.position;
            Quaternion toRotation = Quaternion.LookRotation(forceDirection, Vector3.up);
            playerAim.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 200 * Time.deltaTime);
        }
    }

    private void EnableRagdoll()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        defaultCol.enabled = false;
        rigid.isKinematic = true;
        animator.enabled = false;
    }

    private void DisableRAgdoll()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        defaultCol.enabled = true;
        rigid.isKinematic = false;
        animator.enabled = true;
    }

    private void GetRagdoll()
    {
        ragdollColliders = rigid.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = rigid.GetComponentsInChildren<Rigidbody>();
    }

    private void takingHit()
    {
        EnableRagdoll();

        Vector3 forceDirection = transform.position - player.transform.position;
        forceDirection.y = 1;
        forceDirection.Normalize();

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.AddForce(forceDirection * 30, ForceMode.Impulse);
        }
    }



    private IEnumerator goToBag(BagController bag)
    {
        yield return new WaitForSeconds(3);
        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        if (bag.CanReceiveEnemy())
        {
            bag.CreateDeadEnemy(deadPrefab);
        }
        Destroy(gameObject);
        isDead = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hitBox"))
        {
            takingHit();
            IEnumerator coroutine = goToBag(player.getBag());
            StartCoroutine(coroutine);

        }
    }
}
