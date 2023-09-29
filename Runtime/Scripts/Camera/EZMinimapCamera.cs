using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZController.Camera
{
    public class EZMinimapCamera : MonoBehaviour
    {
        public Transform Target;
        public Vector3 CamOffset = new Vector3(0f, 10f, 0f);
        public float SmoothSpeed = 0.0001f;

        private Vector3 velocity = Vector3.zero;

        void LateUpdate()
        {
            var target = Target.position + CamOffset;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, SmoothSpeed);
        }
    }
}
