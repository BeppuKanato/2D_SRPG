using UnityEngine;

public class Water : Tile
{
    public override bool GetCanMove(int unit_class)
    {
        return true;
    }
}
