using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public bool ZoomedIn;
    public float ZoomFOV;
    public float FOV;

    Camera Camera;


    private float startFOV { get { return ZoomedIn ? FOV : ZoomFOV; } }
    private float endFOV { get { return ZoomedIn ? ZoomFOV : FOV; } }


    // Movement speed in units per second.
    public float LerpSpeed = 1.0F;

    // Time when the movement started.
    private float lerpStart;

    // Total distance between the markers.
    private float lerpLength;


    private void Awake()
    {
        Camera = GetComponent<Camera>();
        if (FOV == 0)
        {
            FOV = Camera.fieldOfView;
        }
        if (ZoomFOV == 0)
        {
            ZoomFOV = FOV;
        }
    }

    // Use this for initialization
    void Start () {        

        // Calculate the journey length.
        lerpLength = Mathf.Abs(startFOV - endFOV);

    }
	
	// Update is called once per frame
	void Update () {
        Zoom();
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

    private void Zoom()
    {

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - lerpStart) * LerpSpeed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / lerpLength;

        // Set our position as a fraction of the distance between the markers.
        Camera.fieldOfView = Mathf.Lerp(startFOV, endFOV, fractionOfJourney);
    }
}
