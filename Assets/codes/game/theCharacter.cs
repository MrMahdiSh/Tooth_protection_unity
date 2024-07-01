using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class theCharacter : MonoBehaviour
{
    public float speed = 2f;

    public float wait = 2f;

    public float health = 100;

    public float damage = 5;

    public float hitDamage = 20;

    public bool isWalking = false;

    public bool angerAble = false;

    public float anger = 50f;

    public float angerFactor = 2f;

    private GameObject castle;

    public bool isAnger = false;

    public float castleDistance = 300f;

    public bool isClickAble = false;

    public bool outHitAnim = true;

    public Animator hitAnimation;

    public GameObject ghandSpawn;

    public bool shirini;

    public bool abnabat;

    public bool nooshabe;

    public float noshaebAutoDestroy;

    public float shiriniPartab;

    public thrower thrower;

    private bool isThrown;

    private bool isNoshabeDestroy;

    void Start()
    {
        StartCoroutine(waitAndMove());
        castle = GameObject.Find("castle");
    }

    System.Collections.IEnumerator waitAndMove()
    {
        yield return new WaitForSeconds(wait);

        GetComponent<Animator>().Play("walking");

        isClickAble = true;

        isWalking = true;
    }
    void Update()
    {
        if (!shirini && !abnabat)
        {
            if (isWalking)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        else
        {
            if (isWalking)
            {
                transform.parent.Translate(Vector3.left * speed * Time.deltaTime);
            }

        }

        // check if character is close enough
        if (Vector3.Distance(transform.position, castle.transform.position) < castleDistance)
        {
            isWalking = false;
            startHitting();
        }

        // nooshabe auto anger
        if (nooshabe && !isNoshabeDestroy && Vector3.Distance(transform.position, castle.transform.position) < noshaebAutoDestroy)
        {
            isNoshabeDestroy = true;
            StartCoroutine(doAnger());
        }

        // shirini throw
        if (shirini && !isThrown && Vector3.Distance(transform.position, castle.transform.position) < shiriniPartab)
        {
            isThrown = true;
            startThrow();
        }

    }

    private void startHitting()
    {
        GetComponent<Animator>().Play("attack");
    }

    public void userClicks()
    {
        if (health > 0 && isClickAble)
        {
            if (health - hitDamage > 0)
            {
                if (outHitAnim)
                {
                    hitAnimation.GetComponent<Animator>().Play("hurt");
                }
                else
                {
                    Debug.Log("for this kind of hit we don't have any animation at this time!");
                }
            }

            health -= hitDamage;

            if (health <= 0)
            {
                death();
            }

            if (health <= anger && !isAnger && angerAble)
            {
                StartCoroutine(doAnger());
            }
        }
    }

    void death()
    {
        isWalking = false;
        GetComponent<Animator>().Play("dead");

        StartCoroutine(removeAfterDeath());

        if (shirini)
        {
            thrower.isThrowing = false;
        }
    }

    System.Collections.IEnumerator doAnger()
    {
        isWalking = false;

        isAnger = true;

        isClickAble = false;

        GetComponent<Animator>().Play("angry");

        StartCoroutine(ghandSpawn.GetComponent<ghandSpawn>().DelayedSpawnHand());

        float animationLength = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;

        // speed *= angerFactor;

        yield return new WaitForSeconds(animationLength);

        Destroy(this.gameObject);

        // isWalking = true;

    }


    System.Collections.IEnumerator removeAfterDeath()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    public void startThrow()
    {
        isWalking = false;

        Debug.Log("here1");

        GetComponent<Animator>().Play("attack");

        thrower.StartThrowing();
    }

}
