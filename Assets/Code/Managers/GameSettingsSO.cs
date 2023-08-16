using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings", fileName = "GameSettings")]
public class GameSettingsSO : ScriptableObject
{
    [field: SerializeField] public float CircleCloseTimer {get; private set;} = 20f;
}