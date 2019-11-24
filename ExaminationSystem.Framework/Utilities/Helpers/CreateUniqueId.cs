using System;

namespace ExaminationSystem.Framework.Utilities.Helpers
{
    public static class CreateUniqueId
    {
        public static string CreateId()
        {
            var guid = Guid.NewGuid().ToString();
            var random = new Random();
            var rnd = random.Next(1000, 9999).ToString();
            var hour = DateTime.Now.Hour.ToString();
            var day = DateTime.Now.Day.ToString();
            var month = DateTime.Now.Month.ToString();
            var year = DateTime.Now.Month.ToString();

            return year + month + day + hour + guid + rnd;
        }
    }
}