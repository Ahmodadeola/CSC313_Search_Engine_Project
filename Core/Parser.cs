using iTextSharp.text;   
using iTextSharp.text.pdf;  
using iTextSharp.text.pdf.parser;  
using System;
using Spire.Doc;
using Document = Spire.Doc.Document;
using Section = Spire.Doc.Section;
using Spire.Doc.Documents;
using Paragraph = Spire.Doc.Documents.Paragraph;
using System.Text;
using System.IO;

namespace Search_Engine_Project.Core
{
    public class Parser
    {
        private static string _rootPath = getRootPath();

        public static string GetTextFromPDF(){  
            try{
                StringBuilder text = new StringBuilder();  
                //form file path
                string filePath = System.IO.Path.Combine(_rootPath, "docs", "Stoichiometry AP Exam Questions.pdf");
                Console.WriteLine(_rootPath);
                using (PdfReader reader = new PdfReader(filePath)){  
                    for (int i = 1; i <= reader.NumberOfPages; i++){  
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));  
                    }  
                }  
                Console.WriteLine(text.ToString());
                return text.ToString();
            }catch(Exception ex){
                return "";
                
            }
        }  

        public static string GetTextFromWord(){  
            StringBuilder text = new StringBuilder();  
            string filePath = System.IO.Path.Combine(_rootPath, "docs", "CSC316_ASSNM.docx");  

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
        }  

        public static string GetTextFromText(){  
            string filePath = System.IO.Path.Combine(_rootPath, "docs", "LICENSE.txt");
            string text = System.IO.File.ReadAllText(filePath);  
            Console.WriteLine(text.ToString());
            return text.ToString();  
        }  

        private static string getRootPath(){
            string _rootPath = Directory.GetParent(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName;
            _rootPath = _rootPath.Substring(0, _rootPath.Length-9);
            return _rootPath;
        }
    }

    
}