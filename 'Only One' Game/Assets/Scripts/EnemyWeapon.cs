using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public Transform player;
    public GameObject projectile;
    public float offset;
    public float delayBetweenAttacks;
    public Transform shotPoint;

    float range;
    float distance;
    float timer=1f;

	// Use this for initialization
	void Start () {

        range = GetComponentInParent<Enemy>().enemyRange;

        timer = delayBetweenAttacks;

     

    }
	
	// Update is called once per frame
	void Update () {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        CheckForPlayer();

        if (range > distance && timer <= 0)
        {
            Invoke("Shoot",.25f);
            timer = delayBetweenAttacks;
        }
      

    }

    void Raycasting()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, player.position, transform.parent.GetComponent<Enemy>().enemyRange);

        if (hitInfo.collider != null && hitInfo.collider.tag != "Enemy")
        {
            if (hitInfo.collider.tag == "Player")
            {
                Debug.Log(hitInfo.collider.tag);
            }
        }
    }

    void CheckForPlayer()
    {
        Vector3 relativePos = player.position - transform.position;
        distance = Vector2.Distance(transform.position, player.transform.position);

        OrientWeapon();

        float rotZ = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        
        
    }
    
void OrientWeapon()
    {

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z <= 180)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            transform.parent.GetComponent<SpriteRenderer>().flipX = false;
        }

        else
        if (transform.eulerAngles.z <= 360 && transform.eulerAngles.z >180)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            transform.parent.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    void Shoot()
    {
        Instantiate(projectile, shotPoint.position, transform.rotation);
    }
}

    