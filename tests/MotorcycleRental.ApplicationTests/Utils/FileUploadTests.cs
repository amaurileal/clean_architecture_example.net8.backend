using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace MotorcycleRental.ApplicationTests.Utils
{
    public class FileUploadTests
    {
        

        public IFormFile CreateTestFormTxtFile(string content, string fileName)
        {
            // Convert the string content to a byte array
            byte[] fileBytes = Encoding.UTF8.GetBytes(content);

            // Create a MemoryStream based on the byte array
            MemoryStream stream = new MemoryStream(fileBytes);

            // Create the FormFile
            FormFile formFile = new FormFile(stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

            // Rewind the stream to ensure it's readable from the start
            stream.Position = 0;

            return formFile;
        }


        public IFormFile CreateTestFormPngFile()
        {

            string content = "xxXFdsafds afds afdsa fdsafdsafds";
            string fileName = "teste.png";

            // Convert the string content to a byte array
            byte[] fileBytes = Encoding.UTF8.GetBytes(content);

            // Create a MemoryStream based on the byte array
            MemoryStream stream = new MemoryStream(fileBytes);

            // Create the FormFile
            FormFile formFile = new FormFile(stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            // Rewind the stream to ensure it's readable from the start
            stream.Position = 0;

            return formFile;
        }

    }
}
