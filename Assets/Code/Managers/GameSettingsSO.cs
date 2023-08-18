using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings", fileName = "GameSettings")]
public class GameSettingsSO : ScriptableObject
{
    [field: SerializeField] public float ZoneActiveTime {get; private set;} = 20f;
    [field: SerializeField] public float ZoneCloseSequence {get; private set;} = 3f;

    [field: SerializeField] public float FirstZoneRange {get; private set;} = 50f;

    [field: SerializeField] public float EachZoneCloseAmount {get; private set;} = 10f;

    [field: SerializeField] public float MinimumZoneRange {get; private set;} = 10f;

    public bool CanZoneClose(float currentZoneRange) => currentZoneRange > MinimumZoneRange; 
    public float GetNextRange(float currentZoneRange) => currentZoneRange -= EachZoneCloseAmount;

    public float GetNextRangeWithLevel(int level)
    {
        return FirstZoneRange - level * EachZoneCloseAmount; 
    }
}