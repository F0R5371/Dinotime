using Unity.VisualScripting;
using UnityEngine;

public class DrillMovement : MonoBehaviour
{
    public float speed;

    private float headingAngle = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        Debug.DrawRay(transform.position, headingDir * 1, Color.red);
    }
}
