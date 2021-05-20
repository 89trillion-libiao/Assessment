using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Function2._02.Scripts
{
    public class ShopPage : MonoBehaviour
    {
        // 现在的金币总数
        public int totalCoinsNumber = 10;

        // 现在的钻石总数
        public int totalDiamondNumber = 100;

        // 动画中的显示金币的最大数量
        public int MaxCreatNumber = 15;


        // 显示金币总数的文本
        [SerializeField] private Text coinsInformationText;
        // 显示钻石总数的文本
        [SerializeField] private Text diamondInformationText;
        // 宝箱
        [SerializeField] private ChestItem chestItem;
        // 金币动画的控制器
        [SerializeField] private CoinsAnimation coinsAnimation;

        void Start()
        {
            chestItem.UpdateInformation(3);
            //  初始化文本信息
            coinsInformationText.text = totalCoinsNumber.ToString();
            diamondInformationText.text = totalDiamondNumber.ToString();
        }


        // 领取金币，参数分别为：领取次数，钻石花费数量，领取金币数
        public void GetCoins(int purchaseCount, int diamondsCost, int coinsNumber)
        {
            // 判断钻石是否足够
            if (totalDiamondNumber - diamondsCost < 0)
            {
                return;
            }

            //  更新文本
            Toolkit.FormatTextNumber(coinsInformationText, totalCoinsNumber, totalCoinsNumber + coinsNumber);
            Toolkit.FormatTextNumber(diamondInformationText, totalDiamondNumber, totalDiamondNumber - diamondsCost);

            //  更新钻石数量，金币数量
            totalCoinsNumber += coinsNumber;
            totalDiamondNumber -= diamondsCost;

            //  设定动画的金币数量
            purchaseCount = purchaseCount > MaxCreatNumber ? MaxCreatNumber : purchaseCount;

            coinsAnimation.PlayCoinsAnimation(purchaseCount);
        }

        // 增加钻石的数量
        public void AddDiamondNumber(int number = 100)
        {
            Toolkit.FormatTextNumber(diamondInformationText, totalDiamondNumber, totalDiamondNumber + number);
            totalDiamondNumber += number;
        }
    }
}