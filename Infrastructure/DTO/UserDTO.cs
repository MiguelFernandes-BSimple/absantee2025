public class CreateUserDto
{
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public DateTime? DeactivationDate { get; set; }
}