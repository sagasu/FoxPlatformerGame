using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public CharacterController2D controller;
    public Animator anim;
    public BoxCollider2D boxCol;
    public CircleCollider2D circleColider;
    public float movementSpeed;
    int score;

    public Text scoreText;
    float horizontalMovementSpeed;
    bool isJumping = false;
    bool isHurt = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Gem")
        {
            score++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Enemy") {
            if (isJumping)
            {
                Destroy(col.gameObject);
                score++;
            }
            else {
                anim.SetBool("isHurt", true);
                boxCol.enabled = false;
                circleColider.enabled = false;
            }
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, isJumping);
    }
}
