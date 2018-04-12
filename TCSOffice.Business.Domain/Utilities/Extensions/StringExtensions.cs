using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumber(this string s)
        {
            int n;
            bool isNumeric = int.TryParse(s, out n);

            return isNumeric;
        }

        /// <summary>
        /// This will convert a number eg '123' to the int 123
        /// It will throw an error if the string is NOT a number.
        /// As such check if its a number first by calling .IsNumber()
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToNumber(this string s)
        {
            int n;
            bool isNumeric = int.TryParse(s, out n);

            if (!isNumeric)
                throw new Exception("The string you have passed is NOT a number: " + s);

            return n;
        }

        /// <summary>
        /// Removes an array of chars/strings from a string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charsToRemove"></param>
        /// <returns></returns>
        public static string RemoveFromString(this string s, string[] charsToRemove)
        {
            foreach (var item in charsToRemove)
            {
                s = s.Replace(item, "");
            }

            return s;
        }

        /// <summary>
        /// removes anything that is not a number or not a letter from a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveNonAlphaNumericChars(this string s)
        {
            char[] arr = s.ToCharArray();

            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c))));
            s = new string(arr);
            return s;
        }

        /// <summary>
        /// removes all white spaces from a string including start and finish spaces
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveSpaces(this string s)
        {
            return s.Trim().Replace(" ", "");
        }

        /// <summary>
        /// snakeCase will remove all spaces and join the words up with the firstletter being small
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TosnakeCase(this string s)
        {
            return s.ToCamelCase();
        }

        public static string ToCamelCase(this string s)
        {
            var firstLetter = s.Substring(0, 1);
            var lowerFirstLetter = firstLetter.ToLower();

            var output = lowerFirstLetter + s.Substring(1);
            output.RemoveSpaces();

            return output;
        }

        public static string Pluralised(this string s)
        {
            if (s.EndsWith("s")) return s + "es";

            //category, opportunity
            if (s.EndsWith("ry") || s.EndsWith("ty"))
            {
                var legnth = s.Length;
                s = s.Substring(0, legnth - 1); //drop y
                return s + "ies";
            }

            return s + "s";
        }

        /// <summary>
        /// will convert dlMbAccountDetail to dl-mb-account-detail
        /// </summary>
        /// <param name="camelCasedText"></param>
        /// <returns></returns>
        public static string ToHtmlAttributeNamingConvention(this string camelCasedText)
        {
            var dashSeparatedCamelCase = camelCasedText.TransformCamelCase("-");
            var lowered = dashSeparatedCamelCase.ToLower();
            return lowered;
        }

        /// <summary>
        /// This will take CamelCase and return Camel[separator]Case
        /// The default is a space
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separator"></param>
        /// <param name="ignoreFirstLetter">this will leave the first letter untouched.. usually the beginning of the line</param>
        /// <returns></returns>
        public static string TransformCamelCase(this string s, string separator = " ", bool ignoreFirstLetter = true)
        {
            if (!ignoreFirstLetter)
                return Regex.Replace(s, "[A-Z]", separator + "$0"); //this will add the separator even before the firstletter if it is a capital

            var firstLetter = s.Substring(0, 1);
            var remainingText = s.Substring(1);
            var formatedText = Regex.Replace(remainingText, "[A-Z]", separator + "$0");
            var fullText = firstLetter + formatedText;
            return fullText;
        }

        /// <summary>
        /// Split a camel case string at the caps
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SplitCamelCase(this string s)
        {
            return Regex.Replace(s, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        public static string[] SplitCsv(this string s)
        {
            var separators = new[] { "," };
            return string.IsNullOrEmpty(s) ? null : s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// converts a string eg querystring to a nameValue collection
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static NameValueCollection ConvertStringToValueCollection(this string s)
        {
            var nvc = new NameValueCollection();

            foreach (string vp in Regex.Split(s, "&"))
            {
                string[] singlePair = Regex.Split(vp, "=");
                if (singlePair.Length == 2)
                {
                    nvc.Add(singlePair[0], singlePair[1]);
                }
            }

            return nvc;
        }

        /// <summary>
        /// converts a string to string[] by splitting at the following {";", ",", ":", " "}
        /// Useful for extracting emails from a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] AdvancedSplit(this string s)
        {
            var separators = new[] { ";", ",", ":", " " };
            return string.IsNullOrEmpty(s) ? null : s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Converts string to array by splitting on carriage return.
        /// Useful for taking large blocks of text and finding the new lines to process
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<string> SplitOnCarriegeReturn(this string s)
        {
            string[] result = s.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return result.ToList();
        }

        ///// <summary>
        ///// Replaces tokens in string. It finds the tokens by splitting the enclosers through the middle.
        ///// </summary>
        ///// <param name="s"></param>
        ///// <param name="dictionary"></param>
        ///// <param name="enclosers"></param>
        ///// <returns></returns>
        //public static string ReplaceTokens(this string s, IEnumerable<KeyValuePair<string, string>> dictionary, string enclosers = "[]")
        //{
        //    //TODO... 1.split enclosers, 2.user stringbuider, 3.implement regex, 4.ignore case
        //    return dictionary.Aggregate(s, (current, item) => current.Replace("[" + item.Key + "]", item.Value));

        //    //MailDefinition md = new MailDefinition();
        //    //md.BodyFileName = pathToTemplate;
        //    //md.From = "test@somedomain.com";

        //    //ListDictionary replacements = new ListDictionary();
        //    //replacements.Add("<%To%>", someValue);
        //    //// continue adding replacements

        //    //MailMessage msg = md.CreateMailMessage("test@someotherdomain.com", replacements, this);
        //    //After this, msg.Body would be created by substituting the values in the template. I guess you can take a look at MailDefinition.CreateMailMessage() with Reflector :).
        //}

        ///// <summary>
        ///// Assumes string is an expression that needs to be evaluated.
        ///// Eg "1+4*20" and returns the result
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //public static object EvaluateExpression(this string s)
        //{
        //    var e = new Expression(s);
        //    return e.Evaluate();
        //}

        /// <summary>
        /// Removes HTML from a string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StripHtml(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            //get rid of HTML tags
            var output = Regex.Replace(source, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return output;
        }

        public static int ComputeDistance(this string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public static bool EqualsApprox(this string s, string t, int expectedMaxDistance)
        {
            var sourceStrings = s.ToLower().AdvancedSplit();
            var targetStrings = t.ToLower().AdvancedSplit();

            if (sourceStrings == null || targetStrings == null)
            {
                return false;
            }

            var m = sourceStrings.Length < targetStrings.Length ? sourceStrings.Length : targetStrings.Length;

            var d = 0;

            for (var i = 0; i < m; i++)
            {
                d += sourceStrings[i].ComputeDistance(targetStrings[i]);
            }

            for (var i = m; i < sourceStrings.Length; i++)
            {
                d += sourceStrings[i].Length;
            }

            for (var i = m; i < targetStrings.Length; i++)
            {
                d += targetStrings[i].Length;
            }

            return d <= expectedMaxDistance;
        }
    }
}
