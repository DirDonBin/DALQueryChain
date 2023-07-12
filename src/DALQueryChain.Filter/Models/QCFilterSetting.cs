namespace DALQueryChain.Filter.Models
{
    public record QCFilterSetting
    {
        public bool NullValueIgnore { get; set; } = false;
        public bool StringSensitiveCase { get; set; } = true;
    }
}
