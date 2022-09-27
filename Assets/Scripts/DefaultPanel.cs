using UnityEngine;

public class DefaultPanel : PannelBase
{
    [SerializeField] private GameObject targetGameObject;

    public override void Enable() => targetGameObject.SetActive(true);

    public override void Disable() => targetGameObject.SetActive(false);
}
