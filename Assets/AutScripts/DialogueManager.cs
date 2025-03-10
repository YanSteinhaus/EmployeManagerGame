using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        NpcInfo npcInfo = employee.gameObject.GetComponent<NpcInfo>();
        DialogueCanva.SetActive(true);
        TakeDialogue(npcInfo);
    }

    private void OnTriggerExit(Collider other)
    {
        DialogueCanva.SetActive(false);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }

    public DialogueData RetunrnSelectedRandomDialogue()

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

    public DialogueData RetunrnSelectedDialogue(NpcInfo employee)

    {
        DialogueData dialogueSelected = null;
        DialogueData[] allDialogues = GetComponent<AvableDialogues>().DialogueData;
        foreach(DialogueData dialogue in allDialogues)
        {
        dialogueSelected = allDialogues[Random.Range(0,allDialogues.Length)];

            if (ValidarDialogo(dialogueSelected, employee))
            {
                return dialogueSelected;
            }
        }
        return null;
       
    }

    public void TakeDialogue(NpcInfo employee)
    {
        DialogueData selectedDialogue = RetunrnSelectedDialogue(employee);
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

    private bool ValidarDialogo(DialogueData dialogue, NpcInfo npcInfo)
    {
        return npcInfo.Employee.happiness >= dialogue.minHappiness && npcInfo.Employee.happiness <= dialogue.maxHappiness &&
               npcInfo.Employee.produtivity >= dialogue.minProdutivity && npcInfo.Employee.produtivity <= dialogue.maxProdutivity &&
               npcInfo.Employee.respect >= dialogue.minRespect && npcInfo.Employee.respect <= dialogue.maxRespect;
    }
}
