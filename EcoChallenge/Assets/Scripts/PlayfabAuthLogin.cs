using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayfabAuthLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInput;
    public void Login()
    {
        PlayfabManager.Instance.Login(_usernameInput);
    }
}
