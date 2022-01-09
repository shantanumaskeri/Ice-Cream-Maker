using UnityEngine;

public class ApplicationLifeCycle : MonoBehaviour
{
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            Application.Quit();
		}
    }

}
