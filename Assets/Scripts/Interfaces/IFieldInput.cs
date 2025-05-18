using UnityEngine;
using UnityEngine.InputSystem;
public interface IFieldInput
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize();
    /// <summary>
    /// 処理をコールバックとして登録
    /// </summary>
    void SetProcesses();
    /// <summary>
    /// 移動、カーソルなどの処理
    /// </summary>
    void OnNavigate(InputAction.CallbackContext context);
    /// <summary>
    /// 決定機能の処理
    /// </summary>
    void OnDecide(InputAction.CallbackContext context);
    /// <summary>
    /// キャンセル処理
    /// </summary>
    void OnCancel(InputAction.CallbackContext context);
}
