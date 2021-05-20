using DG.Tweening;
using UnityEngine;

namespace Function2._02.Scripts
{
    public class CoinsAnimation : MonoBehaviour
    {
        // 刚开始延时金币生产的间隔时间
        [SerializeField] private float starIintervalTime = 0.5f;
        // 每两个金币生产的间隔时间
        [SerializeField] private float intervalTime = 0.1f;

        // 大小变化的参数
        [SerializeField] private float[] scale = new float[] {1, 100};

        // 分散半径
        [SerializeField] private float radius = 10f;

        // 分散移动时间
        [SerializeField] private float dispersionTime = 0.5f;

        // 大小变化的时间
        [SerializeField] private float doScaleTime = 0.5f;

        // 移动的时间
        [SerializeField] private float doMoveTime = 1f;

        //  金币动画的移动目标位置
        [SerializeField] private RectTransform targetTransform;


        // 播放一定数量的金币动画
        public void PlayCoinsAnimation(int coinsNumber)
        {
            for (int i = 0; i < coinsNumber; ++i)
            {
                Sequence sequence = DOTween.Sequence(); 
                // 进行动画
                sequence.AppendInterval(starIintervalTime + intervalTime * i);
                sequence.OnComplete(PlayAnimation);
            }
        }


        // 单个金币动画的播放
        void PlayAnimation()
        {
            // 从对象池获取对象
            GameObject coin = ObjectsPool.Instance.GetInstance();
            // 重置位置，大小
            RectTransform instanceRectTransform;
            instanceRectTransform = coin.GetComponent<RectTransform>();
            instanceRectTransform.position = Vector3.zero;
            instanceRectTransform.localScale = Vector3.one * scale[0];
            instanceRectTransform.parent = this.transform;

            // 进行动画
            Sequence sequence = DOTween.Sequence();

            // 分散
            var position = targetTransform.position;
            Vector3 targetPostion = new Vector3(
                Random.insideUnitCircle.x * radius, Random.insideUnitCircle.y * radius, position.z);
            sequence.Insert(0, coin.transform.DOMove(targetPostion, dispersionTime));

            // 变大
            sequence.Insert(0, coin.transform.DOScale(scale[1], doScaleTime));

            // 移动
            sequence.Append(coin.transform.DOMove(position, doMoveTime));

            // 回收
            sequence.OnComplete(() => { ObjectsPool.Instance.ReturnInstance(coin); });
        }
    }
}