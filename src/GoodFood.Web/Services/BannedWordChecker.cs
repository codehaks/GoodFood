namespace GoodFood.Web.Services;

public class BannedWordChecker
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BannedWordChecker(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> CheckForBannedWordAsync(string input)
    {
        var usernameContainsBannedWord = false;

        var bannedWordFilePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "Files", "banned-words.txt");
        if (System.IO.File.Exists(bannedWordFilePath))
        {
            var bannedWords = await System.IO.File.ReadAllLinesAsync(bannedWordFilePath);
            if (bannedWords.Any(b => input.Contains(b, StringComparison.OrdinalIgnoreCase)))
            {
                usernameContainsBannedWord = true;
            }
        }

        return usernameContainsBannedWord;
    }
}
