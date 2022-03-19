// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, User!");

string? filePath = null;
while (filePath == null)
{
    Console.WriteLine("Paste JSON file's path, like: C:\\Documents\\bruno.dimauro\\database.json");

    filePath = Console.ReadLine();
}

try
{
    using HttpClient client = new HttpClient();
    client.BaseAddress = new Uri("https://localhost:7243");

    byte[] data;
    using FileStream stream = new FileStream(filePath, FileMode.Open);
    using BinaryReader br = new BinaryReader(stream);
    data = br.ReadBytes((int)stream.Length);

    ByteArrayContent bytes = new ByteArrayContent(data);

    MultipartFormDataContent multiContent = new MultipartFormDataContent();
    multiContent.Add(bytes, "file", stream.Name);

    HttpResponseMessage response = await client.PostAsync("/api/import/product", multiContent);
    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine("Fail to upload database file");
    }
    else
    {
        Console.WriteLine("Database uploaded successfully!");
    }
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}