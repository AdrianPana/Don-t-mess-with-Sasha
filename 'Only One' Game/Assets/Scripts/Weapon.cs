using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float offset;

    public GameObject projectile;
    public Transform shotPoint;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (PauseMenu.gameIsPaused == false)
        {

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            OrientWeapon();
            Shoot();
        }
	}

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            this.transform.parent.GetComponent<Player>().hasWeapon = false;
            Destroy(gameObject);

        }
    }

    void OrientWeapon()
    {

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z <= 180)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;

        }

        else
        if (transform.eulerAngles.z <= 360 && transform.eulerAngles.z > 180)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;

        }

    }
}
