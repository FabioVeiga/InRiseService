namespace InRiseService.Domain.Enums
{
    public enum EnumProfile
    {
        [Description("Admin")]
        Admin = 1,

        [Description("User")]
        User = 2
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}