using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private GameObject[] employees; // Lista de NPCs

    private void Start()
    {
        SelectRamdomEmployee();
    }
    void Update()
    {

        //For Testing
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SelectEmployeeByID(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectEmployeeByID(1);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            SelectRamdomEmployee();
        }
    }

    public void SelectEmployeeByName(string name)
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

    public void SelectEmployeeByID(int ID)
    {

        foreach (var employee in employees)
        {
            if (employee == null)
            {
                Debug.LogWarning("Null employee Detected");
                continue;
            }
            NpcInfo npcInfo = employee.GetComponent<NpcInfo>();

            if(npcInfo == null || npcInfo.Employee == null)
            {
                Debug.LogWarning($"O objeto {employee.name} não tem EmployeeData atrinuido!");
                continue;
            }
            if (npcInfo.Employee.employeeID == ID)
            {
                employee.SetActive(true);
                Debug.Log($"{npcInfo.Employee.employeeName} is active at ID:{ID}");
            }
            else
            {
                employee.SetActive(false);
                Debug.Log($"{npcInfo.Employee.employeeName} is unactivaded at ID:{ID}");
            }



        }

    }

    public void SelectRamdomEmployee()
    {
       int ramndomID = Random.Range(0,employees.Length);

        SelectEmployeeByID(ramndomID);
        Debug.Log($"Rand id:{ramndomID}");

    }
}
