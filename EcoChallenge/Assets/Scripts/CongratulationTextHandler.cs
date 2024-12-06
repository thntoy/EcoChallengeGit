using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CongratulationTextHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _congratulationText;
    [SerializeField] private int _level;
    public void ShowCongratulationText()
    {
        int score = ScoreManager.Instance.Score;

        switch (_level)
        {
            case 1:
                if (score < 6)
                {
                    _congratulationText.text = "อย่าเพิ่งยอมแพ้! “ไปลองใหม่ที่ด่านถัดไป” ";
                }
                else if (score >= 6 && score < 11)
                {
                    _congratulationText.text = "อีกนิดเดียว! “ลุยต่อเพื่อเป็นฮีโร่กู้โลก!";
                }
                else if (score >= 11 && score < 16)
                {
                    _congratulationText.text = "ดีมาก! “ด่านหน้าคุณทำได้ดีกว่าเดิมแน่นอน”";
                }
                else if (score >= 16)
                {
                    _congratulationText.text = "ยอดเยี่ยม! “โลกสะอาดขึ้นเพราะคุณ!”";
                }             
                break;
            case 2:
                if (score < 25)
                {
                    _congratulationText.text = "อย่าเพิ่งยอมแพ้! “ไปลองใหม่ที่ด่านถัดไป” ";
                }
                else if (score >= 25 && score < 30)
                {
                    _congratulationText.text = "อีกนิดเดียว! “ลุยต่อเพื่อเป็นฮีโร่กู้โลก!";
                }
                else if (score >= 30 && score < 35)
                {
                    _congratulationText.text = "ดีมาก! “ด่านหน้าคุณทำได้ดีกว่าเดิมแน่นอน”";
                }
                else if (score >= 35)
                {
                    _congratulationText.text = "ยอดเยี่ยม! “โลกสะอาดขึ้นเพราะคุณ!”";
                }             
                break;
            case 3:
                if (score < 15)
                {
                    _congratulationText.text = "\"ไม่เป็นไร ลองใหม่อีกครั้ง โลกยังรอให้คุณช่วยอยู่!\" ทุกความพยายามนับเป็นก้าวสำคัญลองอีกครั้งและ พัฒนาตัวเองให้ดีขึ้นไปเรื่อยๆ";
                }
                else if (score >= 15 && score < 25)
                {
                    _congratulationText.text = "\"เริ่มต้นได้ดี!\" พยายามอีกนิดแล้วคุณจะพบว่าการช่วยโลกสนุกกว่าที่คิด";
                }
                else if (score >= 25 && score < 35)
                {
                    _congratulationText.text = "\"ดีมาก! คุณทำได้ดีแล้ว แต่ยังมีโอกาสปรับปรุงอีกนะ\" ลองดูอีกครั้งและท้าทายตัวเองเพื่อทำให้ดีกว่าเดิม!";
                }
                else if (score >= 35 && score < 45)
                {
                    _congratulationText.text = "\"เก่งมาก! คุณใกล้จะเป็นฮีโร่เต็มตัวแล้ว!\" อีกนิดเดียวก็จะได้ตำแหน่งสุดยอดนักรักษ์โลกสู้ต่อไป!";
                }             
                else if (score >= 45)
                {
                    _congratulationText.text = "ยอดเยี่ยม! ปลดล็อก \"ฮีโร่รักษ์โลก!\"";
                }
                break;
            case 4:
                _congratulationText.text = $"สุดยอด! {score} ดาวแห่งการรักษ์โลก\nผลลัพธ์แสดงให้เห็นถึงพัฒนาการของคุณ";
                break;
        }
    }
}
