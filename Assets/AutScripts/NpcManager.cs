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
        // For Testing
        //if (Input.GetKeyUp(KeyCode.Alpha1))
        //{
        //    SelectEmployeeByID(0);
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha2))
        //{
        //    SelectEmployeeByID(1);
        //}

        //if (Input.GetKeyUp(KeyCode.E))
        //{
        //    SelectRamdomEmployee();
        //}
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

            NpcInfo npcInfo = employee.GetComponent<NpcInfo>();
            if (npcInfo == null || npcInfo.Employee == null)
            {
                Debug.LogWarning($"O objeto {employee.name} não tem um EmployeeData atribuído!");
                continue;
            }

            if (npcInfo.Employee.employeeName == name)
            {
                employee.SetActive(true);
                employee.GetComponent<NpcMovement>()?.ComeToRoom();
                Debug.Log($"{npcInfo.Employee.employeeName} foi ativado e vai para a mesa!");
            }
            else
            {
                employee.SetActive(false);
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

            if (npcInfo == null || npcInfo.Employee == null)
            {
                Debug.LogWarning($"O objeto {employee.name} não tem EmployeeData atribuído!");
                continue;
            }
            if (npcInfo.Employee.employeeID == ID)
            {
                employee.SetActive(true);
                employee.GetComponent<NpcMovement>()?.ComeToRoom();
                Debug.Log($"{npcInfo.Employee.employeeName} is active at ID:{ID}");
            }
            else
            {
                employee.SetActive(false);
            }
        }
    }

    public void SelectRamdomEmployee()
    {
        int randomID = Random.Range(0, employees.Length);
        SelectEmployeeByID(randomID);
        Debug.Log($"Rand id:{randomID}");
    }

    public GameObject GetActiveNpc()
    {
        foreach (var employee in employees)
        {
            if (employee != null && employee.activeInHierarchy)
                return employee;
        }
        return null;
    }
}
