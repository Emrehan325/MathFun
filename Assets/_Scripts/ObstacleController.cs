using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour
{
    [Header("FLOAT")]
    public float speed;

    void Start()
    {
        transform.DOMoveZ(-20,10).OnComplete(()=>
        Destroy(gameObject));
    }


    void Update()
    {
        
    }

}
