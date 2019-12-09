using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{

    bool ZoomedIn = false;
    private Vector3 PosZoomOut;
    public Vector3 PosZoomIn;



    private Vector3 startMarker { get { return ZoomedIn ? PosZoomOut : PosZoomIn; } }
    private Vector3 endMarker { get { return ZoomedIn ? PosZoomIn : PosZoomOut; } }


    // Movement speed in units per second.
    public float lerpSpeed = 1.0F;

    // Time when the movement started.
    private float lerpStart;

    // Total distance between the markers.
    private float lerpLength;

    void Start()
    {
        PosZoomOut = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        if (PosZoomIn == null)
        {
            PosZoomIn = PosZoomOut;
        }

        // Keep a note of the time the movement started.
        lerpStart = Time.time;

        // Calculate the journey length.
        lerpLength = Vector3.Distance(startMarker, endMarker);
    }

    // Update is called once per frame
    void Update()
    {
        zoom();
    }

    public void ZoomIn()
    {
        ZoomedIn = true;
        lerpStart = Time.time;
    }

    public void ZoomOut()
    {
        ZoomedIn = false;
        lerpStart = Time.time;
    }

    private void zoom()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - lerpStart) * lerpSpeed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / lerpLength;

        // Set our position as a fraction of the distance between the markers.
        transform.localPosition = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

    }





}
