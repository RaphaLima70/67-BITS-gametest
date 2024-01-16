using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick floatingJoystick;
    private Vector3 direction;
    private Rigidbody rigid;
    private Animator anim;
    [SerializeField]
    private GameObject hitBox;
    private bool canWalk;
    private float h, v;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private BagController bag;
    private int colorCounter;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Color[] colors;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        canWalk = true;
        SetNewColor();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }
        else if (!Application.isEditor)
        {
            h = floatingJoystick.Horizontal;
            v = floatingJoystick.Vertical;
        }

        if (h != 0 || v != 0)
        {
            SetAnimation("state", 1);
        }
        else
        {
            SetAnimation("state", 0);
        }
    }

    private void FixedUpdate()
    {
        if (h != 0 || v != 0)
        {
            Rotate();
        }
        Movement(h, v);
    }

    private void Movement(float h, float v)
    {
        direction.Set(h, 0, v);

        if (canWalk)
        {
            direction = direction.normalized * velocity * Time.deltaTime; // move o player

            rigid.velocity = direction;
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
    }

    private void Rotate()
    {
        if (canWalk)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(
            h, v) * 180 / Mathf.PI, 0);
        }
    }
    public void Attack()
    {
        SetAnimation("punch");
        canWalk = false;
    }
    private void EnableWalk()
    {
        hitBox.SetActive(false);
        canWalk = true;
    }

    public BagController getBag()
    {
        return bag;
    }

    private void SetAnimation(string name, int id = -1)
    {
        if (id == -1)
        {
            anim.SetTrigger(name);
        }
        else
        {
            anim.SetInteger(name, id);
        }
    }

    public void SetNewColor()
    {
        material.color = colors[colorCounter];
        colorCounter++;
    }
}

