using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YouDo.Core.Validations;

namespace YouDo.Application.DTOs.ToDo
{
    public class CreateToDoDTO
    {
        public string Title { get; set; }

        public string Details { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
