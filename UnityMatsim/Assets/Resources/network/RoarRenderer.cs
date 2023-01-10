using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarRenderer : MonoBehaviour
{
    Renderer rend;
    float x;
    float multiplyer;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        x = transform.localScale.x;

        multiplyer = Mathf.Floor(x / 100);

        rend.material.mainTextureScale = new Vector2(multiplyer, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
