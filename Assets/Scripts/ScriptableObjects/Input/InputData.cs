using UnityEngine;

namespace ZipTheZipper.ScriptableObjects.Input
{
    [CreateAssetMenu(fileName = "InputData", menuName = "ScriptableObjects/Input/InputData")]
    public class InputData : ScriptableObject
    {
        public enum InputState { DragDown, DragUp, NonDrag}
        public InputState CurrentInputState;
    }
}