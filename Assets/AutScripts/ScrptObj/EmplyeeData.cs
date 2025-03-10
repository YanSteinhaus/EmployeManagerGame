using UnityEngine;

[CreateAssetMenu(fileName = "NewEmployee", menuName = "HRGame/Employee")]
public class EmployeeData : ScriptableObject
{
    public string employeeName;
    public int employeeID;

    public int respect;
    public int produtivity;
    public int happiness;
    
}
