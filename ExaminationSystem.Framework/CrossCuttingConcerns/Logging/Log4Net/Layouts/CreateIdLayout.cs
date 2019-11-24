using log4net.Core;
using log4net.Layout;
using System;
using System.IO;

namespace ExaminationSystem.Framework.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    public class CreateIdLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            writer.WriteLine(Guid.NewGuid().ToString());
        }
    }
}