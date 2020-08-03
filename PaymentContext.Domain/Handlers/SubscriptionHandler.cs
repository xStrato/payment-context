using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
  public class SubscriptionHandler : 
    Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>
  {
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;
    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        //Fail Fast Validations
        command.Validate();

        if(command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to complete your register");
        }

        //Desing by contract
        AddNotifications(new Contract()
        .Requires()
        //Verificar se documento está cadastrado
        .IsTrue(_repository.DocumentExists(command.Document), "command.Document", "CPF already in use")
        //Verificar se E-mail já está cadastrado
        .IsTrue(_repository.EmailExists(command.PayerEmail), "command.PayerEmail", "Email already in use"));

        //Gerar os VO's
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.PayerEmail);
        var address = new Address(command.Street, command.Number, command.Neighborhood,  command.City, command.State, command.Country, command.ZipCode);

        //Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, EDocumentType.CPF), address, email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        //Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        //Chegar as Notificações
        if(Invalid) return new CommandResult(false, "Unable to complete your register");

        //Salvar as informações
        _repository.CreateSubscription(student);

        //Enviar email de boas vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome to my 'Service'!", "Your subscription is ready to use!");

        //Retornar informações
        return new CommandResult(true, "Successful subscription");
    }

    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        //Fail Fast Validations
        //*pending*

        //Desing by contract
        AddNotifications(new Contract()
        .Requires()
        //Verificar se documento está cadastrado
        .IsTrue(_repository.DocumentExists(command.Document), "command.Document", "CPF already in use")
        //Verificar se E-mail já está cadastrado
        .IsTrue(_repository.EmailExists(command.PayerEmail), "command.PayerEmail", "Email already in use"));

        //Gerar os VO's
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.PayerEmail);
        var address = new Address(command.Street, command.Number, command.Neighborhood,  command.City, command.State, command.Country, command.ZipCode);

        //Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PaypalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, EDocumentType.CPF), address, email);

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        //Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        //Salvar as informações
        _repository.CreateSubscription(student);

        //Enviar email de boas vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome to my 'Service'!", "Your subscription is ready to use!");

        //Retornar informações
        return new CommandResult(true, "Successful subscription");
    }
  }
}