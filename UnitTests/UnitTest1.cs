using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportsV1.Module.BusinessObjects;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1 : XpoTestBase
    {
        [TestMethod]
        public void TestPerson()
        {
            using (var uow = CreateUnitOfWork())
            {
                Person person = new Person(uow);
                person.FirstName = "Max";
                Assert.AreEqual("Max", person.FirstName);
            }
        }

        [TestMethod]
        public void TestCalculations()
        {
            using (var uow = CreateUnitOfWork())
            {
                Person person = new Person(uow);
                person.FirstName = "Max";
                person.Communication.Add(new PersonCommunication(uow) {
                    Type = "T1",
                    CommunictionValue = "Value1"
                });

                uow.CommitChanges();
                Assert.AreEqual(6, person.CommunicationCount);
            }
        }

        [TestMethod]
        public void TestCalculations2()
        {
            using (var uow = CreateUnitOfWork())
            {
                var person = CreateObject(uow);
                uow.CommitChanges();
                Assert.AreEqual(13, person.CommunicationCount);
            }
        }


        private Person CreateObject(DevExpress.Xpo.Session session)
        {
            Person person1 = new Person(session);
            person1.LastName = "dddd1";
            person1.CommunicationCount = 11;
            PersonCommunication personCommunication1 = new PersonCommunication(session);
            personCommunication1.Type = "bb";
            personCommunication1.CommunictionValue = "12345678";
            personCommunication1.Person = person1;
            person1.Communication.Add(personCommunication1);
            PersonCommunication personCommunication2 = new PersonCommunication(session);
            personCommunication2.Type = "a";
            personCommunication2.CommunictionValue = "12345";
            personCommunication2.Person = person1;
            person1.Communication.Add(personCommunication2);
            return person1;
        }
    }
}
