using System.Data;
using iTextSharp.text;   
using iTextSharp.text.pdf;  
using iTextSharp.text.pdf.parser;  
using System;
using System.Text;

namespace Search_Engine_Project.Core
{
    public class Parser
    {
        public static string GetTextFromPDF(){  
            try{
                StringBuilder text = new StringBuilder();  
                using (PdfReader reader = new PdfReader(@"C:\Users\USER\Downloads\CourseRegistration Ahmad Salaudeen.pdf")){  
                    for (int i = 1; i <= reader.NumberOfPages; i++){  
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));  
                    }  
                }  
                Console.WriteLine(text.ToString());
                return text.ToString();
            }catch(Exception err){
                Console.WriteLine(err);
            }
            return "";
        }  
    }
}