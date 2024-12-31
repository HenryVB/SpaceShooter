using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool isMoving;
    private Vector2 minBoundScreen;
    private Vector2 maxBoundScreen;
    private SpriteRenderer spriteRenderer;
    private float spriteHeight;

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    private void Awake()
    {
        IsMoving = false;

        minBoundScreen = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        maxBoundScreen = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Height y: "+ spriteRenderer.size.y +" VS extents: "+ spriteRenderer.bounds.extents.y);
        
        //Half of size of height
        spriteHeight = spriteRenderer.bounds.extents.y;

        maxBoundScreen.y += spriteHeight;
        minBoundScreen.y -= spriteHeight;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMoving)
            return;

        Vector3 position = transform.position;

        position = new Vector3(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;

        //lower than bounds stop moving
        if(transform.position.y < minBoundScreen.y)
            IsMoving = false;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(minBoundScreen.x, maxBoundScreen.x), maxBoundScreen.y);
    }
}
