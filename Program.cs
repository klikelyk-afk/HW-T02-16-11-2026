using PvStudentsWebApi;
using System.Net;
using System.Text.Json;

var dbContext = new AppDbContext();
dbContext.Database.EnsureCreated();

// camelCase PascalCase snake_case kebab-case

var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

// Об'єкт, який буде слухати запити по мережі до цього серверу
var server = new HttpListener();
server.Prefixes.Add("http://+:5000/students/");
server.Start();

// Бескінечний цикл обробки вхідних запитів
while (true)
{
    // Потік зависає на цьому моменті і чекає поки хтось зверенться до сервера
    // Все дані про зверення знаходяться в об'єкті httpContext
    var httpContext = server.GetContext();

    // response - об'єкт, через котрий формується відповідь сервера
    var response = httpContext.Response;

    response.Headers.Add("Content-Type", "application/json");
    response.StatusCode = 200;

    var queryParams = httpContext.Request.QueryString;
    IQueryable<Student> studentsQuery = dbContext.Students.AsQueryable();

    var maxAgeRaw = queryParams["maxAge"];
    if (!string.IsNullOrWhiteSpace(maxAgeRaw) && int.TryParse(maxAgeRaw, out var maxAge))
    {
        studentsQuery = studentsQuery.Where(s => s.Age <= maxAge);
    }

    var nameRaw = queryParams["name"];
    if (!string.IsNullOrWhiteSpace(nameRaw))
    {
        studentsQuery = studentsQuery.Where(s => s.Name.Contains(nameRaw, StringComparison.OrdinalIgnoreCase));
    }

    var allStudents = studentsQuery.ToList();

    using var outputStream = new StreamWriter(response.OutputStream);
    outputStream.WriteLine(JsonSerializer.Serialize(allStudents, jsonOptions));
    outputStream.Flush();
}