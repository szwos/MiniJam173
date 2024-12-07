using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButtonLogic : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene((int)ScenesEnum.MAIN_SCENE);
    }
}
