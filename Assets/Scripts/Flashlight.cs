using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    public Text batteryText;
    public Image batteryImage;
    public Sprite[] batteryImages;

    private float batteryPercentage = 100f;
    private float duration = 300f;
    private int batteryLevel = 5;

    private IEnumerator drainCoroutine;

    public GameObject Spotlight;
    public KeyCode toggleKey = KeyCode.F;
    private bool isActive = false;

    private void Start()
    {
        ResetBattery();

        drainCoroutine = DrainBattery();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isActive = !isActive;
            if (isActive)
            {
                Spotlight.SetActive(isActive);

                StartCoroutine(drainCoroutine);
            }
            else
            {
                Spotlight.SetActive(isActive);

                StopCoroutine(drainCoroutine);
            }
        }
    }

    public void ResetBattery()
    {
        // if (drainCoroutine != null)
        // {
        //     StopCoroutine(drainCoroutine);
        // }

        batteryPercentage = 100f;
        batteryLevel = 5;
        batteryImage.sprite = batteryImages[batteryLevel];
        batteryText.text = "100%";

        // drainCoroutine = StartCoroutine(DrainBattery());
    }

    private IEnumerator DrainBattery()
    {
        float elapsed = 0f;

        while (batteryPercentage > 0)
        {
            elapsed += Time.deltaTime;
            batteryPercentage = Mathf.Lerp(100f, 0f, elapsed / duration);
            batteryText.text = Mathf.Clamp(Mathf.CeilToInt(batteryPercentage), 0, 100) + "%";
            UpdateBatteryLevel();
            yield return null;
        }

        batteryText.text = "0%";
    }

    private void UpdateBatteryLevel()
    {
        if (batteryPercentage > 80f)
            batteryLevel = 5;
        else if (batteryPercentage > 60f)
            batteryLevel = 4;
        else if (batteryPercentage > 40f)
            batteryLevel = 3;
        else if (batteryPercentage > 20f)
            batteryLevel = 2;
        else if (batteryPercentage > 0f)
            batteryLevel = 1;
        else
            batteryLevel = 0;

        batteryImage.sprite = batteryImages[batteryLevel];
    }
}