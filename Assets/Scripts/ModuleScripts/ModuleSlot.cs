using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ModuleSlot : MonoBehaviour
{
    public enum Side { left, right, center }
    Side side;

    int size;
	[SerializeField] Module module;
	[SerializeField] Image image;
	[SerializeField] Gradient heatGradient;
	float heatReceived;

	ModuleSlot[] connections; // heat connections this module slot has with other module slots

	public ModuleSlot(int _size)
	{
		size = _size;
	}

	private void Awake()
	{
		connections = FindObjectsOfType<ModuleSlot>();

		module.SetTemperature(0f);

		image = GetComponent<Image>();

        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.green, 0.0f);
        colors[1] = new GradientColorKey(Color.yellow, 0.5f);
        colors[2] = new GradientColorKey(Color.red, 1.0f);

		var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

        heatGradient.SetKeys(colors, alphas);

        if (module != null)
		{
			CheckTemperature();
		}
	}

	// utilizes the simple equation from the second law of thermodynamics
	// to transfer heat to all connected modules
	public void FlowHeat(float conductivity)
	{
		float distance;

		foreach (ModuleSlot ms in connections)
		{
			if (ms != this)
			{
                distance = Vector2.Distance(transform.position, ms.transform.position);

                // heat received by the other object
                ms.ReceiveHeat(-conductivity * (ms.GetModule().GetTemperature() - GetModule().GetTemperature()) / distance);
                // this object will receive heat when the other object's heat transfer is calculated
            }
        }
	}

	// is this good practice, or-
	float tempPercentage;
	private void CheckTemperature()
	{
		tempPercentage = module.GetTemperature() / module.GetMaxTemperature();
		image.color = heatGradient.Evaluate(tempPercentage);
	}

	// accessor methods
	public Module GetModule()
	{
		return module;
	}

	// mutator methods
	public bool RecieveModule(Module _module)
	{
		if (_module.GetSize() > size)
		{
			return false;
		}
		else
		{
			module = _module;
			image.sprite = module.GetSprite();
			return true;
		}
	}

	public void RemoveModule()
	{
		module = null;
		image = null;
	}

	public void ReceiveHeat(float _heatReceived)
	{
		heatReceived += _heatReceived;
	}

	public void GiveModuleHeat()
	{
		module.ReceiveHeat(heatReceived);
		heatReceived = 0;

		CheckTemperature();
	}

    private void OnValidate()
    {
		image.sprite = module.GetSprite();

        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.green, 0.0f);
        colors[1] = new GradientColorKey(Color.yellow, 0.5f);
        colors[2] = new GradientColorKey(Color.red, 1.0f);

        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

		heatGradient.SetKeys(colors, alphas);

		CheckTemperature();
    }
}