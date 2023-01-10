using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModelScript : MonoBehaviour
{
    public List<List<Vector3>> wayPoints = new List<List<Vector3>>();

    public float speed;
    float waypointRadius = 10;
    int i = 0, j = 0;
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
        if (Vector3.Distance(wayPoints[i][j], transform.position) < waypointRadius)
        {
            if(j< wayPoints[i].Count-1)
            {
                j++;
            }
            else
            {
                if (i == wayPoints.Count-1)
                {
                    i = 0;
                    j = 0;
                }
                else
                {

                    j = 0;
                    i++;
                }
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[i][j], Time.deltaTime * speed);
        }

    }
}
