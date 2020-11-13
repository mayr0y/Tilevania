using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody2D myRigidbody;

    [SerializeField] float runSpeed = 5f;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        MovementPlayer();
        FlipSprite();
    }

    private void MovementPlayer() {
        float controlThrow = Input.GetAxisRaw("Horizontal") * runSpeed;
        Vector2 velosityPlayer = new Vector2(controlThrow, myRigidbody.velocity.y);
        myRigidbody.velocity = velosityPlayer;
    }

    private void FlipSprite() {
        bool horizontalSpeedPlayer = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (horizontalSpeedPlayer) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

}
