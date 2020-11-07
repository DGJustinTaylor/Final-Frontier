using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool discovered = false;

    public List<GameObject> nextPlanets;

    private bool zoomed = false;

    private GameController gm;
    private LineRenderer pathToNext;
    private Camera mainCam;    

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        pathToNext = this.gameObject.AddComponent<LineRenderer>();
        pathToNext.material = new Material(Shader.Find("Sprites/Default"));
        pathToNext.startColor = Color.red;
        pathToNext.endColor = Color.red;
        pathToNext.startWidth = 0.2f;
        pathToNext.endWidth = 0.2f;
        pathToNext.widthMultiplier = .2f;
        pathToNext.sortingOrder = 3;
        pathToNext.positionCount = 0;
        pathToNext.useWorldSpace = true;

        foreach(GameObject nextPlanet in nextPlanets)
        {
            pathToNext.SetPosition(pathToNext.positionCount++, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0));
            pathToNext.SetPosition(pathToNext.positionCount++, new Vector3(nextPlanet.gameObject.transform.position.x, nextPlanet.gameObject.transform.position.y, 0));
        }
    }

    private void OnMouseDown()
    {
        if(!zoomed)
        {
            mainCam.orthographicSize = 3;
            mainCam.gameObject.transform.localPosition += this.gameObject.transform.localPosition;

            if(this.name == "world_???_special")
            {
                this.gameObject.transform.localScale += new Vector3(6f, 0.8f, 0);
            }

            pathToNext.enabled = false;

            foreach(GameObject world in gm.worlds)
            {
                if(world.name != this.name)
                {
                    world.SetActive(false);
                }
                
            }

            zoomed = true;
        }
        else
        {
            mainCam.orthographicSize = 6;
            mainCam.gameObject.transform.localPosition = new Vector3(0,0,-10);

            if (this.name == "world_???_special")
            {
                this.gameObject.transform.localScale -= new Vector3(6f, 0.8f, 0);
            }

            pathToNext.enabled = true;

            foreach (GameObject world in gm.worlds)
            {
                if(world.GetComponent<Planet>().discovered)
                {
                    world.SetActive(true);
                }
            }

            zoomed = false;
        }
    }

    private void OnMouseEnter()
    {
        if(!zoomed && this.gameObject.tag == "Planet")
        {
            this.gameObject.transform.localScale += new Vector3(0.06f, 0.06f, 0);
        }
        else if(!zoomed && this.gameObject.tag == "NonPlanet")
        {
            this.gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
        }

        //This is here for debugging purposes
        //This code will be handled some other way at a later time
        if(!zoomed)
        {
            foreach (GameObject nextPlanet in nextPlanets)
            {
                nextPlanet.SetActive(true);
                nextPlanet.GetComponent<Planet>().discovered = true;
            }
        }
    }

    private void OnMouseExit()
    {
        if (!zoomed && this.gameObject.tag == "Planet")
        {
            this.gameObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0);
        }
        else if (!zoomed && this.gameObject.tag == "NonPlanet")
        {
            this.gameObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
        }
    }
}
