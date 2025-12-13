namespace UJAS.Core.Entities.System
{
    public class SystemSettings : BaseEntity
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsEditable { get; set; } = true;
    }
}