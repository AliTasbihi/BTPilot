using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Utility.Serialization
{
    public class CustomArrayBase64
    {
        private const string separateKey = "<SeparateKeyValue>";
        private List<string> valueList = new List<string>();
        private string[] allStringArray;
        private int _currentIndex;

        public CustomArrayBase64(string value)
        {
            if (value != "")
            {
                allStringArray = value.Split(separateKey);
            }

            _currentIndex = 0;
        }

        public void AddImage(Bitmap bmp)
        {
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            var base64String = Convert.ToBase64String(byteImage); // Get Base64
            valueList.Add(base64String);
        }
        public Bitmap GetImage()
        {

            byte[] bytes = Convert.FromBase64String(ReadCurrentValue());
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (Bitmap)Image.FromStream(ms);
            }
        }

        public void AddString(string value)
        {
            valueList.Add(Base64Encode(value));
        }
        public string GetString()
        {
            var value = ReadCurrentValue();
            return Base64Decode(value);
        }

        public string GetStringWithoutRaise()
        {
            if (_currentIndex >= allStringArray.Length)
                return "";
            return GetString();
        }

        public void AddInteger(int value)
        {
            valueList.Add(Base64Encode(value.ToString()));
        }
        public int GetInteger()
        {
            var value = ReadCurrentValue();
            return Int32.Parse(Base64Decode(value));
        }

        public void AddPoint(Point point)
        {
            AddInteger(point.X);
            AddInteger(point.Y);
        }
        public Point GetPoint()
        {
            var x = GetInteger();
            var y = GetInteger();
            return new Point(x, y);
        }

       public void AddRectangle(Rectangle rect)
        {
            AddInteger(rect.X);
            AddInteger(rect.Y);
            AddInteger(rect.Width);
            AddInteger(rect.Height);
        }
        public Rectangle GetRectangle()
        {
            var x = GetInteger();
            var y = GetInteger();
            var w = GetInteger();
            var h = GetInteger();
            return new Rectangle(x, y,w,h);
        }


        public string GetAllAsString()
        {
            return String.Join(separateKey, valueList);
        }

        private string Base64Encode(string plainText)
        {
            

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private string ReadCurrentValue()
        {
            var value = allStringArray[_currentIndex];
            _currentIndex++;
            return value;
        }

    }

}
