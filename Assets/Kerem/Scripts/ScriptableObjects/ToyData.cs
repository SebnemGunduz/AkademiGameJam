using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewToyData", menuName = "Toy/ToyData")]
public class ToyData : ScriptableObject
{
    public string toyName;
    public GameObject[] requiredTools;     // Doğru sıralı aletler
    public Sprite toyImage;                // Panelde göstermek istersen
    //public TextMeshProUGUI[] toolHints;             // Opsiyonel: açıklamalar
    public Sprite toyFixedImage;
}