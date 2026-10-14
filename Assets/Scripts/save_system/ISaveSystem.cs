using UnityEngine;

public interface ISaveSystem
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
