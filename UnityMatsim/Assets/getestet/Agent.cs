using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public List<List<int>> numberOfCarTrips = new List<List<int>>();
    public List<int> routeList = new List<int>();
    public int id;


    public void PrintList()
    {
        foreach (var x in routeList)
        {
            Debug.Log(x.ToString());
        }

    }
}
