using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZipTheZipper
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;

        public static GameManager Instance { get; private set; }

        public delegate void LevelCompletedHandler();
        public LevelCompletedHandler OnLevelCompleted;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _targetFrameRate;

            Instance = this;
        }

        public void EndGame(bool success)
        {
            if (success)
            {
                Debug.Log("Level Finished");
                OnLevelCompleted?.Invoke();
            }
        }

        public void NextLevelButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}