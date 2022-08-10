using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI BlinkText;
    public TextMeshProUGUI TimeText;
    private PlayerController playerScript;

    private float messageDisplayTimestamp;
    private float oldHp;
    private float currentHp;
    private float oldLevel;
    private float currentLevel;
    internal float BlinkCooldown;
    public int secondsPassed;
    public int minutes;


    // Start is called before the first frame update
    void Start()
    {
        TimeText.text = $"Time : 00:00";
        InvokeRepeating("InGameTimer", 1, 1);
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        oldLevel = 1;
        messageDisplayTimestamp = Time.time + 1;
        BlinkCooldown = 0;
        BlinkText.text = $"Blink: READY";
        minutes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            currentHp = playerScript.hp;
            if (currentHp != oldHp)
            {
                hpText.text = $"Health: {currentHp}";
                oldHp = currentHp;
            }
        }
        catch (Exception e)
        {
            oldHp = currentHp;
        }

        currentLevel = playerScript.level;
        if (currentLevel != oldLevel)
        {
            levelText.text = $"Level: {currentLevel}";
            oldLevel = currentLevel;
        }

        if (BlinkCooldown == 4)
        {
            StartCoroutine(DisplayBlinkCD());
        }

    }

    [CanBeNull]
    IEnumerator DisplayBlinkCD()
    {
        if (BlinkCooldown == 0)
        {
            BlinkText.text = $"Blink: READY";
        }
        else
        {
            BlinkText.text = $"Blink: {BlinkCooldown}";
            BlinkCooldown -= 1;
            yield return new WaitForSeconds(1);
            StartCoroutine(DisplayBlinkCD());
        }

    }

    void InGameTimer()
    {
        if (!playerScript.isDead)
        {
            secondsPassed += 1;
            int seconds;
            while (secondsPassed > 60)
            {
                minutes += 1;
                secondsPassed -= 60;
            }

            seconds = secondsPassed;
            string minutesString = Convert.ToString(minutes);
            string secondsString = Convert.ToString(seconds);
            if (minutesString.Length < 2)
            {
                minutesString = "0" + minutesString;
            }

            if (secondsString.Length < 2)
            {
                secondsString = "0" + secondsString;
            }

            TimeText.text = $"Time: {minutesString}:{secondsString}";
        }
    }


}
