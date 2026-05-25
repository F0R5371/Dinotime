using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public float titleSpeed;

    private bool showTitle = false;

    private static float t = 0.0f;

    private bool pauseMenu = false;

    private Transform canvas;
    private PlayerManager playerManager;

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        canvas.Find("PauseMenu").gameObject.SetActive(false);
        pauseMenu = false;
        Time.timeScale = 1;
    }

    public void ShowMessage(string str)
    {
        StartCoroutine("ShowMessageCoroutine", str);
    }

    public IEnumerator ShowMessageCoroutine(string str)
    {
        TextMeshProUGUI message = canvas.Find("Message").GetComponentInChildren<TextMeshProUGUI>();
        message.text = str;

        for (int i = 0; i < 3; i++)
        {
            message.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            message.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);

        message.text = " ";
    }

    public void UpdateHUD(int value, string type)
    {
        TextMeshProUGUI text = canvas.Find("HUD").Find(type).GetComponent<TextMeshProUGUI>();

        text.text = $"{type}: {value}";
    }

    public void ShowTitle()
    {
        StartCoroutine("ShowTitleScreen");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        UpdateHUD(0, "Fossil");
        UpdateHUD(25, "Gas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu)
            {
                canvas.Find("PauseMenu").gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                canvas.Find("PauseMenu").gameObject.SetActive(false);
                Time.timeScale = 1;
            }

            pauseMenu = !pauseMenu;
        }

        if (showTitle)
        {
            t += Time.deltaTime * titleSpeed;

            canvas.Find("Title").GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, Mathf.Lerp(0, 255, t));

            print(t);

            if (t >= 1)
            {
                showTitle = false;
            }
        }
    }

    private IEnumerator ShowTitleScreen()
    {
        yield return new WaitForSeconds(3);

        showTitle = true;

        yield return new WaitForSeconds(5);

        canvas.Find("Title").gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        canvas.Find("Instructions").gameObject.SetActive(true);

        yield return new WaitForSeconds(5);

        canvas.Find("Instructions").gameObject.SetActive(false);

        playerManager.StartGame();
    }
}
