namespace SCADA.Frontend.ViewModels;

public class DashboardViewModel
{
    // System Overview
    public int ActivePLCs { get; set; }
    public int TotalTags { get; set; }
    public int ActiveAlarms { get; set; }

    // Real-Time Components
    public List<PLCLiveStatus> PLCStatuses { get; set; } = new();
    public List<TagAlert> CriticalTags { get; set; } = new();
    public List<ActiveAlarm> ActiveAlarmsList { get; set; } = new();

    // Nested classes for organization
    public class PLCLiveStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsConnected { get; set; }
        public DateTime LastPollTime { get; set; }
    }

    public class TagAlert
    {
        public int TagId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PLCName { get; set; } = string.Empty;
        public object? CurrentValue { get; set; }
        public string DataType { get; set; } = string.Empty;
    }

    public class ActiveAlarm
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime TriggeredAt { get; set; }
        public string Severity { get; set; } = string.Empty; // "Critical", "Warning", etc.
    }
}