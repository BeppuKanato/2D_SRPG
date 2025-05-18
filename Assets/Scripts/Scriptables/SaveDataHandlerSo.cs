using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SaveDataHandlerSo", menuName = "Scriptable Objects/SaveDataHandlerSo")]
public class SaveDataHandlerSo : ScriptableObject
{
    public AllyUnitSaveData[] ally_unit_save_data { get; private set; }
}
