using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public class SlugHelper
{
    private readonly Config _config;

    public SlugHelper() : this(new Config())
    {
    }

    public SlugHelper(Config config)
    {
        if (config != null)
            _config = config;
        else
            throw new ArgumentNullException(nameof(config), "can't be null use default config or empty construct.");
    }

    public string GenerateSlug(string str) => GenerateSlug(str, CultureInfo.CurrentUICulture);

    public string GenerateSlug(string str, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;

        str = str.Trim();
        if (_config.ForceLowerCase)
            str = str.ToLower(cultureInfo);

        str = CleanWhiteSpace(str, _config.CollapseWhiteSpace);
        str = ApplyReplacements(str, _config.CharacterReplacements);
        str = RemoveDiacritics(str);
        str = DeleteCharacters(str, _config.DeniedCharactersRegex);

        return str;
    }

    private static string CleanWhiteSpace(string str, bool collapse) => Regex.Replace(str, collapse ? @"\s+" : @"\s", " ");

    private static string RemoveDiacritics(string str)
    {
        var stFormD = str.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var t in stFormD)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(t);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    private static string ApplyReplacements(string str, Dictionary<string, string> replacements)
    {
        var sb = new StringBuilder(str);

        foreach (var (key, value) in replacements)
            sb.Replace(key, value);

        return sb.ToString();
    }

    private static string DeleteCharacters(string str, string regex) => Regex.Replace(str, regex, string.Empty);

    public class Config
    {
        public Dictionary<string, string> CharacterReplacements { get; set; }
        public bool ForceLowerCase { get; set; }
        public bool CollapseWhiteSpace { get; set; }
        public string DeniedCharactersRegex { get; set; }

        public Config()
        {
            CharacterReplacements = new Dictionary<string, string>();
            CharacterReplacements.Add(" ", "-");

            ForceLowerCase = true;
            CollapseWhiteSpace = true;
            // DeniedCharactersRegex = @"[^a-zA-Z0-9\-\._]";
            DeniedCharactersRegex = @"[^\w\-]*";
        }
    }
}