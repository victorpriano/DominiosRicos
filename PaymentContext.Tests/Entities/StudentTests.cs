using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("09406103028", EDocumentType.CPF);
            _address = new Address("Rua 1", "1234", "bairro legal", "gotham", "ce", "BR", "13400");
            _email = new Email("batman@dc.com");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        } 

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {    
            // Deve retornar um erro quando tiver mais de uma assinatura ativa

            var payment = new PaypalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            // Deve retornar um erro quando o subscription não tiver um pagamento
            // var subscription = new Subscription(null);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PaypalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE", _document, _address, _email);
            subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }
        
    }
}