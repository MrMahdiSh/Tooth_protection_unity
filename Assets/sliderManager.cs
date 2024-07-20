using UnityEngine.UI;
using UnityEngine;

public class sliderManager : MonoBehaviour
{
    public Slider mainSlider;
    public bool isGhandCame;
    private GameObject[] Ghands;
    private float allGandsFirstHealth = 0;
    private float currentGhandsHealthSum = 0;
    private int ghandsCount = 0;

    void Start()
    {
        mainSlider = GetComponent<Slider>();
    }
    void Update()
    {
        if (isGhandCame)
        {
            float ghandsValue = currentGhandsHealthSum / ghandsCount / 100 * .5f;
            GetComponent<sliderManager>().mainSlider.value = ghandsValue;
            if (ghandsValue <= 0)
            {
                GameObject theManager = GameObject.Find("theActualGameManger");
                theManager.GetComponent<theActualGameManger>().fin();
                Destroy(gameObject);
            }
        }
    }
    public void ghandsCame()
    {
        isGhandCame = true;
    }

    public void ghandInitialize(GameObject ghandGameObject, theCharacter ghandCharacter)
    {
        ghandsCount++;
        isGhandCame = true;
        ghandCharacter.slider = GetComponent<sliderManager>();
        allGandsFirstHealth += ghandCharacter.health;
        currentGhandsHealthSum += ghandCharacter.health;
    }

    public void ghandDamage(float damage)
    {
        currentGhandsHealthSum -= damage;
    }

    public void ghandDie()
    {
        currentGhandsHealthSum -= 100;
    }
}
