using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private GameObject[] employees; // Lista de NPCs

    void Update()
    {

        //For Testing
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SelectEmployee("Adam");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectEmployee("Eve");
        }
    }

    public void SelectEmployee(string name)
    {
        foreach (var employee in employees)
        {
            if (employee == null)
            {
                Debug.LogWarning("Um dos funcionários na lista está nulo!");
                continue;
            }

            NpcInfo npcInfo = employee.GetComponent<NpcInfo>(); // Obtém o script NpcInfo
            if (npcInfo == null || npcInfo.Employee == null)
            {
                Debug.LogWarning($"O objeto {employee.name} não tem um EmployeeData atribuído!");
                continue;
            }

            Debug.Log($"Verificando {npcInfo.Employee.employeeName}");

            if (npcInfo.Employee.employeeName == name) // Compara o nome correto do ScriptableObject
            {
                employee.SetActive(true); // Ativa apenas o NPC correto
                Debug.Log($"{npcInfo.Employee.employeeName} foi ativado!");
            }
            else
            {
                employee.SetActive(false); // Desativa os outros
                Debug.Log($"{npcInfo.Employee.employeeName} foi desativado!");
            }
        }
    }
}
