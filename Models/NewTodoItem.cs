using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class NewTodoItem
    {
        [Required]
        public string Title {get; set;}

        public DateTimeOffset? DeadLine {get; set;}
    }
}