using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    private float destroyTime = 2f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    
}
