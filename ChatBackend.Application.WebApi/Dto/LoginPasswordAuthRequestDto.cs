using System.ComponentModel.DataAnnotations;

namespace ChatBackend.Application.WebApi.Dto;

public class LoginPasswordAuthRequestDto
{
    [Required(ErrorMessage = "Username must be set")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password must be set")]
    public string Password { get; set; }
}