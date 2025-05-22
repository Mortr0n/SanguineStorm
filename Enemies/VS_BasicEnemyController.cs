using UnityEngine;

public class VS_BasicEnemyController : MonoBehaviour
{
    Transform playerTransform;
    Vector3 targetOffset = Vector2.zero;
    [SerializeField] float enemySpeed = 2.5f;
    [SerializeField] float despawnDistance = 15.0f;
        
    [SerializeField] protected GameObject lootPrefab;

    private float engageDelay = 0.5f;
    private float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
        playerTransform = VS_PlayerController.instance.transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = VS_PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = VS_PlayerController.instance.transform.position;
        float distance = Vector2.Distance(transform.position, playerPos);

        if (distance > despawnDistance)
        {
            Vector2 playerDirection = VS_PlayerController.instance.GetMovementDirection();
            if (playerDirection == Vector2.zero)
                playerDirection = Vector2.right; // default fallback direction

            Vector3 newPosition = playerPos + (Vector3)(playerDirection.normalized * 10f);
            transform.position = newPosition;
        }
        // current pooled setactive to false for pooled objects
        //if (Vector2.Distance(transform.position, playerTransform.position) > despawnDistance) gameObject.SetActive(false);

        // original destroy code
        //if (Vector2.Distance(transform.position, playerTransform.position) > despawnDistance) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("VS_BasicEnemyController: playerTransform is null");
            return;
        }
        if (Time.time < spawnTime + engageDelay) return;

        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position + targetOffset, enemySpeed *  Time.fixedDeltaTime);
    }

    public void TriggerDeath()
    {
        //if(lootPrefab != null) Instantiate(lootPrefab, transform.position, Quaternion.identity);
        DropLoot();
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CombatReceiver2D>())
        {
            if (collision.gameObject.GetComponent<CombatReceiver2D>().GetFactionID() == 1)
            {
                targetOffset = (transform.position - collision.transform.position);
            }
        }
    }


    protected virtual void DropLoot()
    {
        if (lootPrefab != null) Instantiate(lootPrefab, transform.position, Quaternion.identity);
    }
}
