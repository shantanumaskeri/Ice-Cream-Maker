using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	[HideInInspector]
	public int numberOfScoops;
	[HideInInspector]
	public int numberOfCherries;

	public LevelLoader levelLoader;
	public UserInterface userInterface;

	private string currentState;

	private void Start()
	{
		Cursor.visible = false;

		Init();
	}

	private void Init()
	{
		currentState = "Step 1";
		numberOfScoops = 0;
		numberOfCherries = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void Reset()
	{
		SceneManager.LoadScene("Main");
	}

	public void SetState(string state)
	{
		currentState = state;
	}

	public string GetState()
	{
		return currentState;
	}

	public void CheckState()
	{
		if (GetState() == "Completed")
		{
			levelLoader.FadeToLevel();
		}
	}

}
