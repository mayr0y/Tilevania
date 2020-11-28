using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 30f;
    [SerializeField] float climpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;

    bool isAlive = true;

    float gravityScaleAtStart;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        
    }

    private void Update() {
        if (!isAlive) { return; }
        Death();
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        StartCoroutine(TimeToLoad());
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
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelosity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            myRigidbody.velocity += jumpVelosity;
        }
    }

    private void ClimbLadder() {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical") * climpSpeed;
        Vector2 climbVelosity = new Vector2(myRigidbody.velocity.x, controlThrow);
        myRigidbody.velocity = climbVelosity;
        myRigidbody.gravityScale = 0f;

        bool verticalSpeedPlayer = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", verticalSpeedPlayer);
    }

    private void Death() {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) {
            myRigidbody.velocity = deathKick;
            myAnimator.SetTrigger("Die");
            isAlive = false;
        }
    }

    IEnumerator TimeToLoad() {
        if(!isAlive) {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Level 1");
        }
    }

    private void FlipSprite() {
        bool horizontalSpeedPlayer = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (horizontalSpeedPlayer) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

}
