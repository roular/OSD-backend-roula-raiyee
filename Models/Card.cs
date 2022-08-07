using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Card 
    {
        [Key]
        public int CardID {get;set;}
        public string CardTitle {get;set;} = "";
        public string CardCategory {get;set;} = ""; 
        public string CardDuedate {get;set;} = ""; 
        public string CardEstimate {get;set;} = ""; 
        public string CardImportance {get;set;} = ""; 
        public string CardType {get;set;} = ""; 
        public int Personid {get;set;}

    }
}