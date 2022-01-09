using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    public Text instructions;

    public void UpdateInstructions(int size, string copy)
    {
        instructions.fontSize = size;
        instructions.text = copy;
    }

}
