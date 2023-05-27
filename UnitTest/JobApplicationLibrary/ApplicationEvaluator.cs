using JobApplicationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary
{
    public class ApplicationEvaluator
    {
        private const int minAge = 18;
        public ApplicationResult Evaluate(JobApplication form)
        {
            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoReject;

            return ApplicationResult.AutoAccept;
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
