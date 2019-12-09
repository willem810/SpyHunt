using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IBullet
{

    public float speed;
    public bool flying = true;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        //if (flying) Move();
    }


    public override void Fire()
    {
        //flying = true;
        FireRigidBody();
    }

    private void FireRigidBody()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public override void ResetBullet()
    {
        this.gameObject.SetActive(true);
    }

    public override void OnHit()
    {
        flying = false;
        this.gameObject.SetActive(false);
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
