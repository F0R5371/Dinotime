using Unity.VisualScripting;
using UnityEngine;

public class DrillMovement : MonoBehaviour
{
    public float speed;

    private bool isMining = false;
    private bool playerInDrill = false;

    private float headingAngle = 0;
    private Vector2 inputDir;

    private Rigidbody2D rb;
    private SpriteRenderer sp;

    private DrillRange range;
    private PlayerManager playerManager;

    public void StartCamera()
    {
        transform.Find("FollowCamera").gameObject.SetActive(true);

        playerInDrill = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        transform.Find("FollowCamera").gameObject.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        range = transform.GetComponentInChildren<DrillRange>();
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        ExitDrill();
    }

    void FixedUpdate()
    {
        Move();
        View();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputDir = new Vector2(x, y);

        rb.linearVelocity = new Vector2(x * speed, 0);

        if (x > 0)
        {
            sp.flipX = false;
        }
        else
        {
            sp.flipX = true;
        }
    }

    private void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            headingAngle += 90;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, headingAngle));
        }
    }

    private void View()
    {
        Vector2 headingDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * headingAngle), Mathf.Sin(Mathf.Deg2Rad * headingAngle));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, headingDir, 1f, LayerMask.NameToLayer("Mineable"));
        Debug.DrawRay(transform.position, headingDir * 1, Color.red);

        if (hit)
        {
            if (inputDir == headingDir)
            {
                isMining = true;
            }
        }
    }

    private void ExitDrill()
    {
        if (!playerInDrill)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerManager.SwitchCameras("player");

            rb.linearVelocity = Vector2.zero;

            transform.Find("FollowCamera").gameObject.SetActive(false);
            enabled = false;

            range.canActivate = false;
        }
    }
}
