using iTextSharp.text;   
using iTextSharp.text.pdf;  
using iTextSharp.text.pdf.parser;  
using System;
using System.Text;
using System.IO;
using System.Web;

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
                Console.WriteLine("An error occured");
                return "";
                
            }
            return "";
        }  

        private static string getRootPath(){
            string _rootPath = Directory.GetParent(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName;
            _rootPath = _rootPath.Substring(0, _rootPath.Length-9);
            return _rootPath;
        }
    }

    
}