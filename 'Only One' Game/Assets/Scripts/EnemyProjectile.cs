using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public int damage;
    public float speed;
    public float lifetime;
    public float distance;

    GameObject gunShot;

    void Start()
    {
        Invoke("DestroyProjectile", lifetime);
        gunShot = GameObject.FindGameObjectWithTag("sfx");
        gunShot.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);

        if (hitInfo.collider != null &&  hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "EnemyWeapon" && hitInfo.collider.tag != "HealthPackUnlocked")
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<Player>().TakeDamage(damage);
                DestroyProjectile();
            }
            else if (hitInfo.collider.CompareTag("Obstacle"))
            {
                hitInfo.collider.GetComponent<Obstacle>().TakeDamage(damage);
                DestroyProjectile();
            }
            else if (hitInfo.collider.CompareTag("HealthPack"))
            {
                hitInfo.collider.GetComponent<HealthPackage>().TakeDamage(damage);
                DestroyProjectile();
            }
            
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
