using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hudGO);
            Destroy(menuGO);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += LoadState;
    }
    
    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    
    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform healthPointBar;
    public GameObject hudGO;
    public GameObject menuGO;
    [SerializeField] private string spawnPointName = "SpawnPoint";
    
    // Logic
    public int coins;
    public int experience;

    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    
    // Upgrade Weapon 
    public bool TryUpgradeWeapon()
    {
        // If weapon at max level
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.Upgrade();
            return true;
        }

        // If not enough coins
        return false;
    }
    
    // Health Bar
    public void OnHealthPointChange()
    {
        float hpRatio = (float)player.healthPoint / (float)player.maxHealthPoint;
        healthPointBar.localScale = new Vector3(1, hpRatio, 1);
    }
    
    // Experience logic
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;
            
            // Check max level
            if (r == xpTable.Count)
                return r;
        }

        return r;
    }

    public int GetExpToLevel(int level)
    {
        int r = 0;
        int exp = 0;

        while (r < level)
        {
            exp += xpTable[r];
            r++;
        }

        return exp;
    }

    public void GrantExperience(int exp)
    {
        int currentLevel = GetCurrentLevel();
        experience += exp;
        // Check level after adding experience
        if (currentLevel < GetCurrentLevel())
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        player.PlayerLevelUp();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // set player correct spawn position
        player.transform.position = GameObject.Find(spawnPointName).transform.position;
    }

    #region Save and Load region

    // ----= Save n Load states =----
    /*
     * INT preferedSkin
     * INT coins
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("Save state " + s);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        
        //Change player data
        // Change skin
        coins = int.Parse(data[1]);
        
        experience = int.Parse(data[2]);
        player.SetLevel(GetCurrentLevel());

        weapon.SetWeaponLevel(int.Parse(data[3]));
    }

    [MenuItem("Tools/Clear all saves")]
    private static void ClearAllSaves()
    {
        string s = "";

        s += "0" + "|";
        s += "0" + "|";
        s += "0" + "|";
        s += "0";
        
        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("Save state " + s + " (all cleared!)");
    }
    
    // ----= end of Save n Load =----

    #endregion
}
