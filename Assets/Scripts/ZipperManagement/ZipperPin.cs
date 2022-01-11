using UnityEngine;

namespace ZipTheZipper.ZipperManagement
{
    public struct ZipperPin
    {
        public Transform PinTransform { get; set; }
        public MeshRenderer PinRenderer;

        public Vector3 DefaultPosition;
        public Quaternion DefaultRotation;
        public Quaternion MaxRotation;

        public float ZipAmount { get; private set; }

        public void PositionItself(Vector3 holderPos)
        {
            float distanceBtwZipHead = PinTransform.position.z - holderPos.z;

            if (distanceBtwZipHead > 4)
            {
                // Pin is far above from zipper head. No need to change anything
                return;
            }
            else if(distanceBtwZipHead < 0)
            {
                // Pin is below the zipper head. Make sure its zipped properly

                holderPos.z = PinTransform.position.z;
                if (PinTransform.position.x < 0) holderPos.x = -1.05f;
                else holderPos.x = 1.05f;

                ZipAmount = 1.0f;
                PinTransform.position = holderPos;
                PinTransform.rotation = DefaultRotation;
                return;
            }

            // Calculate distance from zipper head to apply smooth zip effect
            distanceBtwZipHead *= 1.0f / 4.0f;
            distanceBtwZipHead = Mathf.Clamp01(distanceBtwZipHead);
            distanceBtwZipHead = 1.0f - distanceBtwZipHead;
            ZipAmount = Mathf.SmoothStep(0.0f, 1.0f, distanceBtwZipHead);

            holderPos.z = PinTransform.position.z;
            if (PinTransform.position.x < 0) holderPos.x = -1.05f;
            else holderPos.x = 1.05f;
            PinTransform.position = Vector3.Lerp(DefaultPosition, holderPos, ZipAmount);

            if(ZipAmount > 0 && ZipAmount < 1)
            {
                PinTransform.rotation = Quaternion.Lerp(DefaultRotation, MaxRotation, ZipAmount);
            }
            else
            {
                PinTransform.rotation = DefaultRotation;
            }
        }
    }
}