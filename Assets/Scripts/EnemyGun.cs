using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyBulletPrefab;
    private float firstFireAfter;

    // Start is called before the first frame update
    void Start()
    {
        firstFireAfter = 1f;
        //Fire after 1s
        Invoke("FireEnemyBullet", firstFireAfter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet()
    {
        //Find object Player
        GameObject playerShip = GameObject.Find("PlayerSS");
       
        //Validate if player is not destroyed / alive
        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(enemyBulletPrefab);
            //Set init position
            bullet.transform.position = transform.position;

            //Vector substraction to redirect bullet to player
            Vector3 newBulletDirection = playerShip.transform.position - bullet.transform.position;

            //Set direction
            bullet.GetComponent<EnemyBullet>().SetDirection(newBulletDirection);
        }
    }
}
