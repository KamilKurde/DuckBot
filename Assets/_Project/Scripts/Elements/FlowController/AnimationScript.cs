using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private FlowController flowController;

    public void ChangeChannels()
    {
        flowController.ChangeChannels();
    }
}