using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int ObstacleHP = 1;
    public Sprite newSprite;
    public Sprite newSprite2;
    public Sprite newSprite3;
    public Sprite newSprite4;
    int maxHP;

    void Start()
    {
         maxHP = ObstacleHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHP - ObstacleHP == 1) this.GetComponent<SpriteRenderer>().sprite = newSprite;
        else if (maxHP - ObstacleHP == 2 && ObstacleHP > 0) this.GetComponent<SpriteRenderer>().sprite = newSprite2;
        else if (maxHP - ObstacleHP == 3 && ObstacleHP > 0) this.GetComponent<SpriteRenderer>().sprite = newSprite3;
        else if (maxHP - ObstacleHP == 4 && ObstacleHP > 0) this.GetComponent<SpriteRenderer>().sprite = newSprite4;
    }

    public void TakeDamage(int damage)
    {
        ObstacleHP -= damage;
        if (ObstacleHP <= 0) Destroy(gameObject);
        AstarPath.active.Scan();
    }
}
