using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    PlayerCamera camera;
    PlayerGunManager plyrGunManager;

    private void Awake()
    {
        camera = GetComponentInChildren<PlayerCamera>();
        plyrGunManager = GetComponentInChildren<PlayerGunManager>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ZoomIn()
    {
        camera.ZoomIn();
        plyrGunManager.ZoomIn();
    }

    public void ZoomOut()
    {
        camera.ZoomOut();
        plyrGunManager.ZoomOut();
    }
}
