using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theActualGameManger myManager = GameObject.Find("theActualGameManger").GetComponent<theActualGameManger>();
        if (myManager.IsMusic == false || myManager.isSound == false)
            gameObject.SetActive(false);
    }
}
