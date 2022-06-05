using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    private const string PlayerName = "Player";
    protected override void OnCollide(Collider2D col)
    {
        if (col.name == PlayerName)
        {
            // Teleport tha Player
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
