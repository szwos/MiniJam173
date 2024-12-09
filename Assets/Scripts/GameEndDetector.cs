using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameEndDetector : MonoBehaviour
    {
        public GameObject grid;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            DontDestroyOnLoad(grid);
            SceneManager.LoadScene((int)ScenesEnum.GAME_END_SCENE);
        }
        
    }
}