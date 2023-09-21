using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jhonMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject bulletPrefab;

    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private float horizontal;
    private bool grounded;
    private float lastShoot; // variable para controlar el disparo
    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        horizontal = Input.GetAxisRaw("Horizontal");

        //mirar para el lado que se quiere ir
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        //animator running
        animator.SetBool("running", horizontal != 0.0f);

        //revisar desde ac� video Raycast 18:31hs del video JLPM canal de youtube https://www.youtube.com/watch?v=GbmRt0wydQU&list=PLI2gzz9HM7zdGXbDnIsY-tE35Q6jSoMiz&index=117&t=517s
        //Detectar suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down * 0.1f))
        {
            grounded = true;
        }
        else grounded = false;
        
        //Salto
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }

        //Disparar
        if (Input.GetKey(KeyCode.F) && Time.time > lastShoot + 0.25f)
        {
            Shoot();
            lastShoot = Time.time; // controlar el disparo
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.01f, Quaternion.identity);
        bullet.GetComponent<bulletScript>().setDirection(direction);
    }

    private void Jump()
    {
        rigidBody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate() // utilizado para f�sicas porque se actualizan muy frecuentemente
    {
        rigidBody2D.velocity = new Vector2 (horizontal, rigidBody2D.velocity.y);
    }

    public void Hit()
    {
        health -= 1;
        if (health == 0) Destroy(gameObject);
    }
}

