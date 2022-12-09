namespace Api.Dtos;

public class FormAddDto
{
    public FormAddDto()
    {
        Username ="";
        Email = "";
        Password = "";
        Reason ="";
    }

    public FormAddDto(string username, string email, string password, string reason)
    {
        Username = username;
        Email = email;
        Password = password;
        Reason = reason;
    }

    public string Username {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string Reason {get; set;}
}