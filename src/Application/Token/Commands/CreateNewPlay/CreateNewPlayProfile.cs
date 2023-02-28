using AutoMapper;

namespace Application.Token.Commands.CreateNewPlay
{
    public class CreateNewPlayProfile: Profile
    {
        public CreateNewPlayProfile()
        {
            CreateMap<CreateNewPlayDto, CreateNewPlayCommand>(MemberList.None);
        }
    }
}
