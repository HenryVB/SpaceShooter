using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject starPreFab;
    [SerializeField]
    private int maxStars;

    private Color[] starColors =
    {
        new Color (0.5f,0.5f,1f), //blue
        new Color (0,1f,1f), //green
        new Color (1f,1f,0), //yellow
        new Color (1f,0,0), //yellow
    };
    
    // Start is called before the first frame update
    void Start()
    {
        //Bounds Screen
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        for(int i = 0; i < maxStars; i++)
        {
            GameObject star = (GameObject)Instantiate(starPreFab);
            
            //set color random by position
            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            //set star position randomly in map
            star.transform.position = new Vector3(Random.Range(min.x,max.x), Random.Range(min.y, max.y));

            //random speed for star
            star.GetComponent<Star>().Speed = -(1f * Random.value + 0.5f);

            //star a child of the StarGenerator
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
