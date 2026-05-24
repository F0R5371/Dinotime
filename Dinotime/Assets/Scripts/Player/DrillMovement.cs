using UnityEngine;

public class DrillMovement : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public Vector2 mineBoxSize = new Vector2(1, 1);

    [Header("References")]
    public Transform drillTip;

    private bool playerInDrill = false;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private DrillRange range;
    private PlayerManager playerManager;

    public void StartCamera()
    {
        transform.Find("FollowCamera").gameObject.SetActive(true);
        playerInDrill = true;
    }

    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        transform.Find("FollowCamera").gameObject.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        range = transform.GetComponentInChildren<DrillRange>();
        rb.gravityScale = 1f;

        enabled = false;
    }

    void Update()
    {
        if (!playerInDrill) return;

        ExitDrill();
        HandleMovementAndMining();
    }

    private void UpdateDrillVisuals(float x)
    {
        if (x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            float rotationMultiplier = (transform.localScale.x < 0) ? -1 : 1;
            transform.rotation = Quaternion.Euler(0, 0, -90 * rotationMultiplier);
        }
    }

    private void HandleMovementAndMining()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);

        UpdateDrillVisuals(x);

        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                TryMine();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            TryMine();
        }
    }

    private void TryMine()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(drillTip.position, mineBoxSize, transform.eulerAngles.z, LayerMask.GetMask("Mineable"));

        foreach (Collider2D hitCollider in hits)
        {
            if (hitCollider.CompareTag("Fossil"))
            {
                Debug.Log("FOSSIL EVENT TRIGGERED! You found some old bones.");
            }

            Destroy(hitCollider.gameObject);
        }
    }

    private void ExitDrill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerManager.SwitchCameras("player");
            rb.linearVelocity = Vector2.zero;
            transform.Find("FollowCamera").gameObject.SetActive(false);

            playerInDrill = false;
            range.canActivate = false;
            enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.red;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(drillTip.position, transform.rotation, Vector3.one);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, mineBoxSize);
    }
}