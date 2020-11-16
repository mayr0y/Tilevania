using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 30f;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider2D;

    bool isAlive = true;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    private void Update() {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 velosityPlayer = new Vector2(controlThrow, myRigidbody.velocity.y);
        myRigidbody.velocity = velosityPlayer;

        bool horizontalSpeedPlayer = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (horizontalSpeedPlayer) {
            myAnimator.SetBool("Running", true);
        }
        else {
            myAnimator.SetBool("Running", false);
        }
    }

    private void Jump() {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelosity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            myRigidbody.velocity += jumpVelosity;
        }
    }

    private void FlipSprite() {
        bool horizontalSpeedPlayer = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (horizontalSpeedPlayer) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

}
