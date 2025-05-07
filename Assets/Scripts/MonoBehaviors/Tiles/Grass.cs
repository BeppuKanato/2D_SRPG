using UnityEngine;

public class Grass : Tile
{
    public override bool GetCanMove(int unit_class)
    {
        return true;
    }
}
