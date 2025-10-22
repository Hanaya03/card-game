using UnityEngine;

public class PirateCard : BCard
{
    void Awake()
    {
        this._value = 10;
        this._name = "Great Pirate Overlord";
        this._moving = false;
    }
}
