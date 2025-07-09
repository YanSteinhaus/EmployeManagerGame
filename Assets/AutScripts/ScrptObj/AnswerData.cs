using UnityEngine;

[CreateAssetMenu(fileName = "NewAnswer", menuName = "HRGame/Answer")]
public class AnswerData : ScriptableObject
{
    public string answerText;

    public int neuroticismPoints;
    public int agreeablenessPoints;
    public int extraversionPoints;
    public int conscientiousnessPoints;
    public int opennessPoints;
}
