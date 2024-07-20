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
    public int damageInterval = 2;
    private GameObject lase;
    public int id;
    public bool badParent;
    private bool isCharacterCame = false;
    public Animator NakhKillAnim;
    public bool isDeath = false;
    public sliderManager slider;
    public GameObject sliderPrefab;
    public float firstHealth;
    public bool isGhand;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public float nooshabeWalkOne, nooshabeWalkTwo = 0;
    private bool isCounting = false;
    private float elapsedTime = 0f;
    public GameObject hitArea;

    void Start()
    {
        if (!nooshabe)
        {
            hitDamage = 100;
        }
        // addd sound to the component
        audioSource = gameObject.AddComponent<AudioSource>();
        firstHealth = health;
        if (nooshabe)
        {
            Transform sliderTransform = GameObject.Find("slider").transform;
            GameObject created = Instantiate(sliderPrefab, sliderTransform);
            slider = created.GetComponent<sliderManager>();
        }
        StartCoroutine(waitAndMove());
        lase = GameObject.Find("lase");
        castle = GameObject.Find("castle");
        if (transform.parent.name == "spawn" || transform.parent.name == "spawn (1)")
        {
            badParent = false;
        }
        else
        {
            badParent = true;
        }
        if (!badParent)
        {
            (transform as RectTransform).anchoredPosition = new Vector2((transform as RectTransform).anchoredPosition.x, 0);
        }
        else
        {
            (transform.parent.transform as RectTransform).anchoredPosition = new Vector2((transform as RectTransform).anchoredPosition.x, 0);
        }
        if (isGhand)
        {
            return;
        }
        if (badParent)
        {
            if (transform.parent.transform.parent.name == "spawn")
            {
                id = 0;
            }
            else
            {
                id = 1;
            }
        }
        else
        {
            if (transform.parent.name == "spawn")
            {
                id = 0;
            }
            else
            {
                id = 1;
            }


        }
    }
    private IEnumerator WalkingCycle()
    {
        while (true)
        {
            speed /= 2;
            yield return new WaitForSeconds(nooshabeWalkOne);
            speed *= 2;
            yield return new WaitForSeconds(nooshabeWalkTwo);
        }
    }

    void startCoroutine()
    {
        StartCoroutine(WalkingCycle());

    }

    System.Collections.IEnumerator waitAndMove()
    {
        yield return new WaitForSeconds(wait);

        if (nooshabe)
        {
            Invoke("startCoroutine", .61f);
            isCounting = true;
            isWalking = true;
            speed /= 2;
        }
        else
        {
            isWalking = true;

        }

        GetComponent<Animator>().Play("walking");

        isClickAble = true;

    }
    void Update()
    {
        if (!isWalking)
        {
            if (!isCharacterCame)
            {
                hitArea.SetActive(false);
            }
            else
            {
                hitArea.SetActive(true);
            }
        }
        else
        {
            hitArea.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            elapsedTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
        }

        if (isCounting)
        {
            elapsedTime += Time.deltaTime;
        }

        if (isGhand)
        {
            if (badParent)
            {
                if (transform.parent.transform.parent.name == "spawn")
                {
                    id = 0;
                }
                else
                {
                    id = 1;
                }
            }
            else
            {
                if (transform.parent.name == "spawn")
                {
                    id = 0;
                }
                else
                {
                    id = 1;
                }


            }

        }
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     damageTeeth();
        // }

        if (!badParent)
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
        if (!isCharacterCame && Vector3.Distance(transform.position, castle.transform.position) < castleDistance)
        {
            isCharacterCame = true;
            isWalking = false;
            startHitting();
            startDamaging();
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
            startDamaging();

        }

    }

    private void startHitting()
    {
        GetComponent<Animator>().Play("attack");
    }

    private void startDamaging()
    {
        InvokeRepeating("damageTeeth", 0f, damageInterval);
    }


    void damageTeeth()
    {
        if (!isDeath)
        {
            PlayHitSound();

            toothManager[] toothManagers = lase.GetComponents<toothManager>();

            foreach (var item in toothManagers)
            {
                if (item.id == id)
                {
                    item.damageRecived(damage);
                }
            }
        }
    }


    public void userClicks()
    {
        if (health > 0 && isClickAble)
        {
            PlayHitSound();
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

            if (nooshabe && !isAnger)
                slider.mainSlider.value = health / firstHealth;

            if (isGhand)
                slider.ghandDamage(hitDamage);


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

    public void death()
    {
        isDeath = true;
        isWalking = false;
        GetComponent<Animator>().Play("dead");

        StartCoroutine(removeAfterDeath());

        if (shirini)
        {
            thrower.isThrowing = false;
        }
    }

    public void deathByKhamir()
    {
        removeSlider();
        isDeath = true;
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

        StartCoroutine(ghandSpawn.GetComponent<ghandSpawn>().DelayedSpawnHand(slider));

        // float animationLength = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;

        yield return new WaitForSeconds(7);

        Destroy(this.gameObject);

    }


    System.Collections.IEnumerator removeAfterDeath()
    {
        yield return new WaitForSeconds(3);

        if (!badParent)
            Destroy(gameObject);
        else
            Destroy(transform.parent.gameObject);
    }

    public void startThrow()
    {
        isWalking = false;

        GetComponent<Animator>().Play("attack");

        thrower.StartThrowing();
    }

    public void killByNakh()
    {
        removeSlider();
        isDeath = true;
        isWalking = false;
        GetComponent<Animator>().Play("dead");
        NakhKillAnim.Play("explotion");

        StartCoroutine(removeAfterDeath());

        if (shirini)
        {
            thrower.isThrowing = false;
        }


    }

    void removeSlider()
    {
        if (isGhand)
        {
            slider.ghandDie();
        }

        if (nooshabe)
        {
            Destroy(slider.gameObject);
        }
    }
    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

}
