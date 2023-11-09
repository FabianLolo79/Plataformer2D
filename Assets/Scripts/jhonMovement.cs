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
                
        //Salto
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
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
            LoadSceneWin();
        }
        if (collision.gameObject.CompareTag("colisionCaida"))
        {
            LoadSceneLose();
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hit();
        }
    }

    //Función escena Winner
    void LoadSceneWin()
    {
        int activeSceceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceceneIndex + 2);
    }

    //Función escena GameOver
    void LoadSceneLose()
    {
        int activeSceceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceceneIndex + 1);
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
        rigidBody2D.AddForce(Vector2.up * jumpForce); 
    }

    // utilizado para físicas porque se actualizan muy frecuentemente
    private void FixedUpdate()
    {
        rigidBody2D.velocity = new Vector2(horizontal, rigidBody2D.velocity.y);
    }

    public void Hit()
    {
        health -= 1;
        if (health == 0) gameObject.SetActive(false); 
    }
}

