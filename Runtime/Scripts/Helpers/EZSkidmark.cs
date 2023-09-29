using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EZController.Helpers
{
    [RequireComponent(typeof(LineRenderer))]
    public class EZSkidmark : MonoBehaviour
    {
        private LineRenderer _ln;
        private List<Vector3> _pointList = new List<Vector3>();
        private float minSkidmarkDistnc = 0.05f;

        public Vector3 CurrentPosition => transform.position;
        void Start()
        {
            _ln = gameObject.GetComponent<LineRenderer>();

            UpdateLine();
        }

        void Update()
        {
            float dist = Vector3.Distance(_pointList.Last(), CurrentPosition);
            if (dist > minSkidmarkDistnc)
                UpdateLine();

        }

        void UpdateLine()
        {
            _pointList.Add(CurrentPosition + new Vector3(0, 0.02f, 0));
            _ln.positionCount = _pointList.Count;
            _ln.SetPositions(_pointList.ToArray());
        }
    }
}