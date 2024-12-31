using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject explosionPreFab;

    private GameObject txtScore;

    // Start is called before the first frame update
    void Start()
    {   
        //Find object score by tag
        txtScore = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //Posicion actual
        Vector3 position = transform.position;
        
        //Update ubicacion
        position = new Vector3(position.x,position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector3 minBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        if(transform.position.y < minBound.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy if collision is with obj with tags of player
        if (collision.CompareTag("PlayerShipTag") || collision.CompareTag("PlayerBulletTag"))
        {
            DoExplosion();
            SetReward();
            Destroy(gameObject);
        }
    }

    private void SetReward()
    {
        txtScore.GetComponent<GameScore>().Score += 100;
    }
    private void DoExplosion()
    {
        //Explosion in the same place of object
        GameObject explosion = (GameObject)Instantiate(explosionPreFab);
        explosion.transform.position = transform.position;
    }
}
