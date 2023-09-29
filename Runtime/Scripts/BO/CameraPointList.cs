using System.Collections.Generic;
using UnityEngine;

namespace EZController.BO
{
    public class CameraPointList : MonoBehaviour
    {        
        [HideInInspector]
        public List<CameraPoint> CameraPoints { get; set; } = new List<CameraPoint>();
    }
}