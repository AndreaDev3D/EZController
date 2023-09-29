using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZResetModule : MonoBehaviour
{
    public KeyCode ResetKey = KeyCode.R;

    void Update()
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        if (Input.GetKeyDown(ResetKey))
        {
            this.transform.eulerAngles = new Vector3(0, this.transform.rotation.y, this.transform.rotation.z);
        }
    }
}
