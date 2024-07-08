using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroManager : MonoBehaviour
{
    private toothManager[] toothmanagers;
    public float totalReceivedDamages;
    private bool isKhamirCame = false;
    private bool isNakhCame = false;
    private bool isKhamirUiCame = false;
    private bool isNakhUiCame = false;
    public GameObject khamir;
    public GameObject nakh;
    public GameObject Hobab;
    public GameObject khamirUi;
    public GameObject nakhUi;
    public Animator zaban;
    public float khamirUiCameAfterDamages;
    public float nakhUiCameAfterDamages;
    public float khamirWait;
    public int khamirUiShouldHitCount;
    public int nakhUiShouldHitCount;
    public int numberOfUsersHitKhamirUi;
    public int numberOfUsersHitNakhUi;
    private spawnManager spawnManager;
    private bool teethHealed;
    public bool isNakhIn = false;
    public float nakhRepeatRate;
    public float slowMotionFactor = 0.5f;

    void Start()
    {
        teethHealed = false;
        spawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        toothmanagers = GameObject.Find("lase").GetComponents<toothManager>();
    }

    void Update()
    {
        if (isNakhIn)
        {
            // stop damaging
            foreach (var item in toothmanagers)
            {
                item.canDamage = false;
            }
            // stop spawn
            spawnManager.spawn = false;

        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     bringNakh();
        // }

        if (totalReceivedDamages >= khamirUiCameAfterDamages && !isKhamirUiCame)
        {
            bringKhamirUi();
        }

        if (totalReceivedDamages >= nakhUiCameAfterDamages && !isNakhUiCame)
        {
            bringNakhUi();
        }
        if (numberOfUsersHitKhamirUi >= khamirUiShouldHitCount && !isKhamirCame)
        {
            StartCoroutine(bringKhamir());
        }

        if (numberOfUsersHitNakhUi >= nakhUiShouldHitCount && !isNakhCame)
        {
            bringNakh();
        }
    }

    public void damageReceived()
    {
        totalReceivedDamages = 0;
        foreach (var item in toothmanagers)
        {
            totalReceivedDamages += item.damageRecive;
        }
    }

    void bringKhamirUi()
    {
        isKhamirUiCame = true;
        khamirUi.GetComponent<Animator>().Play("in");
        khamirUi.transform.GetChild(0).gameObject.SetActive(true);
        // slow motion
        slowMotion();
    }
    void bringNakhUi()
    {
        isNakhCame = true;
        nakhUi.GetComponent<Animator>().Play("in");
        nakhUi.transform.GetChild(0).gameObject.SetActive(true);
        // slow motion
        slowMotion();
    }
    IEnumerator bringKhamir()
    {
        // start it
        normalScaleTime();
        isKhamirCame = true;
        khamirUi.GetComponent<Animator>().Play("out");
        khamirUi.transform.GetChild(0).gameObject.SetActive(false);
        Hobab.SetActive(true);
        khamir.SetActive(true);
        zaban.Play("zaban");
        // normal time scale
        // stop damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = false;
        }
        // stop spawn
        spawnManager.spawn = false;
        // roll back teeth
        if (teethHealed == false)
        {
            teethHealed = true;
            foreach (var tooth in toothmanagers)
            {
                tooth.rollbackTheTeethAnimation();
            }
        }

        yield return new WaitForSeconds(khamirWait);
        teethHealed = false;
        // kill enemies
        killEnemiesKhamir();
        // bring back the damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = true;
        }

        // bring back the spawn
        spawnManager.spawn = true;
        // bring the khamir and hobabs out
        khamir.GetComponent<Animator>().Play("back");
        GameObject[] hobabs = GameObject.FindGameObjectsWithTag("hobabs");
        foreach (var hobab in hobabs)
        {
            hobab.GetComponent<Animator>().Play("out");
        }
    }

    void bringNakh()
    {
        // start it
        nakhUi.GetComponent<Animator>().Play("out");
        nakhUi.transform.GetChild(0).gameObject.SetActive(false);
        isNakhCame = true;
        isNakhIn = true;
        zaban.Play("zaban");
        // normal time scale
        normalScaleTime();
        // bring the character in
        nakh.GetComponent<Animator>().Play("in");
        // count enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
        InvokeRepeating("nakhKillingEnemies", nakhRepeatRate, enemies.Length);
    }

    void nakhKillingEnemies()
    {
        if (isNakhIn)
        {
            nakh.GetComponent<Animator>().Play("attack");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
            bool enemiesNumber = false;
            foreach (var item in enemies)
            {
                if (item.GetComponent<theCharacter>().isDeath == false)
                {
                    enemiesNumber = true;
                    item.GetComponent<theCharacter>().killByNakh();
                    break;

                }
            }

            if (enemiesNumber == false)
            {
                nakhKillingDone();
                isNakhIn = false;
            }
        }
    }

    void nakhKillingDone()
    {
        // health teeth
        foreach (var tooth in toothmanagers)
        {
            tooth.rollbackTheTeethAnimation();
        }

        // bring back the damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = true;
        }

        // bring back the spawn
        spawnManager.spawn = true;
        nakh.GetComponent<Animator>().Play("out");
    }

    void killEnemiesKhamir()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<theCharacter>().death();
        }
    }

    public void khamirUserClick()
    {
        numberOfUsersHitKhamirUi++;
    }

    public void nakhUserClick()
    {
        numberOfUsersHitNakhUi++;
    }

    void slowMotion()
    {
        Time.timeScale = slowMotionFactor;
    }

    void normalScaleTime()
    {
        Time.timeScale = 1;
    }
}
