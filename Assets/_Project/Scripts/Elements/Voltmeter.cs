using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Voltmeter : Element, IListener, IPlacable
{
    [SerializeField] private TextMeshPro textMeshPro;
    [Header("Inputs")]
    [SerializeField] private int inputChannel;

    private bool isVisible = true;
    public void SetInput(float voltage)
    {
        textMeshPro.text = voltage.ToString(CultureInfo.InvariantCulture) + " V";
    }
    
    void Start()
    {
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
    }

    public void Hide()
    {
        if (isVisible)
        {
            isVisible = false;
            transform.position = new Vector3(transform.position.x, transform.position.y -10f, transform.position.z);
        }
    }

    public void Show() { 
        if (!isVisible)
        {
            isVisible = false;
            transform.position = new Vector3(transform.position.x, transform.position.y +10f, transform.position.z);
        }
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public void Place(Vector3 where, float rotation, List<int> inputChannels, List<int> outputChannels)
    {
        transform.position = new Vector3(where.x, transform.position.y, where.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotation);
        GameManager.GetChannel(inputChannel).RemoveVoltageListener(this);
        inputChannel = inputChannels[0];
        GameManager.GetChannel(inputChannel).AddVoltageListener(this);
        Show();
    }
}
