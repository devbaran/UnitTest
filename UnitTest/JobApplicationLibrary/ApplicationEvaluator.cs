using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary
{
    public class ApplicationEvaluator
    {
        private const int minAge = 18;
        private const int autoAcceptedYearOfExperience = 15;
        private List<string> techStackList = new List<string> { "C#", "RabbitMQ", "Microservice", "Visual Studio" };
        private IdentityValidator identityValidator;

        public ApplicationEvaluator()
        {
            identityValidator = new IdentityValidator();
        }

        public ApplicationResult Evaluate(JobApplication form)
        {
            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoReject;

            var validIdentity = identityValidator.IsValid(form.Applicant.IdentityNumber);

            if (!validIdentity)
                return ApplicationResult.TransferToHR;

            var similartyRate = GetTechStackSimilarityRate(form.TechStackList);

            if (similartyRate < 25)
                return ApplicationResult.AutoReject;
            if (similartyRate > 75 && form.YearsOfExperience >= autoAcceptedYearOfExperience)
                return ApplicationResult.AutoAccept;


            return ApplicationResult.AutoAccept;
        }
        private int GetTechStackSimilarityRate(List<string> techStack)
        {
            var matchedCount = techStack.Where(a => techStackList.Contains(a, StringComparer.OrdinalIgnoreCase)).Count();
            return (int)((double)matchedCount / techStackList.Count) * 100;
        }
    }
    public enum ApplicationResult
    {
        AutoReject,
        TransferToHR,
        TransferToLead,
        AutoAccept
    }
}
