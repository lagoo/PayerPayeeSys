using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : AuditableEntity, ITypedEntity
    {
        protected User(){}

        public User(string name, string document, string email, string password)
        {
            Name = name;
            Document = document;
            Email = email;
            Password = password;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }



        public int EntityId => Id;
        public string EntityUniqueIdentifier => $"{Document} - {Email}";
        public string Type => "Usuário";

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
