using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform BarrelEnd;
    public List<ParticleSystem> GunFireParticles;
    private Animator animator;
    public IBullet bulletPrefab;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire()
    {
        animator.Play("fire");
        IBullet bullet = BulletManager.Instance.Spawn(BarrelEnd.position, transform.rotation, bulletPrefab);
        bullet.Fire();
        GunFireParticles.ForEach(p => p.Play());     
    }
}
