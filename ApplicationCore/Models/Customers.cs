using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Models
{
    public class Customers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float AccountBalance { get; set; }
        public string Email { get; set; } = null!;
        /* 
         * Перед тем как вычислить хеш пароля и записать его в базу, 
         * к нему добавляют некий случайный набор символов, который называется соль (salt). 
         * И таким образом записанные в базу хеши полностью изменяются — так, 
         * что даже наиболее глупые и часто используемые пароли вроде «password» 
         * становится невозможно взломать с помощью радужных таблиц.
         * */
        public string PasswordHash { get; set; } = null!;
        public string? Role { get; set; }
        public ICollection<Orders>? orders { get; set; }

        public Customers() { }
        public Customers(string name, float accountBalance, string email, string passwordHash )
        {
            Name = name;
            AccountBalance = accountBalance;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
