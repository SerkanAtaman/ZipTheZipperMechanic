using UnityEngine;

namespace ZipTheZipper.UserInterface
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _winCanvas = null;
        [SerializeField] private GameObject _inputCanvas = null;

        private void Start()
        {
            _winCanvas.SetActive(false);
            GameManager.Instance.OnLevelCompleted += LevelCompleted;
        }

        private void LevelCompleted()
        {
            _winCanvas.SetActive(true);
            _inputCanvas.SetActive(false);
            Time.timeScale = 0;
        }
    }
}