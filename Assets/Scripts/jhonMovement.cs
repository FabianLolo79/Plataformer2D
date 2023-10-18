using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jhonMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject bulletPrefab;

    public Rigidbody2D rigidBody2D;
    public Animator animator;
    public float horizontal;
    public bool grounded;
    public float lastShoot; // variable para controlar el disparo
    public int health = 5;

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
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //animator running
        animator.SetBool("running", horizontal != 0.0f);

        //revisar desde acá video Raycast 18:31hs del video JLPM canal de youtube https://www.youtube.com/watch?v=GbmRt0wydQU&list=PLI2gzz9HM7zdGXbDnIsY-tE35Q6jSoMiz&index=117&t=517s
        //Detectar suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down * 0.1f))
        {
            grounded = true;
        }
        else grounded = false;

        //Salto
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) 
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int activeSceceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceceneIndex + 2);
    }

    //Función disparo
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.01f, Quaternion.identity);
        bullet.GetComponent<bulletScript>().setDirection(direction);
    }

    //Función salto
    private void Jump()
    {
        rigidBody2D.AddForce(Vector2.up * jumpForce); //ForceMode2D.Impulse
    }

    // utilizado para físicas porque se actualizan muy frecuentemente
    private void FixedUpdate()
    {
        rigidBody2D.velocity = new Vector2(horizontal, rigidBody2D.velocity.y);
    }

    public void Hit()
    {
        health -= 1;
        if (health == 0) Destroy(gameObject);
    }
}

