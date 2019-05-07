using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.IO;

namespace SapDemo.Activities
{
    public class SaveAttachment: CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<System.Net.Mail.Attachment> Attachment { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Path { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var att = Attachment.Get(context);
            var path = Path.Get(context);

            byte[] allBytes = new byte[att.ContentStream.Length];
            int bytesRead = att.ContentStream.Read(allBytes, 0, (int)att.ContentStream.Length);

            string destinationFile = path + att.Name;

            BinaryWriter writer = new BinaryWriter(new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));
            writer.Write(allBytes);
            writer.Close();
        }
    }
}
