namespace WebApi.ExceptionHandling.Poc.Models
{
    public class Customer
    {

        public string Name { get; }
        public string Email { get; }
        public int Age { get; }

        public string WelcomeMessage => $"Welcome {Name} your email is : {Email}";

        public Customer(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
        }


    }
}
