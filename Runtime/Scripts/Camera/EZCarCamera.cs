using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZController.Camera
{
    public class EZCarCamera : MonoBehaviour
    {
        [Serializable]
        public class AdvancedOptions
        {
            public bool updateCameraInUpdate;
            public bool updateCameraInFixedUpdate = true;
            public bool updateCameraInLateUpdate;
            public KeyCode switchViewKey = KeyCode.C;
        }

        public float smoothing = 0.1f;

        public Transform lookAtTarget;
        public Transform positionTarget;
        //public Transform sideView;
        public AdvancedOptions advancedOptions;

        //bool m_ShowingSideView;

        private void FixedUpdate()
        {
            if (advancedOptions.updateCameraInFixedUpdate)
                UpdateCamera();
        }

        private void Update()
        {

            if (advancedOptions.updateCameraInUpdate)
                UpdateCamera();
        }

        private void LateUpdate()
        {
            if (advancedOptions.updateCameraInLateUpdate)
                UpdateCamera();
        }

        private void UpdateCamera()
        {
            transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
            transform.LookAt(lookAtTarget);
        }
    }
}
