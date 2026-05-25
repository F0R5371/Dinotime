using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float maxHeight;
    public float moveSpeed;

    private bool moving = false;
    private bool down = true;
    private static float t = 0.0f;
    private Transform platform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platform = transform.Find("Platform");

        platform.localPosition = new Vector2(0, maxHeight);

        StartCoroutine("StartCycle");
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (down)
            {
                t += Time.deltaTime * moveSpeed;

                platform.localPosition = new Vector2(0, Mathf.Lerp(maxHeight, -maxHeight, t));

                if (t >= 1)
                {
                    StartCoroutine("StartCycle");
                }
            }
            else
            {
                t -= Time.deltaTime * moveSpeed;

                platform.localPosition = new Vector2(0, Mathf.Lerp(maxHeight, -maxHeight, t));

                if (t < 0)
                {
                    StartCoroutine("StartCycle");
                }
            }
        }
    }

    private IEnumerator StartCycle()
    {
        moving = false;

        yield return new WaitForSeconds(2);

        moving = true;

        down = !down;
    }
}
