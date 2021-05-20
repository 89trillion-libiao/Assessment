using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
// 不同数字代表的意义
public enum NumberType
{
    score = 1,
    coins = 2,
    season = 3
}

// 存储分数和金币获取量的对应关系
public class ScoreInformation
{
    public int score;
    public int coins;
}

public class ChampionPage : MonoBehaviour
{
    public int coinsNumber = 0;
    public int scoreNumber = 3800;
    public int seasonNumber = 1;
    public int LowestLevel = 4000;  // 展示段位的最低要求
    public int MaxScoreNumber = 6000;  // 得分的最高限制

    [SerializeField] private Text coinsText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text seasonText;
    [SerializeField] private Text TextRankInformation;
    [SerializeField] private Item item;
    [SerializeField] private Transform itemParent;
    private List<ScoreInformation> scoreInformationList = new List<ScoreInformation>();

    private void Start()
    {
        // 初始化得分与金币获取量的信息
        InitializeScoreInformation();
        // 生成界面
        CreatItem(scoreInformationList);
        // 初始化自己的金币,得分,赛季信息
        updateUI();
    }

    // 根据特定算法生成得分与金币获取量的信息
    void InitializeScoreInformation()
    {
        for (int i = 0; i < 11; i++)
        {
            // 金币增量为100；得分增量为200
            ScoreInformation scoreInformation = new ScoreInformation();
            scoreInformation.score = LowestLevel + i * 200;
            scoreInformation.coins = 100;
            // 跳过整千
            if (scoreInformation.score % 1000 == 0)
            {
                continue;
            }
            scoreInformationList.Add(scoreInformation);
        }
    }

    // 根据得分与金币获取量的信息，数据生成对应的Item
    void CreatItem(List<ScoreInformation> scoreInformationList)
    {
            
        // 生成预制件
        GameObject cardPrefeb;
        foreach (ScoreInformation scoreInformation in scoreInformationList)
        {
            // 实例化预制件，放到特定位置；
            cardPrefeb = Instantiate(item.gameObject, itemParent);
            cardPrefeb.GetComponent<Item>().Initialize(scoreInformation);
            cardPrefeb.SetActive(true);
        }

    }
    
    // 更新用户的UI信息；如：金币,得分,赛季信息
    void updateUI()
    {
        seasonText.text = String.Format($"第{seasonNumber}赛季");
        coinsText.text = coinsNumber.ToString();
        scoreText.text = scoreNumber.ToString();
        // 更新段位信息
        UpdateRankInformation();
    }

    // 处理是否需要展示段位信息
    void UpdateRankInformation()
    {
        // 判断是否需要展示段位信息
        if (scoreNumber >= LowestLevel)
        {
            TextRankInformation.gameObject.SetActive(true);
            TextRankInformation.text = "段位：" + ((scoreNumber - LowestLevel) / 1000 + 1).ToString();
        }
        else
        {
            TextRankInformation.gameObject.SetActive(false);
        }
    }
    
    
    
    
    // 赛季更新
    public void AddSeason(int number = 1)
    {
        seasonNumber = seasonNumber + number;
        // 赛季更新的得分算法
        if (scoreNumber > LowestLevel)
        {
            scoreNumber = (scoreNumber - LowestLevel) / 2 + LowestLevel;
        }
        updateUI();

        // 重新刷新领取信息
        foreach (Transform item in itemParent)
        {
            Destroy(item.gameObject);
        }
        // 生成界面
        CreatItem(scoreInformationList);
        
    }
    
    // 更新金币数量
    public void AddCoins(int number = 100)
    {
        coinsNumber = coinsNumber + number;
        Toolkit.FormatTextNumber(coinsText, coinsNumber - number, coinsNumber);
    }
    
    // 更新分数
    public void AddScore(int number = 100)
    {
        int newScoreNumber = scoreNumber + number;
        
        // 检查新分数是否大于最大值
        if (newScoreNumber >= MaxScoreNumber)
        {
            newScoreNumber = MaxScoreNumber;
        }
        
        // 更新新分数
        Toolkit.FormatTextNumber(scoreText, scoreNumber, newScoreNumber);
        scoreNumber = newScoreNumber;

        // 处理是否需要展示段位信息
        UpdateRankInformation();
    }
    
    // 输入获取金币的要求的得分，以及获取的数量；返回这次点击是否成功
    public bool IsReceive(int scoreRequest, int coinsGet)
    {
        if (scoreNumber < scoreRequest)
        {
            return false;
        }
        AddCoins(coinsGet);
        return true;
    }
    
}