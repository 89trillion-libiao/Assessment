using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Function2._02.Scripts
{
    public class Toolkit : MonoBehaviour
    {
    
        //  用动画方式更新文本信息
        public static void FormatTextNumber(Text informationText, float start, float end)
        {
            DOTween.To(value => { informationText.text = FormatNumber(value); }, start, end, 1.5f);
        }

        // 将数字转为字符串；eg: 1200 -> 1.2 K
        private static string FormatNumber(float number)
        {
            if (number <= 999)
            {
                return $"{(int) number}";
            }
        
            // 千 兆 十亿 ....
            string suffix = "KMBTQ"; 
            int count = 0;
            while (number > 999)
            {
                number = number / 1000f;
                count++;
            }

            return $"{number:####.#} " + suffix[count - 1];
        }
        
        

    }
}
