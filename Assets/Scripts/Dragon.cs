using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

    Animator anim;
    public float speed;
    public int dir;
    float dirTimer = .7f;
    public int health;

	void Start () {
        anim = GetComponent<Animator>();
        dir = Random.Range(0, 3);
	}
	
	void Update () {
        dirTimer -= Time.deltaTime;
        if (dirTimer <= 0)
        {
            dirTimer = .7f;
            dir = Random.Range(0, 3);
        }
        Movement();
	}

    void Movement()
    {
        if (dir == 0)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            anim.SetInteger("dir", dir);
        } else if (dir == 1)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", dir);
        } else if (dir == 2)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            anim.SetInteger("dir", dir);
        } else if (dir == 3)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", dir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            health--;
            collision.gameObject.GetComponent<Sword>().CreateParticle();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            collision.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            Destroy(collision.gameObject);
        }
    }
}
