using Unity.VisualScripting;
using UnityEngine;

public class DrillMovement : MonoBehaviour
{
    public float speed;

    private bool isMining = false;

    private float headingAngle = 0;
    private Vector2 inputDir;

    private Rigidbody2D rb;
    private SpriteRenderer sp;

    public void StartCamera()
    {
        transform.Find("FollowCamera").gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Find("FollowCamera").gameObject.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
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
}
