namespace YouDo.API.Models.ToDo
{
    public class CreateToDoModel
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public Guid UserId { get; set; }
    }
}
