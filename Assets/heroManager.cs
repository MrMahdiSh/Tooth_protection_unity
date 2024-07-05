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

        if (Input.GetKeyDown(KeyCode.A))
        {
            bringNakh();
        }

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
        khamirUi.SetActive(true);
    }
    void bringNakhUi()
    {
        isNakhCame = true;
        nakhUi.SetActive(true);
    }
    IEnumerator bringKhamir()
    {
        isKhamirCame = true;
        // start it
        khamirUi.SetActive(false);
        Hobab.SetActive(true);
        khamir.SetActive(true);
        zaban.Play("zaban");
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
        isNakhCame = true;
        isNakhIn = true;
        zaban.Play("zaban");
        nakhUi.SetActive(false);
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
            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<theCharacter>().killByNakh();
            }
            else
            {
                nakhKillingDone();
            }
        }
    }

    void nakhKillingDone()
    {
        isNakhIn = false;
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
}
