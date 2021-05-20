using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int score;
    public int coins;

    // UI 信息
    [SerializeField]private Text scoreRequestText;
    [SerializeField]private Text coinsGetText;
    
    // 领取信息
    [SerializeField]private GameObject collectPanel;
    [SerializeField]private GameObject receiveButton;
    
    // 主界面
    [SerializeField]private ChampionPage championPage;

    // 初始化，更新自己的UI信息
    public void Initialize(ScoreInformation scoreInformation)
    {
        score = scoreInformation.score;
        coins = scoreInformation.coins;
        scoreRequestText.text = score.ToString() + "分";
        coinsGetText.text = coins.ToString()+ "金币";
    }
    
    
    
    // 鼠标点击，获取奖励
    public void ReceiveReward()
    {
        if (championPage.IsReceive(score, coins))
        {
            receiveButton.SetActive(false);
            collectPanel.SetActive(true);
        }
    }
}
