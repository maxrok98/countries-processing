namespace CountriesProcessing.Models {
  public class NameInfo {
    public string Common { get; set; }
    public string Official { get; set; }
    public Dictionary<string, NativeNameInfo> NativeName { get; set; }
  }
}
