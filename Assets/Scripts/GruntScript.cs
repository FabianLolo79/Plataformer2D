using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject jhon;

    private float lastShoot;
    private int health = 2;
    private void Update()
    {
        if (jhon == null) return;

        // para que el enemigo mire siempre al personaje
        Vector3 direction = jhon.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        // distancia al personaje con resultado positivo
        float distance = Mathf.Abs(jhon.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > lastShoot + 0.75f)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        //Debug.Log("ShootEnemy");
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.01f, Quaternion.identity);
        bullet.GetComponent<bulletScript>().setDirection(direction);
    }

    public void Hit()
    {
        health -= 1;
        if (health == 0) Destroy(gameObject);
    }
}
