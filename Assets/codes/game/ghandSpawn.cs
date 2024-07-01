using System.Collections;
using UnityEngine;

public class ghandSpawn : MonoBehaviour
{
    public GameObject handPrefab;
    private Transform finalParentTransform; // Assign the final parent GameObject in the Inspector
    public float throwHeight = 50f; // Height to which the hand will rise
    public float throwDuration = 0.5f; // Duration of the throw effect
    public float finalPositionOffset = -50f; // Final position offset from the original position
    public float minXOffset = -50f; // Minimum X offset for randomization
    public float maxXOffset = 50f; // Maximum X offset for randomization
    public int spawnAmount = 5; // Number of instances to spawn
    public float initialDelay = 1.0f; // Initial delay before starting the spawn operation

    private RectTransform initialParentTransform; // Initial parent RectTransform

    void Start()
    {
        finalParentTransform = transform.parent.parent;
        initialParentTransform = GetComponent<RectTransform>(); // Set the current GameObject as the initial parent
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(DelayedSpawnHand());
        }
    }

    public IEnumerator DelayedSpawnHand()
    {
        yield return new WaitForSeconds(initialDelay); // Wait for the specified initial delay

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject handInstance = Instantiate(handPrefab, initialParentTransform);
            RectTransform handRectTransform = handInstance.GetComponent<RectTransform>();
            StartCoroutine(ThrowEffect(handRectTransform));
        }
    }

    private IEnumerator ThrowEffect(RectTransform handRectTransform)
    {
        Vector2 originalPosition = handRectTransform.anchoredPosition;
        float randomXOffset = Random.Range(minXOffset, maxXOffset);
        Vector2 targetPositionUp = originalPosition + new Vector2(randomXOffset, throwHeight);
        Vector2 targetPositionDown = originalPosition + new Vector2(randomXOffset, finalPositionOffset);

        float elapsedTime = 0f;

        // Move upwards
        while (elapsedTime < throwDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / throwDuration;
            handRectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPositionUp, t);
            yield return null;
        }

        elapsedTime = 0f;

        // Move downwards
        while (elapsedTime < throwDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / throwDuration;
            handRectTransform.anchoredPosition = Vector2.Lerp(targetPositionUp, targetPositionDown, t);
            yield return null;
        }

        handRectTransform.anchoredPosition = targetPositionDown; // Ensure the hand is exactly at the final position at the end
        handRectTransform.SetParent(finalParentTransform, true); // Change the parent transform to the final parent
    }


}
