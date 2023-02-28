namespace Application.Token.Commands.CreateNewPlay
{
    public class CreateNewPlayDto
    {
        public string Username { get; set; }
        public int InitialSquare { get; set; }
        public int SpacesToMoved { get; set; }
        public bool GameIsStarted { get; set; }
    }
}
