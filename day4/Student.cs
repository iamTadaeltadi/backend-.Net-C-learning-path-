public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public readonly int RoleNumber;
    public int Grade { get; set; }

    public Student(int roleNumber)
    {
        RoleNumber = roleNumber;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, RoleNumber: {RoleNumber}, Grade: {Grade}";
    }
}
