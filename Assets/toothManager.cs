using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toothManager : MonoBehaviour
{
    public float damageRecive = 0;

    public int numberOfTeethDamaged;

    public float teethMultiplire = 20;

    public int numberOfteeth;
    public string[] myArray;

    public Image[] theTeeth;

    void Start()
    {

    }


    void Update()
    {
        if ((teethMultiplire * (numberOfTeethDamaged + 1)) <= damageRecive)
        {
            if (numberOfteeth > numberOfTeethDamaged)
            {
                numberOfTeethDamaged++;
            }
        }
    }

    public void damageRecived(int damage)
    {
        damageRecive += damage;

        for (int i = 0; i < numberOfTeethDamaged; i++)
        {
            theTeeth[i].GetComponent<teethProfile>().recieveDamage();
        }
    }
}
