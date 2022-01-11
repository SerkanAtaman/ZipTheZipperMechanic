using UnityEngine;
using static Utilities;

namespace ZipTheZipper.ZipperManagement
{
    [RequireComponent(typeof(ZipperController))]
    public class ZipperCreator : MonoBehaviour
    {
        [Header("Zipper Size")]
        [SerializeField] private float _zipperWidth = 1;
        [SerializeField] private int _zipperLength = 10;

        [Header("Pin Prefab")]
        [SerializeField] private GameObject _pinPrefab = null;

        ZipperController _zipperController;

        private float _verticalSpaceBtwPins = 0.82f;

        private void Awake()
        {
            _zipperController = GetComponent<ZipperController>();
        }

        private void Start()
        {
            CreateZipper();
        }

        private void CreateZipper()
        {
            _zipperController.CreateZipperStruct(_zipperLength);

            Vector3 leftPinPos = new Vector3(transform.position.x - (_zipperWidth * 0.5f), 0, transform.position.z + _verticalSpaceBtwPins * 0.5f);
            Vector3 rightPinPos = new Vector3(transform.position.x + (_zipperWidth * 0.5f), 0, transform.position.z);
            Transform pinLeft;
            Transform pinRight;

            for(int i = 0; i < _zipperLength; i++)
            {
                pinLeft = Instantiate(_pinPrefab, leftPinPos, Quaternion.Euler(new Vector3(-90, 0, 180))).transform;
                pinRight = Instantiate(_pinPrefab, rightPinPos, Quaternion.Euler(new Vector3(-90, 0, 0))).transform;

                leftPinPos.z += _verticalSpaceBtwPins;
                rightPinPos.z += _verticalSpaceBtwPins;

                _zipperController.ExpandZipperStruct(true, i, pinLeft);
                _zipperController.ExpandZipperStruct(false, i, pinRight);
            }

            SceneReferences.ZipperControl.OnZipperCreated();
            SceneReferences.ZipperHead.ReplaceItselfBasedOnZipper();

            enabled = false;
        }
    }
}