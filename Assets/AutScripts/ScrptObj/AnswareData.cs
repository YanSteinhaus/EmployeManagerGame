using UnityEngine;

[CreateAssetMenu(fileName = "NewAnsware", menuName = "HRGame/Answare")]
public class AnswareData : ScriptableObject
{
    public string answare;

    public int respectConsequence;
    public OperationType respectOperation;

    public int produtivityConsequence;
    public OperationType produtivityOperation;

    public int happinessConsequence;
    public OperationType happinessOperation;

    public enum OperationType
    {
        Plus,
        Minus,
        Times,
        Divide
    }
}
