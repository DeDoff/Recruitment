using Recruitment.Contracts;
using System.Linq;

namespace Recruitment.API.Validation
{
    public class ContractModelValidator
    {

        private bool StringValueValidate(string value, int minLenght, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            if (minLenght > maxLength)
                return false;

            if (minLenght < 0 || maxLength < 0)
                return false;

            if (!value.All(x => ((x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z')) || !char.IsLetter(x) || char.IsWhiteSpace(x)))
                return false;

            if (value.Length < minLenght || value.Length > maxLength)
                return false;

            return true;
        }
        public bool Validate(ContractModel contractModel)
        {
            if (contractModel is null)
                return false;
            if (!StringValueValidate(contractModel.Login, 3, 20))
                return false;
            if (!StringValueValidate(contractModel.Password, 5, 20))
                return false;

            return true;
        }
    }
}
