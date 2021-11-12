using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackage : MonoBehaviour
{
    
    int HP = 1;
    public bool isOpen = false;

    public GameObject hpUnlocked;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        /* if (HP <= 0) animator.SetBool("isOpen", true);
         isOpen = true;*/
        // Destroy(this.GetComponent<Rigidbody2D>());

        GameObject floatingHealth = Instantiate(hpUnlocked, transform.position, Quaternion.identity) as GameObject;
        floatingHealth.transform.parent = this.transform.parent;
        Destroy(this.gameObject);
        AstarPath.active.Scan();

    }
}
