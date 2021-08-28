using System;
using Spire.Doc;
using Document = Spire.Doc.Document;
using Section = Spire.Doc.Section;
using Spire.Doc.Documents;
using FileFormat = Spire.Presentation.FileFormat;
using IShape = Spire.Presentation.IShape;
using Spire.Presentation;
using System.Text;
using System.IO;
using Spire.Pdf;
using Spire.Pdf.Exporting.Text;
using IronXL;
using System.Linq;

/*****************************************************
*
*
* Parser Methods Specification
* 
* Entry point method to read all supported file format
* public static string ReadText(string fileName)
* requires: string name of file
* effect: returns string text content of file
*
* PDF File Reader
* public static string GetTextFromPDF(string filename)
* requires: string name of pdf file
* effect: returns string text content of the pdf file

* WORD DOC File Reader
* public static string GetTextFromWord(string filename)
* requires: string name of doc/docx file
* effect: returns string text content of the doc/docx file

* TXT File Reader
* public static string GetTextFromTEXT(string filename)
* requires: string name of txt file
* effect: returns string text content of the txt file

* PPT File Reader
* public static string GetTextFromPPT(string filename)
* requires: string name of ppt/pptx file
* effect: returns string text content of the ppt/pptx file

* PDF File Reader
* public static string GetTextFromPDF(string filename)
* requires: string name of pdf file
* effect: returns string text content of the pdf file

* HTML/XML File Reader
* public static string GetTextFromHTML(string filename)
* requires: string name of html/xml file
* effect: returns string text content of the html/xml file

* XLS/XLSX File Reader
* public static string GetTextFromXLS(string filename)
* requires: string name of xls/xlsx file
* effect: returns string text content of the xls/xlsx file
*
* helper method to get root path
* private static string getRootPath()
* requires: nothing
* effect: returns string for root path
*
*
***********************************************************/

namespace Search_Engine_Project.Core

{
    public class Parser
    {
        private static string _rootPath = getRootPath();
       /// <summary>
        /// entry method for reading text from all file formats
        /// </summary>
        /// <param name="fileName">string for user file name</param>
         /// <returns>string text content of file</returns>
        
        public static string ReadText(string fileName){

            string ext = Path.GetExtension(fileName);
            Console.WriteLine(ext);
            switch(ext){
                case ".txt":
                    return GetTextFromText(fileName);
                case ".pdf":
                    return GetTextFromPDF(fileName);
                case ".docx":
                case ".doc":
                    return GetTextFromWord(fileName);
                case ".html":
                case ".htm":
                case ".xml":
                    return GetTextFromHTML(fileName);
                case ".pptx":
                case ".ppt":
                    return GetTextFromPPT(fileName);
                case ".xlsx":
                case ".xls":
                    return GetTextFromXLS(fileName);
                default:
                    return "";
            }
        }

        /// <summary>
        /// pdf text content reader
        /// </summary>
        /// <param name="fileName">string for user file name</param>
         /// <returns>string text content of pdf file</returns>
        public static string GetTextFromPDF(string fileName = "Stoichiometry AP Exam Questions.pdf"){  
            try{
                StringBuilder text = new StringBuilder();  
                //form file path
                string filePath = System.IO.Path.Combine(_rootPath, "docs", fileName);
                
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(filePath);
                PdfPageBase page = doc.Pages[0];


                SimpleTextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string tx = page.ExtractText(strategy);
                Console.WriteLine(tx);
                return tx;
            }catch(Exception ex){
                return "";
                
            }
        }  

        /// <summary>
        /// doc/docx text content reader
        /// </summary>
        /// <param name="fileName">string for user file name</param>
         /// <returns>string text content of doc/docx file</returns>
        public static string GetTextFromWord(string fileName="CSC316_ASSNM.docx"){  
            try{
                StringBuilder text = new StringBuilder();  
                string filePath = System.IO.Path.Combine(_rootPath, "docs", fileName);  

                //Load Document
                Document document = new Document();
                document.LoadFromFile(filePath);

                //Extract Text from Word and Save to StringBuilder Instance
                foreach (Section section in document.Sections)
                {
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        text.AppendLine(paragraph.Text);
                    }
                }

                Console.WriteLine(text.ToString());
                return text.ToString();  
            }catch(Exception ex){
                return "";
            }
        }  

