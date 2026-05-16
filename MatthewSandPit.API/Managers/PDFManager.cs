using MatthewSandPit.API.Managers.Interfaces;
using PdfSharp.Pdf;

namespace MatthewSandPit.API.Managers
{
    public class PDFManager : IPDFManager
    {
        public byte[] MergePDFs(IEnumerable<Stream> files)
        {
            using MemoryStream outputStream = new MemoryStream();

            PdfDocument outputPDFDoc = new PdfDocument();

            foreach (var file in files)
            {
                PdfDocument inputPDFDoc = PdfSharp.Pdf.IO.PdfReader.Open(file, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
                for (int i = 0; i < inputPDFDoc.PageCount; i++)
                {
                    outputPDFDoc.AddPage(inputPDFDoc.Pages[i]);
                }
            }

            outputPDFDoc.Save(outputStream);

            return outputStream.ToArray();
        }
    }
}
