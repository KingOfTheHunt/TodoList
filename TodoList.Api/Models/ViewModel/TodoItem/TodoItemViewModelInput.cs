using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models.ViewModel.TodoItem
{
    public class TodoItemViewModelInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsCompleted { get; set; } = false;
    }
}
