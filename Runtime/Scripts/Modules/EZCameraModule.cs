using EZController.Camera;
using System.Collections.Generic;
using UnityEngine;

namespace EZController.Modules
{
    public class EZCameraModule : EZBaseModule
    {
        public EZCarCamera CarCamera;
        public float Smoothing = 6.0f;
        public Transform CameraTarget;
        public List<Transform> cameraAnchorList;

        public KeyCode CamerKey = KeyCode.C;
        private void Start()
        {
            SetupCamera();
        }

        private void SetupCamera()
        {
            CarCamera.smoothing = Smoothing;
            CarCamera.lookAtTarget = CameraTarget;
            CarCamera.positionTarget = cameraAnchorList[0];
            CarCamera.advancedOptions.switchViewKey = CamerKey;
        }

        void Update()
        {
            Camera();
        }

        private void Camera()
        {
            var yButton = false;// Input.GetButton("Y Button");
            if (Input.GetKeyDown(CamerKey) || yButton)
            {
                Nextcamera();
            }
        }

        private void Nextcamera()
        {
            int index = cameraAnchorList.FindIndex(a => a == CarCamera.positionTarget);
            int nxetIndex = index + 1;
            if (nxetIndex >= cameraAnchorList.Count)
                nxetIndex = 0;

            CarCamera.positionTarget = cameraAnchorList[nxetIndex];
        }
    }
}
