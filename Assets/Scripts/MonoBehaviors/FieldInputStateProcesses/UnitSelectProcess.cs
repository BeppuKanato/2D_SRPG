using UnityEngine;
using UnityEngine.InputSystem;
public class UnitSelectProcess :MonoBehaviour ,IFieldInput
{
    [SerializeField]
    InputActionHandler input_action_handler;
    [SerializeField]
    MapManager map_manager;
    [SerializeField]
    UnitManager unit_manager;
    [SerializeField]
    GameObject cursol;
    Vector2Int grid_corsol_pos;

    public void Initialize()
    {
        //仮のカーソル初期化
        UpDateCursolPos(new Vector2Int(0, 0));
        SetProcesses();
    }
    public void SetProcesses()
    {
        input_action_handler.SetCallback("stick_move", OnNavigate);
        input_action_handler.SetCallback("pad_move", OnNavigate);
    }

    /// <summary>
    /// カーソル移動処理
    /// </summary>
    /// <param name="context"></param>
    public void OnNavigate(InputAction.CallbackContext context)
    {
        InputControl control = context.control;

        switch (control.name)
        {
            case "up":
                UpDateCursolPos(new Vector2Int(grid_corsol_pos.x, grid_corsol_pos.y - 1));
                Debug.Log("上にスティックを入力");
                break;
            case "down":
                UpDateCursolPos(new Vector2Int(grid_corsol_pos.x, grid_corsol_pos.y + 1));
                Debug.Log("下にスティックを入力");
                break;
            case "left":
                UpDateCursolPos(new Vector2Int(grid_corsol_pos.x - 1, grid_corsol_pos.y));
                Debug.Log("左にスティックを入力");
                break;
            case "right":
                UpDateCursolPos(new Vector2Int(grid_corsol_pos.x + 1, grid_corsol_pos.y));
                Debug.Log("右にスティックを入力");
                break;
        }
    }

    public void OnDecide(InputAction.CallbackContext context)
    {

    }

    public void OnCancel(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// カーソルの座標を更新する
    /// </summary>
    /// <param name="new_grid_pos"></param>
    private void UpDateCursolPos(Vector2Int new_grid_pos)
    {
        //移動先の座標取得
        Tile tile = map_manager.GetTileData(new_grid_pos);
        
        //範囲外だった場合
        if (tile == null)
        {
            Debug.Log("範囲外です");
            return;
        }

        Vector2 pixel_pos = tile.gameObject.transform.position;

        cursol.transform.position = pixel_pos;
        grid_corsol_pos = new_grid_pos;
    }
}
