using UnityEngine;

namespace ZipTheZipper.ZipperManagement
{
    public class ZipperController : MonoBehaviour
    {
        [Header("Script References")]
        [SerializeField] private ZipperPinLine _leftPinLine = null;
        [SerializeField] private ZipperPinLine _rightPinLine = null;
        [SerializeField] private ZipperInnerMesh _innerMeshRenderer = null;

        [Header("Zipper Pin Materials")]
        [SerializeField] private Material _pinOpenMaterial = null;
        [SerializeField] private Material _pinCloseMaterial = null;

        [Header("Object References")]
        [SerializeField] private Transform _zipperHead = null;
        [SerializeField] private Transform _zipperStartPoint = null;
        [SerializeField] private Transform _zipperEndPointLeft = null;
        [SerializeField] private Transform _zipperEndPointRight = null;

        private Zipper _zipper;

        private Vector3[] _leftPinsLinePositions;
        [SerializeField]private Vector3[] _rightPinsLinePositions;

        private int _zipperLength;

        public void CreateZipperStruct(int zipperLength)
        {
            _zipper = new Zipper(zipperLength);
            _zipperLength = zipperLength;

            _leftPinsLinePositions = new Vector3[_zipperLength + 1];
            _rightPinsLinePositions = new Vector3[_zipperLength + 1];
        }

        public void ExpandZipperStruct(bool leftSide, int pinID, Transform pinTransform)
        {
            if (leftSide)
            {
                _zipper.LeftSidePins[pinID].PinTransform = pinTransform;
                _zipper.LeftSidePins[pinID].DefaultPosition = pinTransform.position;
                _zipper.LeftSidePins[pinID].DefaultRotation = pinTransform.rotation;
                _zipper.LeftSidePins[pinID].MaxRotation = Quaternion.Euler(new Vector3(-90, 0, 140));
                _zipper.LeftSidePins[pinID].PinRenderer = pinTransform.GetComponent<MeshRenderer>();
            }
            else
            {
                _zipper.RightSidePins[pinID].PinTransform = pinTransform;
                _zipper.RightSidePins[pinID].DefaultPosition = pinTransform.position;
                _zipper.RightSidePins[pinID].DefaultRotation = pinTransform.rotation;
                _zipper.RightSidePins[pinID].MaxRotation = Quaternion.Euler(new Vector3(-90, 0, 40));
                _zipper.RightSidePins[pinID].PinRenderer = pinTransform.GetComponent<MeshRenderer>();
            }
        }

        public void PositionZipperPins()
        {
            for(int i = 0; i < _zipperLength; i++)
            {
                _zipper.LeftSidePins[i].PositionItself(_zipperHead.position);
                _zipper.RightSidePins[i].PositionItself(_zipperHead.position);

                if (_zipper.LeftSidePins[i].ZipAmount >= 0.95f) _zipper.LeftSidePins[i].PinRenderer.material = _pinCloseMaterial;
                else _zipper.LeftSidePins[i].PinRenderer.material = _pinOpenMaterial;

                if (_zipper.RightSidePins[i].ZipAmount >= 0.95f) _zipper.RightSidePins[i].PinRenderer.material = _pinCloseMaterial;
                else _zipper.RightSidePins[i].PinRenderer.material = _pinOpenMaterial;

                _leftPinsLinePositions[i] = _zipper.LeftSidePins[i].PinTransform.position;
                _rightPinsLinePositions[i] = _zipper.RightSidePins[i].PinTransform.position;
            }

            _leftPinsLinePositions[0] = _leftPinsLinePositions[0] + new Vector3(0, 0, -1.3f);
            _rightPinsLinePositions[0] = new Vector3(_rightPinsLinePositions[0].x, _rightPinsLinePositions[0].y, _leftPinsLinePositions[0].z);

            _leftPinsLinePositions[_zipperLength] = _leftPinsLinePositions[_zipperLength - 1] + new Vector3(0, 0, 0.75f);
            _rightPinsLinePositions[_zipperLength] = new Vector3(_rightPinsLinePositions[_zipperLength - 1].x, _rightPinsLinePositions[_zipperLength - 1].y,
                                                                    _leftPinsLinePositions[_zipperLength].z);

            _leftPinLine.SetLinePositions(_leftPinsLinePositions);
            _rightPinLine.SetLinePositions(_rightPinsLinePositions);
            _innerMeshRenderer.UpdateInnerMesh(_leftPinsLinePositions, _rightPinsLinePositions);

            _zipperEndPointLeft.position = _leftPinsLinePositions[_zipperLength] + new Vector3(0, 0, 1.03901f);
            _zipperEndPointRight.position = _rightPinsLinePositions[_zipperLength] + new Vector3(0, 0, 1.03901f);

            if (CheckIfZipperZipped()) GameManager.Instance.EndGame(true);
        }

        public void OnZipperCreated()
        {
            _zipperStartPoint.position = transform.position - new Vector3(0, 0, 1.1f);
            PositionZipperPins();
            _innerMeshRenderer.GenerateInnerMesh(_leftPinsLinePositions, _rightPinsLinePositions);
        }

        public Vector3 GetZipperMaxPoint()
        {
            return _rightPinsLinePositions[_zipperLength];
        }

        private bool CheckIfZipperZipped()
        {
            bool zipperZipped = true;

            for(int i = 0; i < _zipperLength; i++)
            {
                if(_zipper.LeftSidePins[i].ZipAmount < 1.0f || _zipper.RightSidePins[i].ZipAmount < 1.0f)
                {
                    zipperZipped = false;
                    return zipperZipped;
                }
            }

            return zipperZipped;
        }
    }
}