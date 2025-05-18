using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class InputActionHandler : MonoBehaviour
{
    [SerializeField]
    PlayerInput player_input;

    //�A�N�V�����}�b�v�A�A�N�V�������ɓo�^���Ă���R�[���o�b�N���Ǘ�
    Dictionary<string, Dictionary<string, System.Action<InputAction.CallbackContext>>> current_callbacks = new Dictionary<string, Dictionary<string, System.Action<InputAction.CallbackContext>>>();

    /// <summary>
    /// �g�p����ActionMap��؂�ւ���
    /// </summary>
    /// <param name="action_map">
    /// �L���ɂ���ActionMap
    /// </param>
    public void SwitchActionMap(string action_map)
    {
        InputActionMap map = player_input.actions.FindActionMap(action_map);

        if (map == null)
        {
            Debug.LogError($"ActionMap '{action_map}' �͑��݂��܂���B");
            return;
        }
        player_input.SwitchCurrentActionMap(action_map);
    }

    /// <summary>
    /// ���݂�ActionMap�̎w��Actino��
    /// �R�[���o�b�N��ǉ�
    /// ������1�A�N�V���� = 1�R�[���o�b�N
    /// </summary>
    /// <param name="action_name">
    /// Actions�̖��O
    /// </param>
    /// <param name="callback">
    /// �o�^����R�[���o�b�N
    /// </param>
    public void SetCallback(string action_name, System.Action<InputAction.CallbackContext> callback)
    {
        InputAction action = player_input.actions.FindAction(action_name, false);
        //�A�N�V�������Ȃ��ꍇ
        if (action == null)
        {
            Debug.LogWarning($"�A�N�V���� '{action_name}' �͑��݂��܂���B");
            return;
        }

        string current_action_map = player_input.currentActionMap.name;

        //�A�N�V�����}�b�v�̃L�[���Ȃ��ꍇ
        if (!current_callbacks.ContainsKey(current_action_map))
        {
            current_callbacks[current_action_map] = new Dictionary<string, System.Action<InputAction.CallbackContext>>();
        }

        Dictionary<string, System.Action<InputAction.CallbackContext>> callbacksForMap = current_callbacks[current_action_map];

        //�R�[���o�b�N�o�^�ς݂̏ꍇ
        if (callbacksForMap.TryGetValue(action_name, out var existingCallback))
        {
            action.performed -= existingCallback;
        }

        action.performed += callback;
        current_callbacks[current_action_map][action_name] = callback; 
    }
}
