using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float money = 800000;
    public float ammo = 0;
    public float power = 0;
    public float spares = 0;

    public List<GameObject> worlds;
    public List<GameObject> ships;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            worlds.Add(planet);

            if(!planet.GetComponent<Planet>().discovered)
            {
                planet.SetActive(false);
            }
        }

        foreach (GameObject nonPlanet in GameObject.FindGameObjectsWithTag("NonPlanet"))
        {
            worlds.Add(nonPlanet);

            if (!nonPlanet.GetComponent<Planet>().discovered)
            {
                nonPlanet.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
