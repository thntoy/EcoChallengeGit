using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterSpriteLoader : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Image _characterImage;

    private void Awake()
    {
        TryGetComponent(out _spriteRenderer);
        TryGetComponent(out _characterImage);
    }

    private void OnEnable()
    {
        GameManager.OnGameAwake += LoadUserSprite;
    }

    private void OnDisable()
    {
        GameManager.OnGameAwake -= LoadUserSprite;
    }

    public void LoadUserSprite()
    {
        string gender = PlayfabManager.Instance.CurrentPlayerData.Gender.ToString();
        int characterIndex = PlayfabManager.Instance.CurrentPlayerData.CharacterIndex;

        // Load all sprites from the sprite sheet based on the selected gender
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Characters/" + gender);

        if (sprites != null && sprites.Length > characterIndex)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = sprites[characterIndex];
                Debug.Log("Sprite loaded: " + sprites[characterIndex].name);
            }
            else if (_characterImage != null)
            {
                _characterImage.sprite = sprites[characterIndex];
                Debug.Log("Sprite loaded: " + sprites[characterIndex].name);
            }
        }
        else
        {
            Debug.LogError("Sprite not found: Check if characterIndex is correct or if sprites are correctly named.");
        }
    }
}
