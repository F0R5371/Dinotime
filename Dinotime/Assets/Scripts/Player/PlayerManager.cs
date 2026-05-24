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
            player.GetComponent<PlayerMovement>().enabled = false;
            player.SetActive(false);
        }
        else if (nextCam == "player")
        {
            player.gameObject.SetActive(true);

            Transform drill = GameObject.Find("Drill").transform;
            player.transform.position = drill.position;

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerMovement>().StartCamera();
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
