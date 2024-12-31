using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        
        position = new Vector3(position.x,position.y + Speed * Time.deltaTime);
        transform.position = position;

        //Bounds Screen
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        //Validate if start is below min bound and reposition it to the top
        if(transform.position.y < min.y)
            transform.position = new Vector3(Random.Range(min.x,max.x),max.y);
       
    }
}
