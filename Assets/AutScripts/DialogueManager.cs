using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button[] answerButtons = new Button[4];
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private GameObject endGamePanel; // Painel de fim de jogo com resultados
    [SerializeField] private TextMeshProUGUI resultText; // Texto com o placar final

    private string fullText;
    private Coroutine typingCoroutine;
    private DialogueData currentDialogue;
    private List<DialogueData> availableDialogues;

    private GameObject currentNpc;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
        endGamePanel?.SetActive(false);
        Time.timeScale = 1f; // Garante que o jogo esteja rodando no início

        var availableDialoguesComponent = GetComponent<AvableDialogues>();
        if (availableDialoguesComponent != null)
        {
            availableDialogues = new List<DialogueData>(availableDialoguesComponent.DialogueData);
            Debug.Log($"Diálogos carregados: {availableDialogues.Count}");
        }
        else
        {
            Debug.LogError("Objeto da mesa não tem AvableDialogues!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var npcInfo = other.GetComponent<NpcInfo>();
        if (npcInfo != null && npcInfo.Employee != null)
        {
            currentNpc = other.gameObject;
            nameText.text = npcInfo.Employee.employeeName;

            if (availableDialogues == null || availableDialogues.Count == 0)
            {
                Debug.Log("Todos os diálogos já foram usados.");
                EndGame();
                return;
            }

            StartRandomDialogue();
            dialogueCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentNpc)
        {
            dialogueCanvas.SetActive(false);
            currentNpc = null;

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
        }
    }

    public void StartRandomDialogue()
    {
        if (availableDialogues == null || availableDialogues.Count == 0)
        {
            Debug.LogWarning("Todas as perguntas foram usadas.");
            EndGame();
            return;
        }

        int rand = Random.Range(0, availableDialogues.Count);
        currentDialogue = availableDialogues[rand];
        availableDialogues.RemoveAt(rand);

        fullText = currentDialogue.question;

        foreach (var btn in answerButtons)
            btn.gameObject.SetActive(false);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = "";

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        ShowAnswerButtons();
    }

    private void ShowAnswerButtons()
    {
        if (currentDialogue.answers == null || currentDialogue.answers.Length != 4)
        {
            Debug.LogError("O diálogo não tem exatamente 4 respostas!");
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            int index = i;
            var answerData = currentDialogue.answers[index];

            answerButtons[i].gameObject.SetActive(true);

            var textComponent = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
                textComponent.text = answerData.answerText;

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerData));
        }
    }

    private void OnAnswerSelected(AnswerData selectedAnswer)
    {
        foreach (var btn in answerButtons)
            btn.gameObject.SetActive(false);

        if (playerStats != null)
            playerStats.ApplyAnswer(selectedAnswer);

        if (currentNpc != null)
        {
            var npcMovement = currentNpc.GetComponent<NpcMovement>();
            if (npcMovement != null)
                npcMovement.LeaveRoom();
        }

        if (availableDialogues.Count == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        dialogueCanvas.SetActive(false);

        if (endGamePanel != null && resultText != null && playerStats != null)
        {
            resultText.text = $"FIM DO JOGO\n\n" +
                              $"Neuroticismo: {playerStats.Neuroticism}\n" +
                              $"Amabilidade: {playerStats.Agreeableness}\n" +
                              $"Extroversão: {playerStats.Extraversion}\n" +
                              $"Conscienciosidade: {playerStats.Conscientiousness}\n" +
                              $"Abertura: {playerStats.Openness}";

            endGamePanel.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
        }
        else
        {
            Debug.Log("Fim do jogo! (Mas painel final não está configurado)");
        }
    }

    // Botão no painel chama essa função para retornar ao menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Despausa
        SceneManager.LoadScene("Menu"); // Troque "Menu" pelo nome real da cena do menu
    }
}
