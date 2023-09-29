using EZController.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZController.Modules
{
    public class EZBaseModule : MonoBehaviour
    {
        private EZCarController m_Controller;
        [HideInInspector]
        public EZCarController Controller => m_Controller ??= GetComponent<EZCarController>();
    }
}