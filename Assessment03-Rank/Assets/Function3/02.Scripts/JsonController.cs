using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

// 排行榜信息
public class RankInfomation
{
    public int rank;
    public string uid;
    public string nickName;
    public int avatar;
    public int trophy;
    public string thirdAvatar;
    public int onlineStatus;
    public int role;
    public string abb;
}

// 整个页面的信息
public class ItemInfomation
{
    public int countDown;
    public List<RankInfomation> rankList;
    public int seasonID;
    public int selfRank;
}


public class JsonController
{
    public static ItemInfomation LoadItemInfomation(string path)
    {
        TextAsset textAsset = (TextAsset) Resources.Load(path);

        // 新建并初始化
        ItemInfomation itemInfomation = new ItemInfomation();
        itemInfomation.rankList = new List<RankInfomation>();
        itemInfomation.countDown = JSONNode.Parse(textAsset.text)["countDown"];
        itemInfomation.seasonID = JSONNode.Parse(textAsset.text)["seasonID"];
        itemInfomation.selfRank = JSONNode.Parse(textAsset.text)["selfRank"];


        // 添加list内容
        RankInfomation rankInfomation;
        foreach (JSONNode rank in JSONNode.Parse(textAsset.text)["list"])
        {
            rankInfomation = new RankInfomation();
            rankInfomation.uid = rank["uid"];
            rankInfomation.nickName = rank["nickName"];
            rankInfomation.avatar = rank["avatar"];
            rankInfomation.trophy = rank["trophy"];
            rankInfomation.thirdAvatar = rank["thirdAvatar"];
            rankInfomation.role = rank["role"];
            rankInfomation.abb = rank["abb"];
            itemInfomation.rankList.Add(rankInfomation);
        }

        // 给排行榜排序
        itemInfomation.rankList.Sort((x, y) =>
        {
            if (x.trophy > y.trophy)
            {
                return -1;
            }
            if (x.trophy == y.trophy)
            {
                return 0;
            }
            return 1;
        });

        // 排完顺序，标号
        for (int i = 0; i < itemInfomation.rankList.Count; ++i)
        {
            itemInfomation.rankList[i].rank = i + 1;
        }
        return itemInfomation;
    }
}