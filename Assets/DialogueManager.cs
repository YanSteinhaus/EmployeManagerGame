using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DialogueCanva;

    [SerializeField]
    private TextMeshProUGUI Name;
    private void OnTriggerEnter(Collider employee)
    {

        Name.text = employee.GetComponent<NpcInfo>().Employee.employeeName;
        DialogueCanva.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        DialogueCanva.SetActive(false);
    }
}
