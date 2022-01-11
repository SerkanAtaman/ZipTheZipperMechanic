using UnityEngine;
using UnityEngine.EventSystems;
using ZipTheZipper.ScriptableObjects.Input;
using static Utilities;

namespace ZipTheZipper.InputManagement
{
    public class InputListener : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private InputData _inputData = null;

        [SerializeField] private LayerMask _zipperHeadLayer = 1;

        Vector3 _previousPointerPos;

        bool _zipperHeadSelected;

        private void Start()
        {
            _inputData.CurrentInputState = InputData.InputState.NonDrag;
            _zipperHeadSelected = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            TryCatchingZipperHead(eventData.pointerCurrentRaycast.screenPosition);

            _previousPointerPos = eventData.pointerCurrentRaycast.screenPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_zipperHeadSelected) return;

            Vector3 currentPointerPos = eventData.pointerCurrentRaycast.screenPosition;

            if(currentPointerPos.y > _previousPointerPos.y)
            {
                _inputData.CurrentInputState = InputData.InputState.DragUp;
            }
            else if (currentPointerPos.y < _previousPointerPos.y)
            {
                _inputData.CurrentInputState = InputData.InputState.DragDown;
            }
            else
            {
                _inputData.CurrentInputState = InputData.InputState.NonDrag;
            }

            _previousPointerPos = currentPointerPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _inputData.CurrentInputState = InputData.InputState.NonDrag;
        }

        private void TryCatchingZipperHead(Vector3 pointerPos)
        {
            if(Physics.Raycast(SceneReferences.MainCam.ScreenPointToRay(pointerPos), 100f, _zipperHeadLayer))
            {
                _zipperHeadSelected = true;
            }
            else
            {
                _zipperHeadSelected = false;
            }
        }
    }
}