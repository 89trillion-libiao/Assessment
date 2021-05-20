using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    
    // 每日精选的整个界面
    [SerializeField]
    private GameObject ChampionPage;
    // 管理是否激活商店界面
    public void ActivePage(bool isActive)
    {
        
        ChampionPage.SetActive(isActive);
    }
}
