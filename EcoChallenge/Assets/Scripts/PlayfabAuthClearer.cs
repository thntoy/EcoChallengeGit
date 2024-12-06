using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabAuthClearer : MonoBehaviour
{
    public void ClearAuth()
    {
        PlayfabManager.Instance.Logout();
    }
}
