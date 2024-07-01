using UnityEngine;
using System.Collections;

public class teethProfile : MonoBehaviour
{
    private Animator animator;
    public float stepDuration = 0.1f; // Duration to play the animation

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recieveDamage();
        }
    }

    public void recieveDamage()
    {
        StartCoroutine(StepForward());

    }

    public IEnumerator StepForward()
    {
        animator.speed = 1;
        yield return new WaitForSeconds(stepDuration);
        animator.speed = 0;
    }

}
