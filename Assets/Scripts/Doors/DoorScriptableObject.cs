using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "ScriptableObjects/DoorData")]
public class DoorScriptableObject : ScriptableObject
{
    public DoorColor DoorColor;
    public bool IsPremadeInScene;
}

public enum DoorColor
{
    Red,
    Green,
    Blue
}
