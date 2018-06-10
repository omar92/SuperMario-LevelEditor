using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float xVelocityMax = 5;
    public float xForce = 20;
    public float rayCheckDistance;
    public GameEvent OnEnemyKilled;
  //  public GameEvent killPLayer;

    Vector2 direction;
    private Rigidbody rb;
    private bool isHit;
    Ray ray;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        direction = Vector2.left;
    }

    private void FixedUpdate()
    {


        this.rb.AddForce(direction * xForce);

        if (Mathf.Abs(this.rb.velocity.x) > xVelocityMax)
        {
            Debug.Log("limit");
            this.rb.velocity = new Vector2(Mathf.Sign(this.rb.velocity.x) * xVelocityMax, this.rb.velocity.y);
        }
    }


    private void Update()
    {
        //check direction change
        ray = new Ray(transform.position, direction);
        RaycastHit info;
        isHit = Physics.Raycast(ray, out info, rayCheckDistance);
        if (isHit && info.collider.tag != "Player")
        {
            direction.x *= -1;
        }

        //chek player killed me 
        ray = new Ray(transform.position, Vector3.up);
        isHit = Physics.Raycast(ray, out info, rayCheckDistance+.2f);
       // if(isHit) Debug.Log(info.collider.tag);
        if (isHit && info.collider.tag== "Player")
        {
            OnEnemyKilled.Raise();
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        killPLayer.Raise();
    //    }
    //}

}
