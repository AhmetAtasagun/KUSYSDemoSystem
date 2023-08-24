namespace KUSYS.Core.Extensions
{
    public static class NumericExtensions
    {
        public static int ToInt(this object value)
        {
            var isConvert = int.TryParse(value.ToString(), out var result);
            return isConvert ? result : 0;
        }
    }
}
