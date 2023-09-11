using ChatBackend.Core.Entities;

namespace ChatBackend.Application.WebApi.Dto;

public class AnnotatedMessageDto
{
    public UserViewDto User { get; set; }
    public string Text { get; set; }
    public bool IsMyMessage { get; set; }
    
    public AnnotatedMessageDto() {}

    public AnnotatedMessageDto(Message entity, int currentUserid)
    {
        User = new UserViewDto(entity.User);
        Text = entity.Text;
        IsMyMessage = currentUserid == entity.UserId;
    }
}