using UnityEngine;

public class background : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public float animationSpeedMin = 1f;     // Speed of texture offset animation
    
    public GameObject spritePrefab;           // Assign your sprite prefab in inspector

    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    public float spriteSpeed = 5f;            // Speed of moving sprite

    private GameObject currentSprite;
    private Vector3 moveDirection;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        SpawnAndMoveSprite();
    }

    private void Update()
    {
        // Animate background texture offset (scrolling texture effect)
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeedMin * Time.deltaTime, 0);

        // Move the sprite if it exists
        if (currentSprite != null)
        {
            currentSprite.transform.position += moveDirection * spriteSpeed * Time.deltaTime;

            // Respawn if sprite goes out of bounds
            Vector3 pos = currentSprite.transform.position;
            if (pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY)
            {
                SpawnAndMoveSprite();
            }
        }
    }

    void SpawnAndMoveSprite()
    {
        // Destroy old sprite if any
        if (currentSprite != null)
        {
            Destroy(currentSprite);
        }

        // Spawn new sprite at random position
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        currentSprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

        // Pick a random direction (up/down/left/right)
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: moveDirection = Vector3.up; break;
            case 1: moveDirection = Vector3.down; break;
            case 2: moveDirection = Vector3.left; break;
            case 3: moveDirection = Vector3.right; break;
        }
    }
}
