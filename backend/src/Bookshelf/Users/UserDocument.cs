namespace Bookshelf.Users;

public class UserDocument
{
    public const string CollectionName = "Users";
    
    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static string DocumentId(User user) => $"{CollectionName}/{user.Id}";
    public static string DocumentId(UserId id) => $"{CollectionName}/{id}";
    public static string DocumentId(string id) => $"{CollectionName}/{id}";
}
