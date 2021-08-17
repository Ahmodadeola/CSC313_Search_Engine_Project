using iTextSharp.text;   
using iTextSharp.text.pdf;  
using iTextSharp.text.pdf.parser;  
using System;
using System.Text;
using System.IO;
using System.Web;
using Microsoft.Office.Interop.Word; 

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
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();  
            object miss = System.Reflection.Missing.Value;  
            // object path = System.IO.Path.Combine(_rootPath, "docs", "CSC316_ASSNM.docx");  
            object path = @"C:\Users\USER\Documents\CSC316_ASSNM.docx";
            object readOnly = true;  
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);  
            
            for (int i = 0; i < docs.Paragraphs.Count; i++)  
            {  
                text.Append(" \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString());  
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