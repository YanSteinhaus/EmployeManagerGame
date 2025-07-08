using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialogueCanva;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Dialogue;

    [SerializeField] private Button[] answareBT;

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
        SetAnswers(selectedDialogue,selectedDialogue.answares.);
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

    private void SetAnswers(DialogueData dialogue)
    {
        for (int i = 0; i < answareBT.Length; i++)
        {
            answareBT[i].gameObject.SetActive(true);
            TextMeshProUGUI btText = answareBT[i].GetComponentInChildren<TextMeshProUGUI>();
            btText.text = dialogue.answares[i].answare;

            int index = i;
            answareBT[i].onClick.RemoveAllListeners();
            answareBT[i].onClick.AddListener(()=> ApplyAnswareConsequence(dialogue ));
        }
    }
    public void ApplyAnswareConsequence(DialogueData dialogue)
    {//, NpcInfo[] npcInfo, AnswareData answare

        for (int c = 0; c<dialogue.answares.Length;c++)
        {//AnswareData answare in dialogue.answares
            int indice = c;

            foreach (NpcInfo npc in dialogue.answares[indice].npcAfected) 
            { 
            AnswareConsequence(npc.Employee.respect, dialogue.answares[indice].respectConsequence, dialogue.answares[indice].respectOperation);
            AnswareConsequence(npc.Employee.produtivity, dialogue.answares[indice].produtivityConsequence, dialogue.answares[indice].produtivityOperation);
            AnswareConsequence(npc.Employee.happiness, dialogue.answares[indice].happinessConsequence, dialogue.answares[indice].happinessOperation);
            }
        }
    }

    public void AnswareConsequence(int stat, int value, AnswareData.OperationType operation)
    {
        switch (operation)
        {
            case AnswareData.OperationType.Plus:
                stat += value;
                break;
            case AnswareData.OperationType.Minus:
                stat -= value;
                break;
            case AnswareData.OperationType.Times:
                stat *= value;
                break;
            case AnswareData.OperationType.Divide:
                if (value != 0) stat /= value;
                break;
        }
    }
    private bool ValidarDialogo(DialogueData dialogue, NpcInfo npcInfo)
    {
        return npcInfo.Employee.happiness >= dialogue.minHappiness && npcInfo.Employee.happiness <= dialogue.maxHappiness &&
               npcInfo.Employee.produtivity >= dialogue.minProdutivity && npcInfo.Employee.produtivity <= dialogue.maxProdutivity &&
               npcInfo.Employee.respect >= dialogue.minRespect && npcInfo.Employee.respect <= dialogue.maxRespect;
    }
}
