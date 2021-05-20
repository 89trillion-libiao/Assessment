using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPage : MonoBehaviour
{

    [SerializeField] private Text textSeason; // 赛季文本
    [SerializeField] private RankItem rankItem; // 赛季文本
    [SerializeField] private RecyclingListView scrollList;  // 控件复用控制代码
    [SerializeField] private TimeCountDown timeCountDown;  // 倒计时控件
    [SerializeField] private Text countdownText; // 倒计时文本
    private ItemInfomation itemInfomation;

    void Start()
    {
        // 加载数据，初始化UI信息
        itemInfomation = JsonController.LoadItemInfomation("ranklist");
        InitTitle();
        
        // 列表item更新回调
        scrollList.ItemCallback = UpdateItem;// 设置数据，此时列表会执行更新
        scrollList.RowCount = itemInfomation.rankList.Count;

    }

    // 初始化文本信息
    void InitTitle()
    {
        textSeason.text = $"Season {itemInfomation.seasonID} Ranking";

        // 设置下一赛季的时间倒计时
        DateTime nextRewardTime = DateTime.Now.AddSeconds(itemInfomation.countDown);
        timeCountDown.StartCountDown(countdownText, nextRewardTime);

        // 设置自己的信息
        rankItem.Initialize(itemInfomation.rankList[itemInfomation.selfRank - 1], false);
    }

    // 回调进行的操作
    private void UpdateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as RankItem;
        child.gameObject.SetActive(true);
        Vector3 position = child.gameObject.transform.position;
        child.gameObject.transform.position = new Vector3(0,position.y,position.z);
        child.Initialize(itemInfomation.rankList[rowIndex], true);
        child.RectTransform.anchoredPosition = new Vector2(0, child.RectTransform.anchoredPosition.y);
    }



}