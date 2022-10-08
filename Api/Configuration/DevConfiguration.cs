namespace Api.Configuration;

public class DevConfiguration
{
    public Dictionary<string, string?> Configuration { get; set; } = new Dictionary<string, string?>();

    public DevConfiguration()
    {
        var projectDirectory = Environment.CurrentDirectory;
        var solutionDirectory = Directory.GetParent(projectDirectory)?.FullName;
        string[] lines = File.ReadAllLines($"{solutionDirectory}/.env");
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var splitIndex = line.IndexOf("=");
            if (splitIndex == -1) continue;
            var pair = new string[2] {line[..splitIndex], line[(splitIndex+1)..] };
            if (pair.Length != 2) continue;
            if (string.IsNullOrWhiteSpace(pair.ElementAtOrDefault(0))) continue;
            if (string.IsNullOrWhiteSpace(pair.ElementAtOrDefault(1))) continue;
            Configuration.Add(pair[0], pair[1]);
        }
    }
}