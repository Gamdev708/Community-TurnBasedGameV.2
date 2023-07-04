using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TBRPG.Attributes;
using UnityEngine;

public class testhealth : MonoBehaviour
{
    private List<GameObject> Players = new();
    private List<GameObject> Enemies = new();

    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player").ToList();
        Enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject p in Players)
        {
            if (!p.GetComponent<Health>().IsDead())
            {
                Debug.Log(p.name + ":" + p.GetComponent<Health>().GetHealthPoints()); 
            }
            else
            {
                Debug.Log(p.name + " is Dead");
            }
        }

        foreach (GameObject p in Enemies)
        {
            if (!p.GetComponent<Health>().IsDead())
            {
                Debug.Log(p.name + ":" + p.GetComponent<Health>().GetHealthPoints());
            }
            else
            {
                Debug.Log(p.name + " is Dead");
            }
        }
    }
}
