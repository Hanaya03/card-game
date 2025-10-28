using UnityEngine;

public class RexCard : BCard
{
    void Awake()
    {
        this._value = 10;
        this._name = "King Rex";
        this._moving = true;
    }
}
