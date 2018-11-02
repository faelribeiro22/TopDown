using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour {

    public int health;
    public GameObject particleEffect;
    int direction;
    float timer = 1.5f;
    SpriteRenderer spriteRenderer;
    public float speed;
    public Sprite facingUp;
    public Sprite facingDown;
    public Sprite facingLeft;
    public Sprite facingRight;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = facingUp;
        direction = Random.Range(0, 3);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 1.5f;
            direction = Random.Range(0, 3);
        }
        Movement();

    }

    void Movement()
    {
        switch(direction)
        {
            case 0:
                transform.Translate(0, -speed * Time.deltaTime, 0);
                spriteRenderer.sprite = facingDown;
                break;
            case 1:
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                spriteRenderer.sprite = facingLeft;
                break;
            case 2:
                transform.Translate(speed * Time.deltaTime, 0, 0);
                spriteRenderer.sprite = facingRight;
                break;
            case 3:
                transform.Translate(0, speed * Time.deltaTime, 0);
                spriteRenderer.sprite = facingUp;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            health--;
            if (!player.iniFrames)
            {
                player.currentHealth--;
                player.iniFrames = true;
            }

            if (health <= 0)
            {
                Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Sword")
        {
            health--;
            if(health<=0)
            {
                Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            collision.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            direction = Random.Range(0, 3);
        }
    }
}
