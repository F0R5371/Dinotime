using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Transform canvas;

    public void UpdateHUD(int value, string type)
    {
        TextMeshProUGUI text = canvas.Find(type).GetComponent<TextMeshProUGUI>();

        text.text = $"{type}: {value}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;

        UpdateHUD(0, "Fossil");
        UpdateHUD(25, "Gas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
