using System.ComponentModel.DataAnnotations;

namespace SCADA.Common.ViewModels.Tags;

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    // public string DataType { get; set; } = string.Empty;
    public Enums.DataType DataType { get; set; }
    public string DataTypeDisplay => DataType.ToString();
    public int PlcId { get; set; } // Foreign key only
}