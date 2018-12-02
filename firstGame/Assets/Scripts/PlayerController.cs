using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CharacterController2D controller;
    public Animator anim;

    public float movementSpeed;

    float horizontalMovementSpeed;
    bool isJumping = false;
    bool isHurt = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;

        anim.SetFloat("speed", Mathf.Abs(horizontalMovementSpeed));

        if (Input.GetButtonDown("Jump")) {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
	}

    public void OnLanding() {
        isJumping = false;
        anim.SetBool("isJumping", false);
    }

    void FixedUpdate() {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }
}
