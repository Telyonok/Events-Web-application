namespace EventsWebApp.Application.Helpers
{
    public static class FileToByteArrayConverter
    {
        public static byte[]? Convert(IFormFile? sourceMember)
        {
            if (sourceMember == null || sourceMember.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                sourceMember.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
