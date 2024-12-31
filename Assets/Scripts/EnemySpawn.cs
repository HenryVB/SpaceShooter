using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private float maxSpawnRateInSeconds;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        //Get Player height and width
        GameObject playerShip = GameObject.FindWithTag("PlayerShipTag");
        SpriteRenderer spriteRenderer = playerShip.GetComponent<SpriteRenderer>();
        float spriteWidth = spriteRenderer.bounds.size.x;
        float spriteHeight = spriteRenderer.bounds.size.y;

        //Limites de pantalla
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        //Ajust limits to width and height of the ship to create enemies
        max.x -= spriteWidth;
        min.x += spriteWidth;

        max.y -= spriteHeight;
        min.y += spriteHeight;


        //Generacion de un nuevo enemigo
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = new Vector3(Random.Range(min.x,max.x),max.y);

        //Crea mas enemigos
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f,maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    //para Incrementar dificultad
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawn()
    {
        maxSpawnRateInSeconds = 5f;
        //Crear un enemigo cada 5s
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //Incrementa el spawn rate cada 30s
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
    public void UnscheduleEnemySpawn()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
