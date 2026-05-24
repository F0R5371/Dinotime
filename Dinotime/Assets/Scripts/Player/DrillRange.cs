using UnityEngine;

public class DrillRange : MonoBehaviour
{
    private PlayerManager playerManager;

    private DrillMovement drill;

    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drill = transform.GetComponentInParent<DrillMovement>();
        drill.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerManager.SwitchCameras("drill");
        }
    }
}
