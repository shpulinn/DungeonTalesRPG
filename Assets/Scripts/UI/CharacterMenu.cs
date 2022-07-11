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
    public GameObject lakeOfCoinsLabel;
    
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
        // Change sprite in Menu
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[_currentCharacterSelection];
        // Change Player sprite
        GameManager.instance.player.SwapSprite(_currentCharacterSelection);
    }
    
    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateInfo();
        }
        else
        {
            if (lakeOfCoinsLabel.activeSelf)
                return;
            
            lakeOfCoinsLabel.SetActive(true);
            Invoke(nameof(HideCoinsMessage), 2.0f);
        }
    }
    
    // Update character information
    public void UpdateInfo()
    {
        // Weapon ?
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX LEVEL";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        
        // Meta
        hitpointsText.text = GameManager.instance.player.healthPoint.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        coinsText.text = GameManager.instance.coins.ToString();
        
        // Experience bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            // Display total exp if max level reached
            expText.text = $"{GameManager.instance.experience} total experience points.";
            expBar.localScale = Vector3.one;
        }
        else
        {
            int previousLevelExp = GameManager.instance.GetExpToLevel(currentLevel - 1);
            int currentLevelExp = GameManager.instance.GetExpToLevel(currentLevel);

            int difference = currentLevelExp - previousLevelExp;
            int currentExpIntoLevel = GameManager.instance.experience - previousLevelExp;

            float completionRatio = (float)currentExpIntoLevel / (float)difference;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            expText.text = $"{currentExpIntoLevel} / {difference}";
        }
    }

    private void HideCoinsMessage()
    {
        lakeOfCoinsLabel.SetActive(false);
    }
}
