using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door2 : MonoBehaviour
{

    // public string sceneName;
    bool isOpen;

    public Animator animator;

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

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && isOpen == true)
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
