using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_Engine_Project.Core;



namespace Search_Engine_Project.Core
{
	public class FileDocument
	{

		public string DocumentName { get; private set; }
		public int DocumentLength { get; private set; }

		public FileDocument(string documentName, int documentLength)
		{
			DocumentName = documentName;
			DocumentLength = documentLength;
		}
	}
}

