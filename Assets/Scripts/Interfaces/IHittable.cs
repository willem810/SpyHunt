using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public abstract class IHittable : MonoBehaviour
    {

        private void OnCollisionEnter(Collision collision)
        {

            Vector3 pos = collision.contacts[0].point;
            Hit(pos, collision.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector3 pos = collision.contacts[0].point;
            Hit(pos, collision.gameObject);

        }

        private void Hit(Vector3 pos, GameObject obj)
        {

            if (obj.tag == "Bullet")
            {
                OnBulletHit(pos);
            }
        }

        protected abstract void OnBulletHit(Vector3 position);

    }
}
