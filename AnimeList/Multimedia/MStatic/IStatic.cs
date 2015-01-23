using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeManager.Multimedia.MStatic
{
    public interface IStatic<TContainer>
    {
        void View(ref TContainer container);
        void Show(bool flag = true);
        bool Load(byte[] source);
        bool LoadFile(FileInfo aFile);
        bool LoadFile(string filename);
        bool LoadFile(Uri url);
        byte[] Data
        {
            get;
        }
        bool WriteTo(FileInfo aFile);
        bool WriteTo(string filename);
        void Preview(ref TContainer container, int[] dimensions = null);
    }
}
