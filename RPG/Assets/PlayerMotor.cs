using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Animation animation;
    private Vector3 rotation = Vector3.zero;
    private bool attack = false;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        animation = GetComponent<Animation>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void Attack()
    {
        //animation.Play("Attack");
        if (!animation.IsPlaying("Attack"))
        {
            attack = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (!animation.IsPlaying("Attack"))
        {
            PerformMovement();
        }

        PerformRotation();

        if (attack || animation.IsPlaying("Attack"))
        {
            PerformAttack();
            attack = false;
        }
            
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            animation.CrossFade("Walk");
        }
    }

    private void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
    }

    private void PerformAttack()
    {
        animation.CrossFade("Attack");

        if (attack)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit target;

            if (Physics.Raycast(transform.position, fwd, out target, 1))
                target.collider.gameObject.SendMessageUpwards("ApplyDamage",50);
        }
        
    }

}
