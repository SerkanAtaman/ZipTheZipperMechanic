using static Utilities;
using UnityEngine;
using System.Collections;
using ZipTheZipper.ScriptableObjects.Input;

namespace ZipTheZipper.ZipperManagement
{
    public class ZipperHeadController : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _moveSpeed = 12;

        [SerializeField] private InputData _inputData = null;

        private float _minPosZ;
        private float _maxPosZ;

        private void Update()
        {
            switch (_inputData.CurrentInputState)
            {
                case InputData.InputState.NonDrag:

                    break;

                case InputData.InputState.DragUp:

                    Move(Vector3.forward);
                    break;

                case InputData.InputState.DragDown:

                    Move(Vector3.back);
                    break;
            }

            _inputData.CurrentInputState = InputData.InputState.NonDrag;
        }

        private void Move(Vector3 dir)
        {
            Vector3 targetPoint = transform.position + Time.deltaTime * _moveSpeed * dir;

            targetPoint.z = Mathf.Clamp(targetPoint.z, _minPosZ, _maxPosZ);
            transform.position = targetPoint;

            SceneReferences.ZipperControl.PositionZipperPins();
        }

        public void ReplaceItselfBasedOnZipper()
        {
            transform.position = SceneReferences.ZipperControl.transform.position + new Vector3(0, 0, 1.2f);

            _minPosZ = transform.position.z;

            StartCoroutine(SetMaxPositionZ());
        }

        private IEnumerator SetMaxPositionZ()
        {
            while (true)
            {
                _maxPosZ = SceneReferences.ZipperControl.GetZipperMaxPoint().z;
                
                if(_maxPosZ > 0)
                {
                    yield break;
                }

                yield return null;
            }
        }
    }
}