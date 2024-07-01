using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candyBullet : MonoBehaviour
{
    public float speed;
    public float destroyAfterSeconds;

    void Start()
    {
        Invoke("DestroyObject", destroyAfterSeconds);

    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition += Vector2.left * speed * Time.deltaTime * 1000;
    }

    void DestroyObject(){
        Destroy(gameObject);
    }
}
