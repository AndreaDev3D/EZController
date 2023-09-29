using EZController.Modules;
using System.Collections.Generic;
using UnityEngine;

public class EZLightModule : EZBaseModule
{
	private bool m_IsFrontLightOn;
    private bool m_IsBackLightOn;

    public List<Light> FrontLight;
	public List<Light> RearLight;

	public KeyCode LightKey = KeyCode.E;
	public KeyCode BrakeKey = KeyCode.Space;
    public KeyCode HandBrakeKey = KeyCode.B;

    void Start()
	{
		SetFrontLight(m_IsFrontLightOn);
		SetBackLight(false);
	}

    void Update()
	{
		EvaluateInput();
	}

	private void EvaluateInput()
	{
		if (Input.GetKeyDown(LightKey))
		{
			SetFrontLight(!m_IsFrontLightOn);
		}

		SetBackLight(Controller.BrakingForce > 0f);
    }

	private void SetFrontLight(bool value) 
	{
		m_IsFrontLightOn = value;
		SetLight(FrontLight, value);

    }

	private void SetBackLight(bool value)
	{
		m_IsBackLightOn = value;
        SetLight(RearLight, value);
    }

	private void SetLight(List<Light> lights, bool value)
	{
        foreach (var light in lights)
        {
            light.enabled = value;
        }
    }
}
