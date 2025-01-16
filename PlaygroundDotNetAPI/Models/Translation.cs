using System.ComponentModel.DataAnnotations;

namespace PlaygroundDotNetAPI.Models;

public class TranslationItemResponse
{

    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public required string Text { get; set; }

    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public dynamic Translated { get; set; }
}

public class TranslationItemSource
{
    public int framework { get; set; }
    public string en { get; set; }
    public string cn { get; set; }
    public string tw { get; set; }
    public string ja { get; set; }
    public string ko { get; set; }
    public string id { get; set; }
    public string th { get; set; }
    public string vi { get; set; }
    public string pt { get; set; }
    public string es { get; set; }
    public string ru { get; set; }
    public string fr { get; set; }
    public string sv { get; set; }
    public string de { get; set; }
    public string fi { get; set; }
    public string no { get; set; }
    public string tr { get; set; }
    public string lv { get; set; }
    public string et { get; set; }
    public string lt { get; set; }
    public string pl { get; set; }
    public string nl { get; set; }
    public string ro { get; set; }
    public string gp { get; set; }
    public string sx { get; set; }
    public string el { get; set; }
    public string ys { get; set; }
}