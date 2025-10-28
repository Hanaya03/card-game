using UnityEngine;

public class WizardCard : BCard
{
    void Awake()
    {
        this._value = 10;
        this._name = "Ancient Arch Wizard";
        this._moving = true;
    }
}
