namespace EasyOne.Controls.Editor
{
    using System;
    using System.Collections;
    using System.IO;

    public sealed class PEUtil
    {
        private PEUtil()
        {
        }

        public static DirectoryInfo CreateDirectory(string path)
        {
            ArrayList list = new ArrayList();
            DirectoryInfo parent = new DirectoryInfo(Path.GetFullPath(path));
            while ((parent != null) && !parent.Exists)
            {
                list.Add(parent.FullName);
                parent = parent.Parent;
            }
            if (parent == null)
            {
                throw new DirectoryNotFoundException("Directory \"" + list[list.Count - 1] + "\" not found.");
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                string str = (string) list[i];
                int num2 = NativeMethods._mkdir(str);
                if (num2 != 0)
                {
                    throw new FileNotFoundException(string.Concat(new object[] { "Error calling [msvcrt.dll]:_wmkdir(", str, "), error code: ", num2 }));
                }
            }
            return new DirectoryInfo(path);
        }
    }
}

