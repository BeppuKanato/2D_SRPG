using UnityEngine;

public class Thicket : Tile
{
    public override bool GetCanMove(int unit_class)
    {
        return true;
    }
}
