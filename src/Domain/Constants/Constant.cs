namespace HPC.Domain.Constants
{
    public class Constant
    {
        public class Regex
        {
            public const string ShipCode = @"[A-Z]{4}-\d{4}-[A-Z]{1}\d{1}";
        }

        public class ResponseMessage
        {
            public const string InvalidShipCode = "'Code' must be 'AAAA-1111-A1' format where A is any character from the Latin alphabet and 1 is a number from 0 to 9.";

            public const string DuplicateShipCode = "The specified code already exists.";
        }
    }
}
