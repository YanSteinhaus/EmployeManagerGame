using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button[] answerButtons = new Button[4];
    [SerializeField] private PlayerStats playerStats;

    private string fullText;
    private Coroutine typingCoroutine;
    private DialogueData currentDialogue;
    private List<DialogueData> availableDialogues;

    private GameObject currentNpc;  // Guarda o NPC dentro do trigger

    private void Start()
    {
        dialogueCanvas.SetActive(false);
        Debug.Log("DialogueManager iniciado. Canvas escondido.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter chamado com {other.gameObject.name}");

        var npcInfo = other.GetComponent<NpcInfo>();
        if (npcInfo != null && npcInfo.Employee != null)
        {
            Debug.Log($"NPC válido detectado: {npcInfo.Employee.employeeName}");
            currentNpc = other.gameObject;

            // Mostra nome do NPC
            nameText.text = npcInfo.Employee.employeeName;
            Debug.Log($"Nome do NPC mostrado: {nameText.text}");

            // Pega os diálogos disponíveis no mesmo objeto (mesa)
            var availableDialoguesComponent = GetComponent<AvableDialogues>();
            if (availableDialoguesComponent == null)
            {
                Debug.LogError("Objeto da mesa não tem AvableDialogues!");
                return;
            }

            var allDialogues = availableDialoguesComponent.DialogueData;
            Debug.Log($"Quantidade de diálogos disponíveis: {allDialogues.Length}");

            availableDialogues = new List<DialogueData>(allDialogues);

            StartRandomDialogue();

            dialogueCanvas.SetActive(true);
            Debug.Log("Canvas do diálogo ativado.");
        }
        else
        {
            Debug.Log("Colidiu com objeto sem NpcInfo válido.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit chamado com {other.gameObject.name}");

        if (other.gameObject == currentNpc)
        {
            Debug.Log("NPC saiu do trigger, desativando UI e limpando estado.");
            dialogueCanvas.SetActive(false);
            currentNpc = null;

            if (typingCoroutine != null)
            {
                Debug.Log("Parando corrotina de digitação.");
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
        }
    }

    public void StartRandomDialogue()
    {
        if (availableDialogues == null || availableDialogues.Count == 0)
        {
            Debug.LogWarning("Todas as perguntas já foram usadas para este NPC.");
            return;
        }

        int rand = Random.Range(0, availableDialogues.Count);
        currentDialogue = availableDialogues[rand];
        availableDialogues.RemoveAt(rand);

        Debug.Log($"Selecionado diálogo: {currentDialogue.question}");

        fullText = currentDialogue.question;

        foreach (var btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        if (typingCoroutine != null)
        {
            Debug.Log("Parando corrotina de digitação existente.");
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = "";
        Debug.Log("Iniciando digitação do texto.");

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        Debug.Log("Texto digitado completamente.");
        ShowAnswerButtons();
    }

    private void ShowAnswerButtons()
    {
        if (currentDialogue.answers == null || currentDialogue.answers.Length != 4)
        {
            Debug.LogError("O diálogo não tem exatamente 4 respostas!");
            return;
        }

        Debug.Log("Mostrando botões de resposta.");

        for (int i = 0; i < 4; i++)
        {
            int index = i;
            var answerData = currentDialogue.answers[index];

            answerButtons[i].gameObject.SetActive(true);

            var textComponent = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = answerData.answerText;
                Debug.Log($"Botão {i} configurado com texto: {answerData.answerText}");
            }

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerData));
        }
    }

    private void OnAnswerSelected(AnswerData selectedAnswer)
    {
        Debug.Log("Resposta escolhida: " + selectedAnswer.answerText);
        Debug.Log($"Neuroticismo: {selectedAnswer.neuroticismPoints}");
        Debug.Log($"Amabilidade: {selectedAnswer.agreeablenessPoints}");
        Debug.Log($"Extroversão: {selectedAnswer.extraversionPoints}");
        Debug.Log($"Conscienciosidade: {selectedAnswer.conscientiousnessPoints}");
        Debug.Log($"Abertura: {selectedAnswer.opennessPoints}");

        foreach (var btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        if (playerStats != null)
        {
            Debug.Log("Aplicando pontos ao jogador.");
            playerStats.ApplyAnswer(selectedAnswer);
        }
        else
        {
            Debug.LogWarning("PlayerStats não está atribuído!");
        }

        if (currentNpc != null)
        {
            var npcMovement = currentNpc.GetComponent<NpcMovement>();
            if (npcMovement != null)
            {
                Debug.Log("NPC está indo embora.");
                npcMovement.LeaveRoom();
            }
            else
            {
                Debug.LogWarning("NPC não tem componente NpcMovement!");
            }
        }
        else
        {
            Debug.LogWarning("currentNpc é null no momento da saída do NPC.");
        }
    }
}
