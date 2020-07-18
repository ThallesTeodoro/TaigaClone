using System.Globalization;
using System.Text;

namespace Taiga.Api.Utilities
{
    public static class StringFormat
    {
        /// <summary>
        /// Remove accents from a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveAccents(string text)
        {
            StringBuilder sbRetrun = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormKD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbRetrun.Append(letter);
            }

            return sbRetrun.ToString();
        }
    }
}
