using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;

    public void SwitchCameras(string nextCam)
    {
        if (nextCam == "drill")
        {
            Transform drill = GameObject.Find("Drill").transform;
            DrillMovement drillMove = drill.GetComponent<DrillMovement>();

            drillMove.enabled = true;
            drillMove.StartCamera();

            player.GetComponent<PlayerMovement>().StopCamera();
            player.SetActive(false);
        }
        else
        {
            
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
