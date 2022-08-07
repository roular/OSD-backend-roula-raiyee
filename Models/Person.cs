using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Person
    {
        [Key]
        public int Personid {get;set;}
        public string Email {get;set;} = "";
        public string Password {get;set;} = "";
    }
}