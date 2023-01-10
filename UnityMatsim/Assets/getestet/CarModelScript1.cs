using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModelScript1 : MonoBehaviour
{
    public List<List<Vector3>> wayPoints = new List<List<Vector3>>();

    public float speed;
    float waypointRadius = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (wayPoints.Count != 0) {
            transform.position = wayPoints[0][0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            for (int j = 0; j < wayPoints[i].Count;)
            {
                if (wayPoints[i].Count > 2)
                {
                    if (Vector3.Distance(wayPoints[i][j], transform.position) < waypointRadius)
                    {
                        j++;
                        Debug.Log(Vector3.Distance(wayPoints[i][j], transform.position));
                    }

                }
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[i][j], Time.deltaTime * speed);
            }
        }
    }
}
