using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    private const string PlayerName = "Player";
    protected override void OnCollide(Collider2D col)
    {
        if (col.name != PlayerName) return;
        // Teleport tha Player to random scene (dungeon)
        GameManager.instance.SaveState();  // expensive way, but ok for now
        string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
