using System;
using UnityEngine;

namespace EZController.Data
{
    [Serializable]
    public class EZWheelConfig
    {
        [field:SerializeField]
        public WheelCollider Collider { get; set; }
        [field: SerializeField]
        public Transform Wheel { get; set; }
        [field: SerializeField]
        public Vector3 WheelOffset { get; set; }

        [field: SerializeField]
        public Transform Suspension { get; set; }
        [field: SerializeField]
        private SkinnedMeshRenderer m_SuspensionRenderer { get; set; }
        private float m_SuspensionBlendShap { get; set; }
        [SerializeField]
        private bool m_FollowWheelRotation;

        public float smoothTime = 0.5f;
        float smoothVelocity = 0.0f;
        
        public void Init()            
        {
            m_SuspensionRenderer = Suspension.gameObject.GetComponent<SkinnedMeshRenderer>();
        }

        public void SetMotorTorque(float value) 
        {
            Collider.motorTorque = value;
            UpdateVisual();
        }

        public void SetBrakeTorque(float value)
        {
            Collider.brakeTorque = value;
            UpdateVisual();
        }

        public void SetSteerAngle(float value)
        {
            Collider.steerAngle = value;
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            Collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            Wheel.position = new Vector3(Collider.transform.position.x, position.y, Collider.transform.position.z);
            Wheel.rotation = rotation;

            UpdateSuspensionVisual();
        }

        private void UpdateSuspensionVisual()
        {
            if (!Suspension)
             return;
            
            Suspension.localPosition = new Vector3(Suspension.localPosition.x, Wheel.localPosition.y, Suspension.localPosition.z);
            if (m_FollowWheelRotation)
            {
                var rotationAmount = Input.GetAxis("Horizontal") * Mathf.Abs(Collider.steerAngle);
                Suspension.transform.localRotation = Quaternion.Euler(0, rotationAmount, 0);
            }
            m_SuspensionBlendShap = Mathf.SmoothDamp(m_SuspensionBlendShap, GetSpringDistance(), ref smoothVelocity, smoothTime);
            m_SuspensionRenderer?.SetBlendShapeWeight(0, m_SuspensionBlendShap);            
        }

        public float GetSpringDistance() 
        {
            var distance = 0f;
            WheelHit hit;
            if (Collider.GetGroundHit(out hit))
            {
                var point = hit.point + (Wheel.up * Collider.radius);
                distance = Vector3.Distance(Wheel.position, point);
                distance = Mathf.Lerp(0f, 100f, distance * 0.95f);
            }

            return distance;
        }
    }
}
