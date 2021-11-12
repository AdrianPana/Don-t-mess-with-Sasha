using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    void Awake()
    {
       /* GameObject[] musicInstances = GameObject.FindGameObjectsWithTag("Music");
        if (musicInstances.Lenght > 1)
            Destroy(this.gameObject);
            */
        DontDestroyOnLoad(this.gameObject);
    }
}
