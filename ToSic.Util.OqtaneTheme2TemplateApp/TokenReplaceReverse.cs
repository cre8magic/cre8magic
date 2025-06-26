using System.Text.RegularExpressions;
using Oqtane.Infrastructure.Interfaces;
using Oqtane.Interfaces;

namespace ToSic.Util.OqtaneTheme2TemplateApp;

public partial class ThemeTemplateService
{
    public class TokenReplaceReverse : ITokenReplace
    {
        public const string GenericName = "generic";

        private IDictionary<string, IDictionary<string, object>> _tokens;
        // private readonly ILogManager _logger;

        public TokenReplaceReverse(/*ILogManager logger*/)
        {
            _tokens = new Dictionary<string, IDictionary<string, object>>();
            // _logger = logger;
        }

        public void AddSource(ITokenSource source)
        {
            this.AddSource(GenericName, source);
        }

        public void AddSource(Func<IDictionary<string, object>> sourceFunc)
        {
            this.AddSource(GenericName, sourceFunc);
        }

        public void AddSource(IDictionary<string, object> source)
        {
            this.AddSource(GenericName, source);
        }

        public void AddSource(string key, object value)
        {
            this.AddSource(GenericName, key, value);
        }

        public void AddSource(string name, ITokenSource source)
        {
            var tokens = source.GetTokens();
            this.AddSource(name, tokens);
        }

        public void AddSource(string name, Func<IDictionary<string, object>> sourceFunc)
        {
            var tokens = sourceFunc();
            this.AddSource(name, tokens);
        }

        public void AddSource(string name, IDictionary<string, object> source)
        {
            if (source != null)
            {
                foreach (var key in source.Keys)
                {
                    this.AddSource(name, key, source[key]);
                }
            }
        }

        public void AddSource(string name, string key, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = GenericName;
            }

            var source = _tokens.ContainsKey(name.ToLower()) ? _tokens[name.ToLower()] : null;
            if (source == null)
            {
                source = new Dictionary<string, object>();
            }
            source[key] = value;

            _tokens[name.ToLower()] = source;
        }

        public string ReplaceTokens(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            var result = source;

            // Create a list of value-token pairs for replacement
            var replacements = new List<ValueTokenPair>();

            // Collect all values and their corresponding tokens
            foreach (var sourceName in _tokens.Keys)
            {
                var tokenSource = _tokens[sourceName];
                foreach (var key in tokenSource.Keys)
                {
                    var value = tokenSource[key]?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        string tokenName = (sourceName == GenericName) ? $"[{key}]" : $"[{sourceName}:{key}]";
                        replacements.Add(new ValueTokenPair
                        {
                            Value = value,
                            Token = tokenName,
                            Length = value.Length
                        });
                    }
                }
            }

            // Sort by length (longest first) to avoid partial replacements
            // This ensures "MyLongCompanyName" is replaced before "Company"
            foreach (var replacement in replacements.OrderByDescending(r => r.Length))
            {
                if (!string.IsNullOrEmpty(replacement.Value))
                {
                    // Use word boundary to prevent replacing substrings within larger words
                    // For example, replace "Company" but not "MyCompany" when looking for "Company"
                    result = Regex.Replace(
                        result,
                        $"\\b{Regex.Escape(replacement.Value)}\\b",
                        replacement.Token,
                        RegexOptions.CultureInvariant);

                    // For non-word values (paths, etc.), do a direct replacement
                    // This handles values that might contain special characters
                    if (replacement.Value.Contains("\\") || replacement.Value.Contains("/"))
                    {
                        result = result.Replace(replacement.Value, replacement.Token);
                    }
                }
            }

            return result;
        }

        private class ValueTokenPair
        {
            public string Value { get; set; }
            public string Token { get; set; }
            public int Length { get; set; }
        }
    }
}