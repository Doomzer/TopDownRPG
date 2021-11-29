using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    public string nextSceneName;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.SaveState();
            string sceneName = nextSceneName;
            if (sceneName == "")
                sceneName = sceneNames[Random.Range(0, sceneNames.Length)];

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
