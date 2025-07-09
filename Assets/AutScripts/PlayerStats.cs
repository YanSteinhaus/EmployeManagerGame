using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int neuroticism = 0;
    public int agreeableness = 0;
    public int extraversion = 0;
    public int conscientiousness = 0;
    public int openness = 0;

    public void ApplyAnswer(AnswerData answer)
    {
        neuroticism += answer.neuroticismPoints;
        agreeableness += answer.agreeablenessPoints;
        extraversion += answer.extraversionPoints;
        conscientiousness += answer.conscientiousnessPoints;
        openness += answer.opennessPoints;

        Debug.Log($"[PlayerStats Atualizado]");
        Debug.Log($"Neuroticismo: {neuroticism}");
        Debug.Log($"Amabilidade: {agreeableness}");
        Debug.Log($"Extroversão: {extraversion}");
        Debug.Log($"Conscienciosidade: {conscientiousness}");
        Debug.Log($"Abertura: {openness}");
    }
}