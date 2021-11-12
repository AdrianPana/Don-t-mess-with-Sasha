using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{

   // public string sceneName;
    bool isOpen;

    public GameObject oldLevel;
    public GameObject newLevel;

    public Animator animator;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isOpen == false)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                isOpen = true;
                animator.SetBool("levelFinished", true);
            }
        }

        else
        {
            
            animator.SetBool("levelFinished", true);
            
        }
        }

    public void OnTriggerEnter2D (Collider2D other)
    {

        if (other.tag == "Player" && isOpen == true)
        {
            oldLevel.SetActive(false);
            newLevel.SetActive(true);
            other.transform.position = spawnPoint.position;
            AstarPath.active.Scan();
            other.GetComponent <Player>().doorsfx = false;
        }

    }
}
