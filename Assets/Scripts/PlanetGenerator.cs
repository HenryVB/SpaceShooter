using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] planets;

    private Queue<GameObject> availablePlanets = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        availablePlanets.Enqueue(planets[0]);
        availablePlanets.Enqueue(planets[1]);
        availablePlanets.Enqueue(planets[2]);

        //Call show planets every 20 secs
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Show the planet
    void MovePlanetDown()
    {
        EnqueuePlanets();

        if (availablePlanets.Count == 0)
            return;

        GameObject nPlanet = availablePlanets.Dequeue();

        nPlanet.GetComponent<Planet>().IsMoving = true;
    }

    //Enqueue planets below bounds of screen to show again in top
    void EnqueuePlanets()
    {
        foreach (GameObject iPlanet in planets)
        {
            if ((iPlanet.transform.position.y < 0) && (!iPlanet.GetComponent<Planet>().IsMoving)) { 
                iPlanet.GetComponent <Planet>().ResetPosition();
                availablePlanets.Enqueue(iPlanet);
            }

        }
    }
}
