using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update usando translate
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        //upper bound right
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

        //Destroy if goes beyond upper limit
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy if collision is with obj with enemy ship
        if (collision.CompareTag("EnemyShipTag"))
        {
            Destroy(gameObject);
        }
    }
}
