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
                
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(filePath);
                PdfPageBase page = doc.Pages[0];


                SimpleTextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string tx = page.ExtractText(strategy);

                
                Console.WriteLine(_rootPath);
                // using (PdfReader reader = new PdfReader(filePath)){  
                //     for (int i = 1; i <= reader.NumberOfPages; i++){  
                //         text.Append(PdfTextExtractor.GetTextFromPage(reader, i));  
                //     }  
                // }  
                Console.WriteLine(tx);
                return tx;
            }catch(Exception ex){
                return "";
                
            }
        }  

        public static string GetTextFromWord(){  
            try{
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
            }catch(Exception ex){
                return "";
            }
        }  

        public static string GetTextFromText(){  
            try{
                string filePath = System.IO.Path.Combine(_rootPath, "docs", "LICENSE.txt");
                string text = System.IO.File.ReadAllText(filePath);  
                Console.WriteLine(text.ToString());
                return text.ToString();  
            }catch(Exception ex){
                return "";
            }
            
        }  

        public static string GetTextFromPPT(){
            try{
                string filePath = System.IO.Path.Combine(_rootPath, "docs", "Wired Local Area Network (LAN).pptx");
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

        private static string getRootPath(){
            string _rootPath = Directory.GetParent(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName;
            _rootPath = _rootPath.Substring(0, _rootPath.Length-9);
            return _rootPath;
        }
    }

    
}