        /// <summary>
        /// txt text content reader
        /// </summary>
        /// <param name="fileName">string for user file name</param>
         /// <returns>string text content of txt file</returns>
        public static string GetTextFromText(string fileName = "LICENSE.txt"){  
            try{
                string filePath = System.IO.Path.Combine(_rootPath, "docs", fileName);
                string text = System.IO.File.ReadAllText(filePath);  
                Console.WriteLine(text.ToString());
                return text.ToString();  
            }catch(Exception ex){
                return "";
            }
            
        }  

        ///  <summary>
        ///   ppt/pptx text content reader
        ///  </summary>
        ///  <param name="fileName">string for user file name</param>
         /// <returns>string text content of ppt/pptx file</returns>
        public static string GetTextFromPPT(string fileName = "Wired Local Area Network (LAN).pptx"){
            try{
                string filePath = System.IO.Path.Combine(_rootPath, "docs", fileName);
                Presentation presentation = new Presentation(filePath, FileFormat.Pptx2010);
                StringBuilder text = new StringBuilder();  
                foreach (ISlide slide in presentation.Slides)
                {
                    foreach (IShape shape in slide.Shapes)
                    {
                        if (shape is IAutoShape)
                        {
                            foreach (TextParagraph tp in (shape as IAutoShape).TextFrame.Paragraphs)
                            {
                                text.Append(tp.Text + Environment.NewLine);
                            }
                        }
                    }
                }
                Console.WriteLine(text.ToString());
                return text.ToString();
            }catch(Exception ex){
                    return "";
            }
        }

        /// <summary>
        /// html/xml text content reader
        /// </summary>
        /// <param name="fileName">string for user file name</param>
         /// <returns>string text content of html/xml file</returns>
        public static string GetTextFromHTML(string fileName = "word_unscrambler.htm"){
            string filePath = System.IO.Path.Combine(_rootPath, "docs", fileName);
            try{
                string text;
                Document docHTML = new Document();
                docHTML.LoadHTML(new StreamReader(filePath), XHTMLValidationType.None);
                Console.WriteLine(docHTML);
                return "";
            }catch(Exception ex){
                Console.WriteLine(ex);
                return "";
            }
        }

        /// <summary>
        /// xls/xlsx text content reader
        /// </summary>
        /// <param name="fileName">string for user file name</param>
         /// <returns>string text content of xls/xlsx file</returns>
        public static string GetTextFromXLS(string fileName ="dataset.xlsx"){
            IronXL.License.LicenseKey = "IRONXL.SALAUDEENAHMAD.32426-E6FBCA4380-GYFVEE-7M5GFWRMTF2Y-F4A5QLGAMLKS-IOQLT6I3UHQH-IZEVES2MKUMC-QNNUMAPN5BI6-5UT3CU-TNRRKDJ6UWSBUA-DEPLOYMENT.TRIAL-4CMNUC.TRIAL.EXPIRES.22.SEP.2021";
            string filePath = System.IO.Path.Combine(_rootPath, "docs", fileName);
            StringBuilder text = new StringBuilder();
            try{
                WorkBook workbook = new WorkBook(filePath);
                WorkSheet sheet = workbook.WorkSheets[0];

                //access all rows of open Excel WorkSheet
                for (int i = 0; i < sheet.Rows.Count(); i++)
                {    
                    //access all columns of specific row
                    for (int j = 0; j < sheet.Columns.Count(); j++)
                    {
                        //Access each cell for specified column
                        // Console.WriteLine(sheet.Rows[i].Columns[j].Value.ToString());
                        text.Append(sheet.Rows[i].Columns[j].Value.ToString() + " ");
                    }
                        // Console.WriteLine();
                        text.Append('\n');
                }
                Console.WriteLine(text.ToString().Trim());
                return text.ToString().Trim();
            }catch(Exception ex){
                Console.WriteLine(ex);
                return "";
            }
        }

        /// <summary>
        /// helper method to get root path
        /// </summary>
         /// <returns>string text of root path</returns>
        public static string getRootPath(){
            string _rootPath = Directory.GetParent(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName;
            _rootPath = _rootPath.Substring(0, _rootPath.Length-9);
            return _rootPath;
        }
    }

    
}