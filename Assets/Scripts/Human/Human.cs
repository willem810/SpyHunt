using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public enum Role
{
    Innocent,
    Spy
}

public class Human : MonoBehaviour
{
    // General Properties
    public Role role;
    public bool isDead = false;
    public bool disguised = false;
    public float maxSecWandering = 1;
    public float maxSecondsStanding = 1;


    private bool shouldWander { get { return Time.time < wanderUntil; } }
    private float wanderUntil;

    // Materials
    public Material NormalMaterial;
    public Material SpyMaterial;
    public Material InnocentMaterial;
    private Material UnDisguisedMaterial
    {
        get
        {
            if (role == Role.Spy) return SpyMaterial;
            else if (role == Role.Innocent) return InnocentMaterial;
            else return NormalMaterial;
        }
    }

    // Move Properties
    public float speed = 5;
    public float turnSpeed = 1;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;

    private float heading;
    private Vector3 targetRotation;

    // Events
    public event HumanEvent.DieEvent OnDie;
    public event HumanEvent.FallEvent OnFall;

    // Components
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(NewHeadingRoutine());
    }

    void Update()
    {
        if (!isDead)
        {
            Wander();
        }
    }
    private void Wander()
    {
        Vector3 newEuler = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * turnSpeed);
        float margin = 0.001f;

        if (!(transform.eulerAngles.x < margin && transform.eulerAngles.x > -margin) ||
            !(transform.eulerAngles.z < margin && transform.eulerAngles.z > -margin) ||
            !(newEuler.x < margin && newEuler.x > -margin) ||
            !(newEuler.z < margin && newEuler.z > -margin))
        {
            //EditorApplication.isPaused = true;            
            //Debug.LogFormat("{0} - euler: {1} | new Euler: {2} | targetRotation: {3} | forward: {4}", this.gameObject.name, transform.eulerAngles, newEuler, targetRotation, transform.forward);

            // Unit Fall, Stop moving
            Fall();
            return;
        }


        transform.eulerAngles = newEuler;
        if (shouldWander)
        {
            //Keep on walking
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void Fall()
    {
        Vector3 newEuler = Vector3.Lerp(transform.eulerAngles, targetRotation, Time.deltaTime);
        transform.eulerAngles = newEuler;
        if (OnFall != null) OnFall(this);
    }

    private void Die()
    {
        SetRagdol(true);
        isDead = true;
        SetDisguised(false);
        if (OnDie != null) OnDie(this);
    }

    public void SetRole(Role r)
    {
        role = r;
    }

    private void SetRagdol(bool isRagdol)
    {
        rb.freezeRotation = !isRagdol;
        rb.useGravity = isRagdol;
    }

    public void SetDisguised(bool disguised)
    {
        this.disguised = disguised;
        //SetMaterial(this.disguised);
        if (disguised) this.GetComponentInChildren<MeshRenderer>().material = NormalMaterial;
        else this.GetComponentInChildren<MeshRenderer>().material = UnDisguisedMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Human Collision hit: " + collision.transform.root.tag);
        if (collision.transform.root.tag == "Bullet") Die();
        //// Check For Bullet Hit
        //if (collision.gameObject.tag == "Bullet")
        //{
        //    Die();
        //    return;
        //}

        // Change Moving direction
        var normal = collision.contacts[0].normal;
        var newDirection = Vector3.Reflect(transform.forward, normal);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, newDirection);
        heading = transform.eulerAngles.y;
        NewHeading();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Human Trigger hit: " + other.tag);
        if (other.transform.root.tag == "Bullet") Die();
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    private void NewHeading()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    private IEnumerator NewHeadingRoutine()
    {
        while (true)
        {
            NewHeading();
            float wanderTime = Random.Range(0, maxSecWandering);
            wanderUntil = Time.time + wanderTime;

            float waitTime = Random.Range(0, maxSecondsStanding);
            yield return new WaitForSeconds(wanderTime + waitTime);
        }
    }

}
