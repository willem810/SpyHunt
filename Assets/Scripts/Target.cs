using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : IHittable
{

    public int pointCircles;
    MeshCollider col;

    List<Vector3> hits = new List<Vector3>();
    Vector3 lastHit { get { return hits.Last(); } }

    private float radius;

    private void Awake()
    {
        col = GetComponentInChildren<MeshCollider>();
        radius = getRadius();

    }


    private float getRadius()
    {
        radius = col.gameObject.transform.localScale.x / 2;
        return radius;
    }


    protected override void OnBulletHit(Vector3 position)
    {
        hits.Add(position);
        float distPerCircle = radius / pointCircles;
        float dist = Vector3.Distance(transform.position, position);

        float points = pointCircles - Mathf.Ceil(dist / distPerCircle) + 1;

        Debug.LogFormat("Points: {0} - radius: {1}, dist {2}, distPerCircle {3}", points, radius, dist, distPerCircle);





    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        hits.ForEach(h => Gizmos.DrawSphere(h, 0.02f));
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
