﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    float timer = .15f;
    float specialTimer = 1f;
    public bool special;
    public GameObject swordParticle;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("attackDir", 5);
        }
        if (!special)
        {
            if (timer <= 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
                Destroy(gameObject);
            }
        }
        specialTimer -= Time.deltaTime;
        if(specialTimer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            CreateParticle();
            Destroy(gameObject);
        }
	}

    public void CreateParticle()
    {
        Instantiate(swordParticle, transform.position, transform.rotation);
    }

}
