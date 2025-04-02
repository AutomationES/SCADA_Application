namespace SCADA.Common.ViewModels.PLCs;

public class PLCDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public int Port { get; set; }
}