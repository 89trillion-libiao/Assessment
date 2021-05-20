using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemUIShow
{
    public Image imageRank;
    public Image imageAvatarBorder;
    public Image imageTrophy;
    public Image imagePanel; // 按顺序：金 银 铜 普通

    public Text textRank;
    public Text nickName;
    public Text textRanking;
}

// Item 的UI配置数据
public class ItemUIConfig
{
    public static Sprite GetImageRank(int index)
    {
        return (Sprite) Resources.Load<Sprite>("Sprites/rank_" + (index));
    }
    
    public static Sprite GetImageAvatarBorder(int index)
    {
        return (Sprite) Resources.Load<Sprite>("Sprites/avatar_" + (index));
    }
    
    public static Sprite GetImageTrophy(int index)
    {
        return (Sprite) Resources.Load<Sprite>("Sprites/Rank/arenaBadge_" + (index));
    }
    public static Sprite GetImagePanel(int index)
    {
        return (Sprite) Resources.Load<Sprite>("Sprites/rank list_" + (index));
    }
}

public class RankItem : RecyclingListViewItem
{
    public Toast toast;
    
    [SerializeField] private ItemUIShow itemUIShow = new ItemUIShow();
    
    private RankInfomation rankInfomation;

    // 记录信息、更新UI
    public void Initialize(RankInfomation rankInfomation, bool isChangeBackground)
    {
        // 记录信息
        this.rankInfomation = rankInfomation;
        
        // 更新UI
        if (isChangeBackground)
        {
            if (rankInfomation.rank <= 3)
            {
                itemUIShow.imagePanel.sprite = ItemUIConfig.GetImagePanel(rankInfomation.rank);
            }
            else
            {
                itemUIShow.imagePanel.sprite = ItemUIConfig.GetImagePanel(4);
            }
        }
        InitializeUI(rankInfomation);
    }

    // 根据信息更新UI 
    private void InitializeUI(RankInfomation rankInfomation)
    {
        itemUIShow.nickName.text = rankInfomation.nickName;

        itemUIShow.imageTrophy.sprite = ItemUIConfig.GetImageTrophy(1 + rankInfomation.trophy / 1000);
        itemUIShow.textRanking.text = rankInfomation.trophy.ToString();

        if (rankInfomation.rank <= 3)
        {
            itemUIShow.textRank.gameObject.SetActive(false);
            itemUIShow.imageRank.gameObject.SetActive(true);
            itemUIShow.imageRank.sprite = ItemUIConfig.GetImageRank(rankInfomation.rank );
            itemUIShow.imageRank.SetNativeSize();
            itemUIShow.imageAvatarBorder.sprite = ItemUIConfig.GetImageAvatarBorder(rankInfomation.rank );
        }
        else
        {
            itemUIShow.imageRank.gameObject.SetActive(false);
            itemUIShow.textRank.gameObject.SetActive(true);
            itemUIShow.textRank.text = rankInfomation.rank.ToString();
            itemUIShow.imageAvatarBorder.sprite = ItemUIConfig.GetImageAvatarBorder(4);
        }
    }
    
    
    // 点击事件
    public void ShowToast()
    {
        toast.Show($"User：{rankInfomation.uid}\nRank：{rankInfomation.rank}");
    }
}