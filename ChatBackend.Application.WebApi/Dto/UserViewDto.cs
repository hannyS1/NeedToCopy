using ChatBackend.Core.Entities;

namespace ChatBackend.Application.WebApi.Dto;

public class UserViewDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public UserViewDto() {}

    public UserViewDto(User user)
    {
        Id = user.Id;
        Name = user.Name;
    }
}