using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;
    Animator anim;
    public Image[] hearts;
    public int maxHealth;
    int currentHealth;
    public GameObject sword;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        getHealth();

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
    }

    void Movement() {
        /*
         * forma que era feita: Input.GetKey(KeyCode.S)
         */
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
        GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);
        int swordDir = anim.GetInteger("dir");
        if (swordDir == 0)
        {
            newSword.transform.Rotate(0, 0, 0);
        }
        else if (swordDir == 1)
        {
            newSword.transform.Rotate(0, 0, 180);
        }
        else if (swordDir == 2)
        {
            newSword.transform.Rotate(0, 0, 90);
        }
        else if (swordDir == 3)
        {
            newSword.transform.Rotate(0, 0, -90);
        }
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
