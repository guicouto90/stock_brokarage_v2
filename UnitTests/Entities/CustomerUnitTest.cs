using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace UnitTests.Entities
{
    public class CustomerUnitTest
    {
        [Fact]
        public void CreateCustomer_Succeed()
        {
            Action action = () => new Customer("Guilherme", "12345678999", 1, "123456");
            action.Should()
                 .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCustomer_ShortName_DomainExceptionNameLength()
        {
            Action action = () => new Customer("Gu", "12345678999", 1, "123456");
            action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("Name must have at least length of 3 characters");
        }

        [Fact]
        public void CreateCustomer_ShortPassword_DomainExceptionPasswordLength()
        {
            Action action = () => new Customer("Guilherme", "12345678999", 1, "12345");
            action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("Password must have at least length of 6 characters");
        }

        [Fact]
        public void CreateCustomer_CpfLengthInvalid_DomainExceptionCpfInvalid()
        {
            Action action = () => new Customer("Guilherme", "1234567899", 1, "123456");
            action.Should()
               .Throw<DomainExceptionValidation>()
                .WithMessage("Cpf must have length of 11 characters");
        }
    }
}
