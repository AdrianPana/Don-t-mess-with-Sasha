using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage; 
    public float speed;
    public float lifetime;
    public float distance;
    public Collider2D currentEnemy;

    GameObject gunShot;

    void Start () {
        Invoke("DestroyProjectile", lifetime);
        gunShot = GameObject.FindGameObjectWithTag("sfx");
        gunShot.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector2.up * speed * Time.deltaTime);

      
        
	}

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void shootAttemptOne()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);

        if (hitInfo.collider != null) Debug.Log("hit");

        if (hitInfo.collider != null && hitInfo.collider.tag != "Player" && hitInfo.collider.tag != "Weapon" && hitInfo.collider.tag != "HealthPackUnlocked")
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
                if (currentEnemy == null) { currentEnemy = collision; }
                currentEnemy.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        

        Obstacle wall = collision.GetComponent<Obstacle>();
        
        //if (collision.tag == "Obstacle")
        if (wall!= null)
        {
            wall.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            HealthPackage hppack = collision.GetComponent<HealthPackage>();
            if (hppack != null)
            {
                hppack.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
       
    }
}
