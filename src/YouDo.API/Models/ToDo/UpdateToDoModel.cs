namespace YouDo.API.Models.ToDo
{
    public class UpdateToDoModel : CreateToDoModel
    {
        public Guid Id { get; set; }

        public bool Completed { get; set; }
    }
}
