namespace SCADA.Frontend.ViewModels;

public class TagViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public object? LastValue { get; set; }
    public int PlcId { get; set; }

    // For configuration
    public bool LogHistory { get; set; }
    public int HistoryInterval { get; set; }
    public bool HasAlarm { get; set; }
    public string? AlarmCondition { get; set; }
    public double? AlarmThreshold { get; set; }
    public string? AlarmMessage { get; set; }
}