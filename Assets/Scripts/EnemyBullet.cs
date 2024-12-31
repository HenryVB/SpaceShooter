using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 _direction;
    private bool isReady;

    private void Awake()
    {
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet position
        Vector3 position = transform.position;

        //Update position
        position += _direction * speed * Time.deltaTime;
        transform.position = position;

        //Bounds of screen
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

        //Destroy bullet if is out of bounds
        if((transform.position.x < min.x) || (transform.position.x > max.x) ||
           (transform.position.y < min.y) || (transform.position.y > max.y))
        {
            Destroy(gameObject);
        }

    }

    //Set Bullet Direction
    public void SetDirection(Vector3 direction)
    {
        //Set Direction to Unit Vector
        _direction = direction.normalized;
        isReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy if collision is with obj with ship of player
        if (collision.CompareTag("PlayerShipTag"))
        {
            Destroy(gameObject);
        }
    }
}
