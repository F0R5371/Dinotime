using UnityEngine;

public class DrillRange : MonoBehaviour
{
    private DrillMovement drill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drill = transform.GetComponentInParent<DrillMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            drill.SwitchToDrill();
        }
    }
}
