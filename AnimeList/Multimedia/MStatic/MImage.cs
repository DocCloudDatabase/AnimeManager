using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using MyLibrary.RegUtili;
using System.Windows.Media.Imaging;

namespace AnimeManager.Multimedia.MStatic
{
    public class MImage : IStatic<Image>
    {
        private byte[] _data;
        public void View(ref Image container)
        {
            throw new NotImplementedException();
        }

        public void Show(bool flag = true)
        {
            throw new NotImplementedException();
        }

        public bool Load(byte[] source)
        {
            this._data = source;
            return true;
        }

        public bool LoadFile(FileInfo aFile)
        {
            BitmapDecoder decoder = null;
            switch (aFile.Extension)
            {
                case "png":
                    //decoder for PNG
                    break;
                    
            }
            return false;
        }

        public bool LoadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public bool LoadFile(Uri url)
        {
            throw new NotImplementedException();
        }

        public byte[] Data
        {
            get { return this._data; }
        }

        public bool WriteTo(FileInfo aFile)
        {
            throw new NotImplementedException();
        }

        public bool WriteTo(string filename)
        {
            throw new NotImplementedException();
        }

        public void Preview(ref Image container, int[] dimensions = null)
        {
            throw new NotImplementedException();
        }
    }
}
