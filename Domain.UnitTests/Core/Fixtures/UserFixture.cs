using Bogus;
using Bogus.DataSets;
using Domain.Entities;
using Domain.UnitTests.Core.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.UnitTests.Core.Fixtures
{
    [CollectionDefinition(nameof(UserCollection))]
    public class UserCollection : ICollectionFixture<UserFixture>
    { }

    public class UserFixture : IDisposable
    {
        public Dictionary<string, string[]> ValidationErrosExpected;

        public UserFixture()
        {
            ValidationErrosExpected = FluentValidationHelper.GetFluentValidationRules(new UserValidator());
        }

        public User GenerateValidEntity(string password)
        {
            Name.Gender genero = new Faker().PickRandom<Name.Gender>();

            return new Faker<User>("pt_BR")
                        .CustomInstantiator(f =>
                        {
                            string name = f.Name.FirstName(genero);

                            return new User(name: name,
                                            document: "92426261803",
                                            email: f.Internet.Email(name),
                                            password: password);
                        });
        }

        public User GenerateInvalidEntity()
        {
            Name.Gender genero = new Faker().PickRandom<Name.Gender>();

            return new Faker<User>("pt_BR")
                        .CustomInstantiator(f =>
                        {
                            return new User(name: "",
                                            document: "",
                                            email: "",
                                            password: "");
                        });
        }

        public void Dispose()
        {
        }
    }
}