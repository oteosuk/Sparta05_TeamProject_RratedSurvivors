using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectScale : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponentInParent<Rigidbody2D>().transform;
    }

    private void Update()
    {
        transform.localScale = new Vector3(_transform.localScale.x , 1, 1);
    }

}
