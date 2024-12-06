using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public string ItemName;
    public BinType MatchingBinType;

    public string GetItemNameThai()
    {
        switch (ItemName)
        {
            case "Apple":
                return "แอปเปิ้ล";
            case "Banana Peel":
                return "เปลือกกล้วย";
            case "Battery":
                return "แบตเตอรี่";
            case "Candy":
                return "เปลือกลูกอม";
            case "Fabric":
                return "เศษผ้า";
            case "Fish Bone":
                return "ก้างปลา";
            case "Food Waste":
                return "เศษอาหาร";
            case "Glass":
                return "แก้วน้ำ";
            case "Leaf":
                return "ดอกไม้";
            case "Lipstick":
                return "เครื่องสำอางหมดอายุ";
            case "Mask":
                return "หน้ากากอนามัย";
            case "Milk Carton":
                return "กล่องนม";
            case "Noodle":
                return "ซองมาม่า";
            case "Paper":
                return "กระดาษ";
            case "Pesticide Canister":
                return "ยาฆ่าแมลง";
            case "Plaster":
                return "พลาสเตอร์ยา";
            case "Plastic Bottle":
                return "ขวดพลาสติก";
            case "Snack":
                return "ซองขนม";
            case "Soda Can":
                return "กระป๋องน้ำอัดลม";
            case "Straw":
                return "หลอดดูดน้ำ";
            default:
                return "ไม่ทราบชื่อรายการ";
        }
    }

    public string GetMatchingBinTypeThai()
    {
        switch (MatchingBinType)
        {
            case BinType.General:
                return "ถังขยะทั่วไป";
            case BinType.Recycle:
                return "ถังขยะรีไซเคิล";
            case BinType.Organic:
                return "ถังขยะอินทรีย์";
            case BinType.Hazardous:
                return "ถังขยะอันตราย";
            default:
                return "ถังขยะ";

        }
    }
}
