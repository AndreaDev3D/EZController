using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EZController.Modules
{
    public class EZSpeedUIModule : EZBaseModule
    {
        public enum SpeedUnitType 
        {
            KmH,
            MiH
        }

        public Text txtSpeed;
        public SpeedUnitType speedUnit;

        private float m_rate = 0;
        private const float kmHrRate = 3.6f;
        private const float MiHrRate = 2.23694f;

        private string SpeedText => $"{(Controller.Speed* m_rate):0}";

        private void Start()
        {
            switch (speedUnit)
            {
                case SpeedUnitType.KmH:
                    m_rate = kmHrRate;
                    break;
                case SpeedUnitType.MiH:
                    m_rate = MiHrRate;
                    break;
            }
        }

        private void Update()
        {
            if(txtSpeed != null)
                txtSpeed.text = SpeedText;
        }
    }
}
