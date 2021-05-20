using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour
{
    private Text countdownText; // 倒计时文本
    private DateTime nextRewardTime; // 下一次领取奖励的时间

    public  void StartCountDown(Text countdownText, DateTime nextRewardTime)
    {

        this.countdownText = countdownText;
        this.nextRewardTime = nextRewardTime;
        StartCoroutine(CountDownTime());
    }

    
    // 计算领取倒计时，更新文本
     IEnumerator CountDownTime()
    {
        TimeSpan duration = nextRewardTime.Subtract(DateTime.Now);
        while (duration.TotalSeconds > 1)
        {
            countdownText.text =
                $"Ends in:{duration.Days:00}d {duration.Hours:00}h {duration.Minutes:00}m {duration.Seconds:00}s";
            duration = nextRewardTime.Subtract(DateTime.Now);
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Ends in:00d 00h 00m 00s";
    }
}

