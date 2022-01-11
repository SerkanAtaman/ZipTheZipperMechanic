using UnityEngine;
using ZipTheZipper.ZipperManagement;

public class SceneReferenceHolder : MonoBehaviour
{
    public static SceneReferenceHolder Instance { get; private set; }

    [Header("Scripts References")]
    public ZipperController ZipperControl;
    public Camera MainCam;
    public ZipperHeadController ZipperHead;

    private void Awake()
    {
        Instance = this;
    }
}