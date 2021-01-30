using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{
    public float minVertical = -45.0f; // Limits the upwards mouselook
    public float maxVertical = 45.0f;  // Limits the Downwards mouselook

    public float sensitivityHorizontal = 10.0f;
    public float sensitivityVertical = 10.0f;

    public float _rotationX = 0;

    public Camera playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main; //TODO: set this up with a tag for player camera so we can have a specific camera for the player.

        //Set Cursor to not be visible
        

    }

    // Update is called once per frame
    void Update()
    {
        /* 
        We want to seperate out the rotation of the PLAYER & the CAMERA
        The player should be turning their body when looking left/right
        when it comes to Up/Down look only the camera should be rotating (not the player mesh)        
        */

        // Controls the Left/Right turning of the player
        this.gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0);


        //Controls the Up/Down rotation of the camera
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;
        _rotationX = Mathf.Clamp(_rotationX, minVertical, maxVertical); // sets limits on the up/down rotation of the camera        

        playerCamera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }
}
