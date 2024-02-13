using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "ScriptableObjects/DoorData")]
public class DoorScriptableObject : ScriptableObject
{
    public DoorColor doorColor;
    public bool isPremadeInScene;
}

public enum DoorColor
{
    Red,
    Green,
    Blue
}
