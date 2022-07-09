using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    public Text levelText, hitpointsText, coinsText, upgradeCostText, expText;
    
    // Logic
    private int _currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform expBar;
    
    // Character Selection
    public void OnArrowClick(bool rightDirection)
    {
        if (rightDirection)
        {
            _currentCharacterSelection++;
            
            // If we've reached the last sprite, return to start
            if (_currentCharacterSelection == GameManager.instance.playerSprites.Count)
                _currentCharacterSelection = 0;
        }
        else
        {
            _currentCharacterSelection--;
            
            // If we've reached the first sprite, loop and show the last
            if (_currentCharacterSelection < 0)
                _currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[_currentCharacterSelection];
    }
    
    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateInfo();
        }
    }
    
    // Update character information
    public void UpdateInfo()
    {
        // Weapon ?
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        upgradeCostText.text = "NOT IMPLEMENTED YET!";
        
        // Meta
        hitpointsText.text = GameManager.instance.player.hitPoint.ToString();
        levelText.text = "NOT IMPLEMENTED YET!";
        coinsText.text = GameManager.instance.coins.ToString();
        
        // Experience bar
        expText.text = "NOT IMPLEMENTED YET!";
        expBar.localScale = new Vector3(.5f, 0, 0);
    }
}