using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    UnitManager unit_manager;
    [SerializeField]
    MapManager map_manager;

    [SerializeField]
    List<StateProcessPair> state_process_pairs;
    Dictionary<FieldInputStateType, IFieldInput> processes = new Dictionary<FieldInputStateType, IFieldInput>();
    void Start()
    {
        ConvertStateProcessPairToDictionary();
        map_manager.LoadAndGenerateMap();
        unit_manager.LoadAndGenerateUnits();

        processes[FieldInputStateType.UnitSelect].Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ��ԁA�����̃y�A��List���玫���^�ɕϊ�
    /// </summary>
    private void ConvertStateProcessPairToDictionary()
    {
        foreach(StateProcessPair pair in state_process_pairs)
        {
            if (pair.process is IFieldInput input)
            {
                processes[pair.state_type] = input;
            }
        }
    }

    [System.Serializable]
    public class StateProcessPair
    {
        public FieldInputStateType state_type;
        public MonoBehaviour process; // ���ӁF�����ł� IFieldInput ���g���Ȃ��̂� MonoBehaviour ��
    }

}
