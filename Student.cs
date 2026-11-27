namespace PvStudentsWebApi;

// Сутність (Entity) - клас, описуючий одиницю даних в базі даних
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}