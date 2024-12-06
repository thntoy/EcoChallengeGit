using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public static PopupHandler Instance;

    [SerializeField] private GameObject _keyPopupPrefab;
    [SerializeField] private GameObject _textPopupPrefab;
    [SerializeField] private GameObject _correctPopupPrefab;
    [SerializeField] private GameObject _wrongPopupPrefab;
    [SerializeField] private Transform _popupParent;
    private GameObject _currentPopup;
    private Vector3 _targetPosition;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_currentPopup != null)
        {
            _currentPopup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(_targetPosition);
        }
    }

    public GameObject SpawnPopupKey(string text, Vector3 position, int fontSize)
    {
        GameObject popup = Instantiate(_keyPopupPrefab, _popupParent);
        popup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(position);
        popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = fontSize;

        _targetPosition = position;
        _currentPopup = popup;

        return popup;
    }

    public GameObject SpawnPopupText(string text, Vector3 position, int fontSize)
    {
        GameObject popup = Instantiate(_textPopupPrefab, _popupParent);
        popup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(position);
        popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = fontSize;

        _targetPosition = position;
        _currentPopup = popup;

        Destroy(popup, 1.5f);
        return popup;
    }

    public GameObject SpawnCorrectPopup(Vector3 position, float delay = 1, bool isWorldPosition = false)
    {
        GameObject popup = Instantiate(_correctPopupPrefab, _popupParent);

        if (isWorldPosition)
            popup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(position);
        else
            popup.GetComponent<RectTransform>().position = position;

        Destroy(popup, delay);
        return popup;
    }

    public GameObject SpawnWrongPopup(Vector3 position, float delay = 1, bool isWorldPosition = false)
    {
        GameObject popup = Instantiate(_wrongPopupPrefab, _popupParent);

        if (isWorldPosition)
            popup.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(position);
        else
            popup.GetComponent<RectTransform>().position = position;

        Destroy(popup, delay);
        return popup;

    }

    
}
