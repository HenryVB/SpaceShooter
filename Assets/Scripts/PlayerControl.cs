using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bulletPosition01;
    [SerializeField]
    private GameObject bulletPosition02;
    [SerializeField]
    private GameObject explosionPreFab;
    [SerializeField]
    private TextMeshProUGUI textLifes;
    [SerializeField]
    private GameObject gameManager;

    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private float spriteHeight;
    private const int maxLifes = 3;
    private int currentLifes;
    private AudioSource audioData;

   
    public void Init()
    {
        currentLifes = maxLifes;
        textLifes.text = currentLifes.ToString(); //Text at init
        transform.position = new Vector3(0, 0, 0); //Player position start
        gameObject.SetActive(true); //Show player at init
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
        spriteHeight = spriteRenderer.bounds.size.y;
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        LimitPlayerMovementToBounds();
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioData.Play();
            GameObject bullet01 = (GameObject) Instantiate(bulletPrefab);
            bullet01.transform.position = bulletPosition01.transform.position;
            
            GameObject bullet02 = (GameObject)Instantiate(bulletPrefab);
            bullet02.transform.position = bulletPosition02.transform.position;
        }
    }
    void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(x, y, 0).normalized * speed * Time.deltaTime);
    }

    void LimitPlayerMovementToBounds() {
        Vector3 minBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        maxBound.x -= spriteWidth;
        minBound.x += spriteWidth;

        maxBound.y -= spriteHeight;
        minBound.y += spriteHeight;

        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minBound.x, maxBound.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minBound.y, maxBound.y);

        transform.position = currentPosition;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy if collision is with obj with tags of enemy
        if (collision.CompareTag("EnemyShipTag") || collision.CompareTag("EnemyBulletTag")) {
            DoExplosion();

            currentLifes--;
            textLifes.text = currentLifes.ToString();

            if(currentLifes == 0)
            {
                gameManager.GetComponent<GameManager>().SetGMState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }

    private void DoExplosion() { 
        //Explosion in the same place of object
        GameObject explosion = (GameObject)Instantiate(explosionPreFab);
        explosion.transform.position = transform.position;
    }
}
