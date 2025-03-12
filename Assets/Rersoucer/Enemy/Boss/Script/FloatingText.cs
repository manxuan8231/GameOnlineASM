using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public float destroyTime = 1f;
    public Vector3 Offset = new Vector3(0f, 2f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += Offset;
    }

    // Update is called once per frame
    void Update()
    {

    }
}