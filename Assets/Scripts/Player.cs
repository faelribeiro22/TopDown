using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;
    Animator anim;
    public Image[] hearts;
    public int maxHealth;
    public int currentHealth;
    public GameObject sword;
    public float thrustPower;
    public bool canMove;
    public bool canAttack;
    public bool iniFrames;
    SpriteRenderer sr;
    float iniTimer = 1f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        getHealth();
        canMove = true;
        canAttack = true;
        iniFrames = false;
        sr = GetComponent<SpriteRenderer>();
    }
	
    void getHealth()
    {
        for (int i = 0; i <= hearts.Length - 1; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
        for (int i = 0; i <= currentHealth - 1; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }

	// Update is called once per frame
	void Update () {
        Movement();
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        getHealth();
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (iniFrames)
        {
            iniTimer -= Time.deltaTime;
            int rn = Random.Range(0, 100);
            if (rn < 50)
            {
                sr.enabled = false;
            }
            if (rn > 50)
            {
                sr.enabled = true;
            }
            if (iniTimer <= 0)
            {
                iniTimer = 1f;
                iniFrames = false;
                sr.enabled = true;
            }
        }
    }

    void Movement() {
        /*
         * forma que era feita: Input.GetKey(KeyCode.S)
         */
         if(!canMove)
        {
            return;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            anim.SetInteger("dir", 0);
            startSpeedAnim();
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if(Input.GetAxisRaw("Vertical") < 0)
        {
            anim.SetInteger("dir", 1);
            startSpeedAnim();
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetInteger("dir", 2);
            startSpeedAnim();
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.SetInteger("dir", 3);
            startSpeedAnim();
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            stopSpeedAnim();
        }
    }

    void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        canMove = false;
        canAttack = false;
        GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);
        if(currentHealth == maxHealth )
        {
            newSword.GetComponent<Sword>().special = true;
            canMove = true;
            thrustPower = 500;
        }
        #region //SwordRotation
        int swordDir = anim.GetInteger("dir");
        anim.SetInteger("attackDir", swordDir);
        if (swordDir == 0)
        {
            newSword.transform.Rotate(0, 0, 0);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
        }
        else if (swordDir == 1)
        {
            newSword.transform.Rotate(0, 0, 180);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
        }
        else if (swordDir == 2)
        {
            newSword.transform.Rotate(0, 0, 90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
        }
        else if (swordDir == 3)
        {
            newSword.transform.Rotate(0, 0, -90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
        }
        #endregion
    }

    void startSpeedAnim()
    {
        anim.speed = 1;
    }

    void stopSpeedAnim()
    {
        anim.speed = 0;
    }
}
