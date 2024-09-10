namespace EventsWebApp.Application.Helpers
{
    public static class DateHelpers
    {
        public static bool IsAValidBirthdate(DateTime birthday)
        {
            var age = DateTime.Now.Year - birthday.Year;

            return age < 10 || age > 120;
        }
    }
}
