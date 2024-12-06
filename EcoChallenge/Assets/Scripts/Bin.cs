using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{ 
    public BinType Type;
}
public enum BinType
{
    General,
    Recycle,
    Organic,
    Hazardous
}

