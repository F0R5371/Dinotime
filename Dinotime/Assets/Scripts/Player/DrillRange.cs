using UnityEngine;

public class DrillRange : MonoBehaviour
{
    internal bool canActivate = true;

    private PlayerManager playerManager;
    private DrillMovement drill;

    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canActivate)
            return;

        if (collision.gameObject.tag == "Player")
        {
            playerManager.SwitchCameras("drill");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!canActivate)
            {
                canActivate = true;
            }
        }
    }
}
