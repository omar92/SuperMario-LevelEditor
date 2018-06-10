using System.Collections;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public float minJump = 15;
    public float maxjump = 20;
    public float xVelocityFormaxJump = 3;
    public float xVelocityMax = 5;
    public float xForce = 20;
    public float rayCheckDistance;

    public float gravityDown = 5;
    public float onGroundDrag = 5;

    private Rigidbody rb;

    private bool IsGrounded;
    private float originalmass;
    private bool isFacingColider1;
    private bool isFacingColider2;
    Ray ray;
    public TransformVariable camera;

    public GameEvent PLayerDeath;
    bool isDead = false;
    bool isBig = false;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        originalmass = rb.mass;
    }
    private void FixedUpdate()
    {
        // var force = (Mathf.Abs(this.rb.velocity.y) > .1) ? xVelocityMax : xForce;//if on air mov horizontal using difffrent force
        if (!isDead)
        {
            float x = Input.GetAxis("Horizontal");
            if (x > 0)
            {

                ray = new Ray((transform.position + new Vector3(0, .4f)), Vector3.right);
                isFacingColider1 = Physics.Raycast(ray, rayCheckDistance);
                ray = new Ray((transform.position + new Vector3(0, -.4f)), Vector3.right);
                isFacingColider2 = Physics.Raycast(ray, rayCheckDistance);

                if (isFacingColider1 || isFacingColider2)
                {
                    this.rb.velocity = new Vector2(0, this.rb.velocity.y);
                }
                else
                {
                    this.rb.AddForce(Vector2.right * xForce);
                }
            }
            if (x < 0)
            {
                var checkPoint = transform.position + (Vector3.left * .8f);
                var screenPoint = camera.value.GetComponent<Camera>().WorldToViewportPoint(checkPoint);

                ray = new Ray((transform.position + new Vector3(0, .4f)), Vector3.left);
                isFacingColider1 = Physics.Raycast(ray, rayCheckDistance);
                ray = new Ray((transform.position + new Vector3(0, -.4f)), Vector3.left);
                isFacingColider2 = Physics.Raycast(ray, rayCheckDistance);


                if (screenPoint.x < 0 || isFacingColider1 || isFacingColider2)
                {
                    this.rb.velocity = new Vector2(0, this.rb.velocity.y);
                }
                else
                {
                    this.rb.AddForce(Vector2.left * xForce);
                }
            }
            if (Mathf.Abs(this.rb.velocity.x) > xVelocityMax)
            {
                //Debug.Log("limit");
                this.rb.velocity = new Vector2(Mathf.Sign(this.rb.velocity.x) * xVelocityMax, this.rb.velocity.y);
            }
        }
    }
    private void Update()
    {
        if (!isDead)
        {
            float jumpSpeed = 15;
            float vX = this.rb.velocity.x;
            if (Mathf.Abs(vX) > xVelocityFormaxJump)
            {
                jumpSpeed = maxjump;
            }
            else
            {
                float m = (maxjump - minJump) / xVelocityFormaxJump;
                float c = minJump;
                jumpSpeed = m * Mathf.Abs(vX) + c;
            }
            if (Input.GetAxis("Jump") > 0.2)
            {
                Ray ray = new Ray(transform.position, Vector3.down);
                IsGrounded = Physics.Raycast(ray, rayCheckDistance);
                if ((IsGrounded))
                {
                    rb.velocity = new Vector2(vX, jumpSpeed);
                }
            }
            if (Input.GetAxis("Jump") == 0.2 && rb.velocity.y > 0.2)
            {
                rb.velocity = new Vector2(0, rb.velocity.y / 2);
            }

            if (Mathf.Abs(rb.velocity.y) < .2)
            {
                rb.drag = onGroundDrag;
            }
            else
            {
                rb.drag = 0;
            }

            if (transform.position.y < -5)
            {
                Die();

            }
        }

        if (rb.velocity.y < -0.1)
        {
            // rb.mass = originalmass * gravityDown;
            rb.AddForce(Vector3.down * gravityDown, ForceMode.Acceleration);
        }

    }

    public void Die()
    {
        StartCoroutine(PLayerDeathAnim());
    }

    IEnumerator PLayerDeathAnim()
    {
        isDead = true;
        rb.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        transform.position = transform.position + Vector3.back;
        yield return new WaitForSeconds(2);
        rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        PLayerDeath.Raise();
        Destroy(gameObject);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (!isBig)
                Die();
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                rayCheckDistance = .6f;
                isBig = false;
            }
        }
        if (collision.gameObject.tag == "Mashrom")
        {
            transform.localScale = new Vector3(1, 1.5f, 1);
            rayCheckDistance = 0.8f;
            isBig = true;
            Destroy(collision.gameObject);
        }
    }
}
