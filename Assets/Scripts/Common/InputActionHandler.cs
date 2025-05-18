using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class InputActionHandler : MonoBehaviour
{
    [SerializeField]
    PlayerInput player_input;

    //アクションマップ、アクション毎に登録しているコールバックを管理
    Dictionary<string, Dictionary<string, System.Action<InputAction.CallbackContext>>> current_callbacks = new Dictionary<string, Dictionary<string, System.Action<InputAction.CallbackContext>>>();

    /// <summary>
    /// 使用するActionMapを切り替える
    /// </summary>
    /// <param name="action_map">
    /// 有効にするActionMap
    /// </param>
    public void SwitchActionMap(string action_map)
    {
        InputActionMap map = player_input.actions.FindActionMap(action_map);

        if (map == null)
        {
            Debug.LogError($"ActionMap '{action_map}' は存在しません。");
            return;
        }
        player_input.SwitchCurrentActionMap(action_map);
    }

    /// <summary>
    /// 現在のActionMapの指定Actinoに
    /// コールバックを追加
    /// ※原則1アクション = 1コールバック
    /// </summary>
    /// <param name="action_name">
    /// Actionsの名前
    /// </param>
    /// <param name="callback">
    /// 登録するコールバック
    /// </param>
    public void SetCallback(string action_name, System.Action<InputAction.CallbackContext> callback)
    {
        InputAction action = player_input.actions.FindAction(action_name, false);
        //アクションがない場合
        if (action == null)
        {
            Debug.LogWarning($"アクション '{action_name}' は存在しません。");
            return;
        }

        string current_action_map = player_input.currentActionMap.name;

        //アクションマップのキーがない場合
        if (!current_callbacks.ContainsKey(current_action_map))
        {
            current_callbacks[current_action_map] = new Dictionary<string, System.Action<InputAction.CallbackContext>>();
        }

        Dictionary<string, System.Action<InputAction.CallbackContext>> callbacksForMap = current_callbacks[current_action_map];

        //コールバック登録済みの場合
        if (callbacksForMap.TryGetValue(action_name, out var existingCallback))
        {
            action.performed -= existingCallback;
        }

        action.performed += callback;
        current_callbacks[current_action_map][action_name] = callback; 
    }
}
