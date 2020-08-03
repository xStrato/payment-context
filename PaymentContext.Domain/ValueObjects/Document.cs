using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document: ValueObject
    {
        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Invalid Document"));
        }

        private bool Validate()
        {
            if(Type.Equals(EDocumentType.CNPJ) && Number.Length.Equals(14)) return true;
            if(Type.Equals(EDocumentType.CPF) && Number.Length.Equals(11)) return true;

            return false;
        }
    }
}