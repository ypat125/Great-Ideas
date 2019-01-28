using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4f; //Change in inspector to adjust move speed
    Vector3 forward, right; // Keeps track of our relative forward and right vectors
	public Transform respawnPoint;

    public int forceConst = 50;
	public Text display;
	private int lives;
	private double stamina;
	private String powerups;

    private bool canJumpPressed;
    private bool canJump;
    private Rigidbody selfRigidbody;

    void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
        forward.y = 0; // make sure y is 0
        forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector
		lives = PlayerStats.lives;
		stamina = PlayerStats.stamina;
		powerups = PlayerStats.powerups;
		SetCountText();
    }

    void OnCollisionEnter(Collision other)
    {
        canJump = true;
    }

    void OnCollisionExit(Collision other)
    {
        canJump = false;
    }

    void FixedUpdate()
    {
        if (canJumpPressed && canJump)
        {
            canJumpPressed = false;
            selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            canJumpPressed = true;
        }
        if (Input.anyKey) // only execute if a key is being pressed
        {
			Move();
		}
		if (lives == 0) {
			SceneManager.LoadScene(2);
		}
    }
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // setup a direction Vector based on keyboard input. GetAxis returns a value between -1.0 and 1.0. If the A key is pressed, GetAxis(HorizontalKey) will return -1.0. If D is pressed, it will return 1.0
        transform.position += direction/moveSpeed; // move our transform's position right/left
    }
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Kill Zone")) {
			// gameObject.SetActive(false);
			lives = lives - 1;
			SetCountText();
			transform.position = respawnPoint.transform.position;
		}
	}

	void SetCountText() {
		display.text = "";
		display.text += "Lives: " + lives.ToString() + '\n';
		display.text += "Stamina: " + stamina.ToString() + '\n';
		// display.text += "Abilities: " + powerups;
	}
}
