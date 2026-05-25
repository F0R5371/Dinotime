using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public float titleSpeed;

    private bool showTitle = false;

    private static float t = 0.0f;

    private Transform canvas;
    private PlayerManager playerManager;

    public void UpdateHUD(int value, string type)
    {
        TextMeshProUGUI text = canvas.Find(type).GetComponent<TextMeshProUGUI>();

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

        playerManager.StartGame();
    }
}
