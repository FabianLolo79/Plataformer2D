using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Vector3 direction;

    public float speed;
    public AudioClip sound;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
    }

    // a cada frame o cuadro se comprueba la velocidad y movimiento
    void FixedUpdate()
    {
        rb2D.velocity = direction * speed;
    }

    //dirección de la bala
    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    //destruir la bala 3 segundos luego de ser disparada y ejecutada la animación
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // colisión de la bala ACA ME DA EL ERROR o comenzar acá!!!

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        jhonMovement jhon = other.GetComponent<jhonMovement>();
        GruntScript grunt = other.GetComponent<GruntScript>();
        if (jhon != null)
        {
            //jhon.Hit();
            Debug.Log("Jhon");
        }
        if (grunt != null)
        {
            //grunt.Hit();
            Debug.Log("grunt");
        }
        DestroyBullet();
    }*/
}
