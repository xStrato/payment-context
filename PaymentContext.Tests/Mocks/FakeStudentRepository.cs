using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
  public class FakeStudentRepository : IStudentRepository
  {
    public void CreateSubscription(Student student){}
    public bool DocumentExists(string document) => document.Equals("1234") ? true : false;
    public bool EmailExists(string email) => email.Equals("email@email.com") ? true : false;
  }
}