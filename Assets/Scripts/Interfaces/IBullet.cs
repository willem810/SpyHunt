using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{

    public abstract class IBullet : MonoBehaviour
    {

        public abstract void Fire();
        public abstract void OnHit();
        public abstract void ResetBullet();

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Bullet Collision hit");

            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.red);
            }

            OnHit();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet Trigger hit");
            OnHit();
        }
    }
}
