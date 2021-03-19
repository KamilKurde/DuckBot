using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    public void Hide();
    public void Show();
    public bool IsVisible();
    public void Place(Vector3 where, float rotation, List<int> inputChannels, List<int> outputChannels);
}
