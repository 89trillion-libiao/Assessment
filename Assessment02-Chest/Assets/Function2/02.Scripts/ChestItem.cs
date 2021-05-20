using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Function2._02.Scripts
{
    public class ChestItem : MonoBehaviour
    {
        public Text diamondsCostText; // 钻石花费按钮文本
        public ShopPage shopPage; // 主代码
        public GameObject Effect;

        private int purchasedCount = 1; //  购买金币次数
        private int diamondsCost = 1; //  花费钻石的数量
        private int coinsNumber = 5; //  获取金币的数量

        public Animator chestAnimator;

        public int TimeToClose = 5;
        [SerializeField] private int timeWaitingClose = 0;

        // 更新钻石花费、以及金币获取信息
        public void UpdateInformation(int purchasedCount)
        {
            // 数量更新算法为：钻石花费数等于购买次数；金币获取数等于购买次数 * 5
            this.purchasedCount = purchasedCount;
            this.diamondsCost = purchasedCount;
            this.coinsNumber = purchasedCount * 5;

            // 更新金币花费的数字文本
            diamondsCostText.text = this.diamondsCost.ToString();
        }

        // 响应鼠标点击事件，购买金币
        public void Click()
        {
            shopPage.GetCoins(purchasedCount, diamondsCost, coinsNumber);
            UpdateInformation(purchasedCount + 1);
            BoxOpen(true);
        }

        // 自动关闭宝箱
        IEnumerator BoxAutoClose(float yieldTime = 0.1f)
        {
            // 如果已经存在等待关闭协程，不再重复开启
            if (timeWaitingClose > 0)
            {
                timeWaitingClose = TimeToClose;
                yield break;
            }

            timeWaitingClose = TimeToClose;
            while (--timeWaitingClose > 0)
            {
                yield return new WaitForSeconds(yieldTime);
            }

            BoxOpen(false);
        }

        // 打开宝箱
        public void BoxOpen(bool isOpenBox)
        {
            chestAnimator.SetBool("box_open_2", isOpenBox);
            chestAnimator.SetBool("box_close_1", !isOpenBox);
            Effect.SetActive(isOpenBox);

            // 如果是打开操作，则自动关闭
            if (isOpenBox)
            {
                StartCoroutine(BoxAutoClose(1));
            }
        }
    }
}