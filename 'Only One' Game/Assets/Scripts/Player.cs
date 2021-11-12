using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {

    public float speed = 10f;
    public float speedBoost = 2f;

    float screenHalfWidth;
    float screenHalfHeight;
    float halfPlayerWidth;
    float halfPlayerHeight;

    public int HP;
    int maxHP = 3;
    public GameObject heart1, heart2, heart3, heart4;
    public GameObject bonusHeart;

    public bool hasWeapon = true;

    public Sprite full_heart;
    public Sprite empty_heart;

    Queue<GameObject> enemiesInRangeList ;

    public float attackDelay = 0;
    public float attackDelayThreshold = 1f;

    private Rigidbody2D body;

    public float range = 1f;

    public Collider2D currentEnemy; 
   // bool canPickUpWeapon = false;

    private Vector2 screenBounds;

    public GameObject weapon;
    public Transform weaponGripPoint;

    public Animator animator;

    GameObject door_opening;
    public bool doorsfx = false;
    GameObject damageTakenSfx;

    private void Start()
    {
        Time.timeScale = 1f;

        halfPlayerWidth = transform.localScale.x / 2f;
        halfPlayerHeight = transform.localScale.y / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize ;
        screenHalfHeight = Camera.main.orthographicSize;
        body = GetComponent<Rigidbody2D>();
        enemiesInRangeList = new Queue<GameObject>();

        door_opening = GameObject.FindGameObjectWithTag("sfx1");
        damageTakenSfx = GameObject.FindGameObjectWithTag("sfx2");
    }

    void Update() {

        Debug.Log(Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        if (attackDelay >= 0.3f) move();

        if (doorsfx == false)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy == null)
            {
                doorsfx = true;
                door_opening.GetComponent<AudioSource>().Play();
            }
        }

    }

    private void move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 movement = velocity * Time.deltaTime;

        body.MovePosition(new Vector2((transform.position.x + input.normalized.x * speed * Time.deltaTime),
            (transform.position.y + input.normalized.y * speed * Time.deltaTime)));

        HeartUIManage();
    }

    private void LateUpdate()
    {

        attackDelay += Time.deltaTime;
        if (attackDelay >= 0.3f) animator.SetBool("isAttacking", false);

        if (hasWeapon == false)
        if (Input.GetKeyDown(KeyCode.Space) && enemiesInRangeList.Count != 0 && attackDelay>=attackDelayThreshold )
        {
            animator.SetBool("isAttacking", true);
            enemiesInRangeList.Dequeue().GetComponent<Enemy>().TakeDamage(100);
            attackDelay = 0;
        }
        


        enemiesInRangeList.Clear();
        Vector3 posCoord = transform.position;
        posCoord.x = Mathf.Clamp(transform.position.x, -screenHalfWidth + halfPlayerWidth, screenHalfWidth - halfPlayerWidth);
        posCoord.y = Mathf.Clamp(transform.position.y, -screenHalfHeight + halfPlayerHeight, screenHalfHeight - halfPlayerHeight);
        transform.position = posCoord;
    }

    void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "DroppedWeapon") {


            if (hasWeapon == false)
            {
                Destroy(triggerCollider.gameObject);
                
                Instantiate(weapon, weaponGripPoint.position, Quaternion.identity, gameObject.transform);
                hasWeapon = true;

            }
           
        }

        else if (triggerCollider.tag == "Enemy")
        {
            enemiesInRangeList.Enqueue(triggerCollider.gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HealthPackUnlocked" && HP < maxHP)
        {
            HP += 1;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "SpeedBoost")
        {
            speed += speedBoost;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "HealthBoost")
        {
            HP++;
            maxHP++;
            bonusHeart.SetActive(true);
            Destroy(collision.gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        HP -= damage;
        damageTakenSfx.GetComponent<AudioSource>().Play();

        if (HP <= 0)
        {
            gameObject.SetActive(false);
            GameObject music = GameObject.FindGameObjectWithTag("Music");
            Destroy(music);
        }
    }

    public void HeartUIManage()
       {
        switch (HP)
        {
            case 4:
                heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart4.GetComponent<SpriteRenderer>().sprite = full_heart;
                break;
            case 3:
                heart1.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart4.GetComponent<SpriteRenderer>().sprite = empty_heart;
                break;
            case 2:
                heart1.GetComponent<SpriteRenderer>().sprite = empty_heart;
                heart2.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart4.GetComponent<SpriteRenderer>().sprite = empty_heart;
                break;
            case 1:
                heart1.GetComponent<SpriteRenderer>().sprite = empty_heart;
                heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
                heart3.GetComponent<SpriteRenderer>().sprite = full_heart;
                heart4.GetComponent<SpriteRenderer>().sprite = empty_heart;
                break;
            case 0:
                heart1.GetComponent<SpriteRenderer>().sprite = empty_heart;
                heart2.GetComponent<SpriteRenderer>().sprite = empty_heart;
                heart3.GetComponent<SpriteRenderer>().sprite = empty_heart;
                heart4.GetComponent<SpriteRenderer>().sprite = empty_heart;
                break;
        }
}

}
