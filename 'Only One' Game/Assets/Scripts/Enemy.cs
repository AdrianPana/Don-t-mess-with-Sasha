using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int HP;

    public Transform playerTransform;
    public float speed;
    public float enemyRange;

    public GameObject itemDrop;

    float screenHalfWidth;
    float screenHalfHeight;

    bool hasDroppedItem = false;

    

    private void Start()
    {
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        screenHalfHeight = Camera.main.orthographicSize;
    }
    void Update() {

        if (HP <= -50)
        {
            Invoke("Die", .3f);
        }
        else if (HP <=0)
        {
            Die();
        }
    }
	


    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    void Die()
    {
        if (hasDroppedItem == false)
        {
            GameObject droppedItem = Instantiate(itemDrop, transform.position, Quaternion.identity) as GameObject;
            droppedItem.transform.parent = this.transform.parent.parent;
            hasDroppedItem = true;
        }

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
        
    }

    void OldTracking()
    {
            Vector3 displacementFromTarget = playerTransform.position - transform.position;
                    Vector3 directionToTarget = displacementFromTarget.normalized;
                    Vector3 velocity = directionToTarget * speed;

                    float distanceToTarget = displacementFromTarget.magnitude;

                    if (distanceToTarget > enemyRange)
                    {
                        transform.Translate(velocity * Time.deltaTime);
                    }
        
    }
}
