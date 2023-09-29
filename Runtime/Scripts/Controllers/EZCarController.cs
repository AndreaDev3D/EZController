using EZController.Data;
using System.Collections.Generic;
using UnityEngine;
using static EZController.Enumerators.Enumerators;

namespace EZController.Controllers
{
    public class EZCarController : MonoBehaviour
    {
        [HideInInspector]
        public bool IsAI;

        [Header("Setup")]
        public List<EZWheelConfig> FrontWheelConfigs;
        public List<EZWheelConfig> BackWheelConfigs;

        [Header("Car Settings")]
        [Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
        public TractionType TractionType = TractionType.AllWheelDrive;
        [Tooltip("Maximum torque applied to the driving wheels")]
        public float MaxAcceleration = 500f;
        [Tooltip("Maximum speed the vehicle can reach.")]
        public float MaxSpeed = 100;
        [Tooltip("The brake torque type: front-brakes, rear-brakes, all-brakes.")]
        public BrakesType BrakesType = BrakesType.FrontBrakes;
        [Tooltip("Maximum brake torque applied to the driving wheels")]
        public float MaxBrakingForce = 300f;
        [Tooltip("Maximum steering angle of the wheels")]
        public float MaxTurnAngle = 15f;

        [SerializeField]
        [Tooltip("The center of the mass of the vehicle.")]
        private GameObject centerOfMass;
        private Rigidbody m_rigidbody;
        public Vector3 AngularVelocity => m_rigidbody.angularVelocity;


        public bool IsHandbrakeOn { get; private set; } = true;
        public float Acceleration { get; private set; } = 0f;
        public float BrakingForce { get; private set; } = 0f;
        public float TurnAngle { get; private set; } = 0f;

        public float Speed => m_rigidbody.velocity.magnitude;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.centerOfMass = centerOfMass.transform.localPosition;

            FrontWheelConfigs.ForEach(config => config.Init());
            BackWheelConfigs.ForEach(config => config.Init());
        }

        private void Update()
        {

            if (!IsAI)
            {
                if (Input.GetKeyUp(KeyCode.B))
                {
                    IsHandbrakeOn = !IsHandbrakeOn;
                }
            }
        }

        // https://answers.unity.com/storage/temp/113530-controlmap.png
        private void FixedUpdate()
        {
            if (!IsAI)
            {
                var breaking = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0) ? 1f : 0f;                
                var accelerate = -Input.GetAxis("Joystick9") + Input.GetAxis("Joystick10") + Input.GetAxis("Vertical");
                var turn = Input.GetAxis("Horizontal");
                Drive(breaking, accelerate, turn, IsHandbrakeOn);
            }
        }

        public void Drive(float breackingValue, float accelerationValue, float turnValue, bool isHandBrakOn = false)
        {
            BreakCheck(isHandBrakOn ? MaxBrakingForce : breackingValue);
            Accelerate(isHandBrakOn ? 0f : accelerationValue);
            Turn(turnValue);            
        }

        private void BreakCheck(float value)
        {
            BrakingForce = value != 0f ? MaxBrakingForce : 0f;

            switch (BrakesType)
            {
                case BrakesType.FrontBrakes:
                    FrontWheelConfigs.ForEach(config => config.SetBrakeTorque(BrakingForce));
                    break;
                case BrakesType.RearBrakes:
                    BackWheelConfigs.ForEach(config => config.SetBrakeTorque(BrakingForce));
                    break;
                case BrakesType.AllBrakes:
                    FrontWheelConfigs.ForEach(config => config.SetBrakeTorque(BrakingForce));
                    BackWheelConfigs.ForEach(config => config.SetBrakeTorque(BrakingForce));
                    break;
            }
        }

        private void Accelerate(float value)
        {
            Acceleration = Speed > MaxSpeed ? 0f : MaxAcceleration * value;

            switch (TractionType)
            {
                case TractionType.FrontWheelDrive:
                    FrontWheelConfigs.ForEach(config => config.SetMotorTorque(Acceleration));
                    break;
                case TractionType.RearWheelDrive:
                    BackWheelConfigs.ForEach(config => config.SetMotorTorque(Acceleration));
                    break;
                case TractionType.AllWheelDrive:
                    FrontWheelConfigs.ForEach(config => config.SetMotorTorque(Acceleration));
                    BackWheelConfigs.ForEach(config => config.SetMotorTorque(Acceleration));
                    break;
            }
        }

        private void Turn(float value)
        {
            TurnAngle = MaxTurnAngle * value;
            FrontWheelConfigs.ForEach(config => config.SetSteerAngle(TurnAngle));
        }
    }
}