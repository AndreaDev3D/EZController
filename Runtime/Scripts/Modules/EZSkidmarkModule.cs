using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZController.Modules
{
    public class EZSkidmarkModule : EZBaseModule
    {
        public GameObject SkidMarkPrefab;

        [HideInInspector]
        public List<GameObject> m_SkidMarkPrefabList;

        public KeyCode BrakeKey = KeyCode.Space;

        private List<WheelCollider> m_Wheels;
        [field: SerializeField]
        private float Test;// => Controller.AngularVelocity.y;

        void Start()
        {
            m_Wheels = new List<WheelCollider>();
            Controller.FrontWheelConfigs.ForEach(wheel => m_Wheels.Add(wheel.Collider));
            Controller.BackWheelConfigs.ForEach(wheel => m_Wheels.Add(wheel.Collider));
        }

        void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 worldVelocity = rb.velocity;
            Vector3 localVelocity = transform.InverseTransformVector(worldVelocity);
            Test = localVelocity.x;
            Skidmarks();
        }

        private void Skidmarks()
        {
            if (Mathf.Abs(Test) >= 1.5f)
            {
                AddSkidmark();
            }
            else
            {
                RemoveSkidmark();
            }

            if (Input.GetKeyDown(BrakeKey))
            {
                AddSkidmark();
            }
            if (Input.GetKeyUp(BrakeKey))
            {
                RemoveSkidmark();
            }
            foreach (var w in m_Wheels)
            {
                if (!w.isGrounded)
                {
                    RemoveSkidmark();
                }
            }
        }

        private void AddSkidmark()
        {
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                var wheel = m_Wheels[i];
                wheel.GetGroundHit(out WheelHit hit);
                // generate skidmark wheel.
                if (SkidMarkPrefab != null)
                {
                    if (wheel.transform.childCount >= 2)
                        return;

                    var skm = Instantiate(SkidMarkPrefab);
                    skm.transform.parent = wheel.transform;
                    skm.transform.position = hit.point;
                    m_SkidMarkPrefabList.Add(skm);
                }
            }
        }

        private void RemoveSkidmark()
        {

            if (m_SkidMarkPrefabList.Count == 0)
                return;

            m_SkidMarkPrefabList.ForEach(skm =>
            {
                skm.transform.parent = null;
                Destroy(skm, 5.0f);
            });
            m_SkidMarkPrefabList.Clear();
        }
    }
}