using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "ScriptableObjects/DeckScriptableObject", order = 1)]
public class DeckScriptableObject : ScriptableObject
{
    public GameObject[] Deck;
    public GameObject[] Hand;
}
