using JobApplicationLibrary.Models;
using NUnit.Framework;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {
        /*
         How to write test function name?
        UnitOfWork_Condition_ExpectedResult or Condition_ExpectedResult or
        UnitOfWork_ExpectedResult_Condition

        - Every single test should be independent
         */
        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var application = new JobApplication
            {
                Applicant = new Applicant { Age = 17 }
            };

            //Action
            var appResult = evaluator.Evaluate(application);


            //Assert
            Assert.AreEqual(ApplicationResult.AutoReject, appResult);
        }

        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var application = new JobApplication
            {
                Applicant = new Applicant { Age = 18 },
                TechStackList = new System.Collections.Generic.List<string> { "" }
            };

            //Action
            var appResult = evaluator.Evaluate(application);


            //Assert
            Assert.AreEqual(ApplicationResult.AutoReject, appResult);
        }

        [Test]
        public void Application_WithTechStackOver75p_TransferredToAutoAccepted()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var application = new JobApplication
            {
                Applicant = new Applicant { Age = 18 },
                TechStackList = new System.Collections.Generic.List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio" },
                YearsOfExperience = 15
            };

            //Action
            var appResult = evaluator.Evaluate(application);


            //Assert
            Assert.AreEqual(ApplicationResult.AutoAccept, appResult);
        }
    }
}