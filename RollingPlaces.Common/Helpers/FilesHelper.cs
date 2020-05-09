using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RollingPlaces.Common.Helpers
{
    public class FilesHelper : IFilesHelper
    {
        public byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
