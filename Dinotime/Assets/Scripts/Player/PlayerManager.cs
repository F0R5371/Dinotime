using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int fossilCount = 0;
    private int oilCount = 25;

    private GameObject player;
    private PlayerMovement playerMovement;

    private UIManager uiManager;

    public void StartGame()
    {
        player.GetComponent<PlayerMovement>().StartCamera();

        GameObject.Find("TitleScreenCamera").gameObject.SetActive(false);

        playerMovement.StartGame();
    }

    public void CollectItem(string item)
    {
        if (item == "fossil")
        {
            fossilCount += 1;
            uiManager.UpdateHUD(fossilCount, "Fossil");
        }
        else if (item == "oil")
        {
            oilCount += 1;
            uiManager.UpdateHUD(oilCount, "Gas");
        }

        FossilUpgrades();
    }

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

    public void DrainOil(int amount)
    {
        oilCount -= amount;
        uiManager.UpdateHUD(oilCount, "Gas");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        player.transform.position = new Vector2(0, 0.5f);

        player.GetComponent<PlayerMovement>().StopCamera();
        uiManager.ShowTitle();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void FossilUpgrades()
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (fossilCount >= 10 && !playerMovement.dashAbilityActivated)
        {
            playerMovement.dashAbilityActivated = true;
            uiManager.ShowMessage("Dash Unlocked!<br>Press E To Dash");
        }
        else if (fossilCount >= 25 && !playerMovement.doubleJumpAbilityActivated)
        {
            playerMovement.doubleJumpAbilityActivated = true;
            uiManager.ShowMessage("Double Jump Unlocked!");
        }
    }
}
