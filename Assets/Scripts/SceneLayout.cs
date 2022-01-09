using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLayout : MonoBehaviour
{

    public float delay = 3.0f;
    public LevelLoader levelLoader;

    private void Start()
    {
        CheckCurrentSceneLoaded();
    }

    private void CheckCurrentSceneLoaded()
	{
        switch (SceneManager.GetActiveScene().buildIndex)
		{
            case 0:
            case 2:
                StartCoroutine(AutoSceneChange());
                break;
		}
	}

    private IEnumerator AutoSceneChange()
	{
        yield return new WaitForSeconds(delay);

        levelLoader.FadeToLevel();
    }
    
    public void ManualSceneChange()
	{
        FindObjectOfType<GameManager>().CheckState();
	}

}
