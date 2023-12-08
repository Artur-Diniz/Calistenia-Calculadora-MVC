namespace Calistenia_Calculadora.Models;

public class UsuarioViewModel
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public String PasswordString { get; set; }= string.Empty;
    
    public byte[] Foto {get; set;}

    public string perfil { get; set; }= string.Empty;

    public string Email {get; set;}= string.Empty;

    public string Token { get; set; }= string.Empty;
}
