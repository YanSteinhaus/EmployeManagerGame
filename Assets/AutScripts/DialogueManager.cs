using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialogueCanva;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Dialogue;

    private string fullText;
    private Coroutine typingCoroutine;
    [SerializeField] private float typingSpeed = 0.05f; // Velocidade da digitação

    private void OnTriggerEnter(Collider employee)
    {
        Name.text = employee.GetComponent<NpcInfo>().Employee.employeeName;
        DialogueCanva.SetActive(true);
        TakeDialogue();
    }

    private void OnTriggerExit(Collider other)
    {
        DialogueCanva.SetActive(false);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }

    public DialogueData RetunrnSelectedDialogue()

    {
        DialogueData dialogueSelected= null;
       DialogueData[] allDialogues = GetComponent<AvableDialogues>().DialogueData;
        //foreach(DialogueData dialogue in allDialogues)
        //{
        //dialogueSelected = allDialogues[Random.Range(0,allDialogues.Length)];
        //}
        dialogueSelected = allDialogues[Random.Range(0, allDialogues.Length)];
        return dialogueSelected;
    }

    public void TakeDialogue()
    {
        DialogueData selectedDialogue = RetunrnSelectedDialogue();
        //fullText = GetComponent<AvableDialogues>().DialogueData[1].question;
        fullText = selectedDialogue.question;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        Dialogue.text = "";
        foreach (char letter in fullText)
        {
            Dialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Atraso entre letras
        }
    }
}
