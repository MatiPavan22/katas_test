using AutoMapper;
using Common.Interfaces;
using MediatR;

namespace Application.Token.Commands.CreateNewPlay
{
    public class CreateNewPlayCommand : IRequest<CreateNewPlayVm>
    {
        public string Username { get; set; }
        public int InitialSquare { get; set; }
        public int SpacesToMoved { get; set; }
        public bool GameIsStarted { get; set; }
    }
}
