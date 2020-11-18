using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Palette : MonoBehaviour
{
    public GameObject palette;
    public float moveSpeed = 0.25f;

    private float x = 0f;

    void Start()
    {
        //moveSpeed = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        x += h * moveSpeed;

        if (x > 13f)
            x = 13f; 
        else if (x < -13f)
            x = -13f;

        palette.transform.position = new Vector3(x, 0, 0);
    }
}
