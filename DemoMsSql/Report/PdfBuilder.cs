using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace DemoMsSql.Report
{
    public static class PdfBuilder
    {
        public static PdfBuilderResult ConvertHtmlToPdfAsBytes(string htmlData, bool landscape = false)
        {
            // variables
            PdfBuilderResult result = new PdfBuilderResult();

            // do some additional cleansing to handle some scenarios that are out of control with the html data
            htmlData = htmlData.Replace("<br>", "<br />");

            // convert html to pdf
            try
            {
                // create a stream that we can write to, in this case a MemoryStream
                using (var stream = new MemoryStream())
                {
                    // create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var document = new Document())
                    {
                        // portrait vs landscape
                        if (landscape)
                        {
                            document.SetPageSize(PageSize.A4.Rotate());
                        }

                        // create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(document, stream))
                        {
                            // open the document for writing
                            document.Open();

                            // read html data to StringReader
                            using (var html = new StringReader(htmlData))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
                            }

                            // close document
                            document.Close();
                        }
                    }

                    // get bytes from stream
                    result.Data = stream.ToArray();

                    // success
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            // return
            return result;
        }

        public static PdfBuilderResult ConvertHtmlToPdfAsFile(string filePath, string htmlData)
        {
            // variables  
            PdfBuilderResult result = new PdfBuilderResult();

            try
            {
                // convert html to pdf and get bytes array  
                result = ConvertHtmlToPdfAsBytes(htmlData: htmlData);

                // check for errors  
                if (!result.Success)
                {
                    return result;
                }

                // create file  
                File.WriteAllBytes(path: filePath, bytes: result.Data);

                // result  
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            // return  
            return result;
        }

        public static void TestConvertHtmlToPdf()
        {
            // loading html file
            string htmlData = File.ReadAllText(@"C:\Users\chris\Documents\Developpement\Pierre\DemoMsSql\DemoMsSql\demo.html");

            // converting
            ConvertHtmlToPdfAsFile(@"C:\Users\chris\Documents\Developpement\Pierre\DemoMsSql\DemoMsSql\result.pdf", htmlData);

        }
    }

    public class PdfBuilderResult
    {
        // constructor  
        public PdfBuilderResult()
        {
            this.Success = false;
            this.Message = string.Empty;
        }

        // properties  
        public bool Success = false;
        public string Message = string.Empty;
        public Byte[] Data = null;
    }
}
