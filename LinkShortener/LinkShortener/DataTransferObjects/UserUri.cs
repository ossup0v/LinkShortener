using System;

namespace LinkShortener.DataTransferObjects
{
	public sealed class UserUri
  {
    public static string ShortUriPrefix = "https://localhost:";
    public string Id { get; set; }
    public string FullUri { get; set; }
    public string ShortUri { get; set; }
    public string Token { get; set; }
    public int ClickCounter { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Creator { get; set; }
  }
}
