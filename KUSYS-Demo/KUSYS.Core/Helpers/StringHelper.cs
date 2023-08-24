namespace KUSYS.Core.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns> <see cref="[span] Value1 [/span] [span] Value2 [/span] ...."/>  /></returns>
        public static string CastOfSpans(this IEnumerable<string> values)
        {
            if (values.Count() == 1)
                return $"<span class='badge bg-light text-dark'>{values.First()}</span>";
            return values.Aggregate((b, a) => $"<span class='badge bg-light text-dark'>{b}</span> <span class='badge bg-light text-dark'>{a}</span>");
        }
    }
}
