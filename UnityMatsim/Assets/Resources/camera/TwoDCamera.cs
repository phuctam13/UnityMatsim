using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDCamera : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 10;
    public Camera ZoomCamera;

    public float moveSpeed = 1000;

    XmlReaderNetwork1 network;

    // Start is called before the first frame update
    void Start()
    {
        network = GameObject.FindObjectOfType<XmlReaderNetwork1>();
    }

    // Update is called once per frame
    void Update()
    {
        ZoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        MoveCamera();

        if (Input.GetKeyDown(KeyCode.K))
        {
            Set2DCameraToNetwork();
        }
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3 (0,0,1) * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
        }
    }

    private void Set2DCameraToNetwork()
    {
        Vector3 vector = network.networkPosition;
        transform.position = new Vector3(vector.x, transform.position.y, vector.z);
    }
}