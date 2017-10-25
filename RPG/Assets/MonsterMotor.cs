using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMotor : MonoBehaviour {

    private float health = 120f;

    private bool damaged = false;
    private bool dead = false;

    private Animation animation;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged || animation.IsPlaying("Damage"))
        {
            PerformDamage();
            damaged = false;
        }

        if (dead || animation.IsPlaying("Dead"))
        {
            PerformDeath();
            dead = false;
        }
        else
        {
            if (health < 0)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.name == "Ground")
        {
            rb.isKinematic = true;
        }
    }

    private void PerformDamage()
    {
        animation.CrossFade("Damage");
    }

    private void PerformDeath()
    {
        animation.CrossFade("Dead");
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        damaged = true;

        Debug.Log(string.Format("current health is {0}", health));

        if (health < 0f)
        {
            dead = true;
        }
    }
}
