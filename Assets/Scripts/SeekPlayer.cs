using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : MonoBehaviour {
    public Transform projectile; // The transform of the projectile to follow.
    public Transform projectile2;


    public Transform farLeft;           // The transform representing the left bound of the camera's position.
    public Transform farRight;         // The transform representing the right bound of the camera's position.
    public Transform farUp;
    public Transform farDown;
    public float velo;

    void Update()
    {
        velo = projectile.GetComponent<Rigidbody2D>().velocity.sqrMagnitude;
        if (projectile.GetComponent<SpringJoint2D>() == null)
        {
            if (velo < 0.05)
                projectile = projectile2;

            // Store the position of the camera.
            Vector3 newPosition = transform.position;

            // Set the x value of the stored position to that of the bird.
            newPosition.x = projectile.position.x;
            //newPosition.y = projectile.position.y;

            // Clamp the x value of the stored position between the left and right bounds.
            newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x);
            //newPosition.y = Mathf.Clamp (newPosition.y, farDown.position.y, farUp.position.y);

            // Set the camera's position to this stored position.
            transform.position = newPosition;
        }
    }
}
