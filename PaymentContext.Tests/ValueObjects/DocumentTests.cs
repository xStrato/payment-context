using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("82.695.298/0001-66")]
        [DataRow("23.696.362/000164")]
        [DataRow("77.822.5600001-56")]
        [DataRow("89.628294/0001-04")]
        public void ShouldReturnErrorWhenCPNJIsInvalid(string cnpj)
        {
            var document = new Document(cnpj, EDocumentType.CNPJ);
            Assert.AreEqual(true, document.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("82695298000166")]
        [DataRow("23696362000164")]
        [DataRow("77822560000156")]
        [DataRow("89628294000104")]
        public void ShouldReturnSucessWhenCPNJIsValid(string cnpj)
        {
            var document = new Document(cnpj, EDocumentType.CNPJ);
            Assert.AreEqual(true, document.Valid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var document = new Document("598.422.640-50", EDocumentType.CPF);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenCPFIsIValid()
        {
            var document = new Document("59842264050", EDocumentType.CPF);
            Assert.IsTrue(document.Valid);
        }
    }
}