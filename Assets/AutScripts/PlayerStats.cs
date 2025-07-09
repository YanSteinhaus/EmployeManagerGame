using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int neuroticismPoints;
    private int agreeablenessPoints;
    private int extraversionPoints;
    private int conscientiousnessPoints;
    private int opennessPoints;

    public void ApplyAnswer(AnswerData answer)
    {
        neuroticismPoints += answer.neuroticismPoints;
        agreeablenessPoints += answer.agreeablenessPoints;
        extraversionPoints += answer.extraversionPoints;
        conscientiousnessPoints += answer.conscientiousnessPoints;
        opennessPoints += answer.opennessPoints;

        Debug.Log("Pontuações atualizadas:");
        Debug.Log($"Neuroticismo: {neuroticismPoints}");
        Debug.Log($"Amabilidade: {agreeablenessPoints}");
        Debug.Log($"Extroversão: {extraversionPoints}");
        Debug.Log($"Conscienciosidade: {conscientiousnessPoints}");
        Debug.Log($"Abertura: {opennessPoints}");
    }

    // Propriedades de leitura para exibir os pontos no placar final
    public int Neuroticism => neuroticismPoints;
    public int Agreeableness => agreeablenessPoints;
    public int Extraversion => extraversionPoints;
    public int Conscientiousness => conscientiousnessPoints;
    public int Openness => opennessPoints;
}
