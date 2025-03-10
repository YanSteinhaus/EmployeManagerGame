using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "HRGame/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string question;

    public int maxRespect;
    public int minRespect;

    public int maxProdutivity;
    public int minProdutivity;

    public int maxHappiness;
    public int minHappiness;

    public bool avable;


}
