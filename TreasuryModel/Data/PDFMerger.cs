using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.IO;


namespace KKB.Treasury.TreasuryModel.Model
{
    public class PDFMerger
    {
        #region Fields
        private string sourcefolder;
        private string destinationfile;
        private IList fileList = new ArrayList();
        #endregion


        public PDFMerger()
        {
        }
        public PDFMerger(IList _fileList)
        {
            this.fileList = _fileList;
        }
        #region Public Methods
        ///
        /// Add a new file, together with a given docname to the fileList and namelist collection
        ///
        public void AddFile(string pathnname)
        {
            fileList.Add(pathnname);
        }
        ///
        /// Generate the merged PDF
        ///
        public void Execute()
        {
            MergeDocs();
        }
        #endregion


        #region Private Methods
        ///
        /// Merges the Docs and renders the destinationFile
        ///
        private void MergeDocs()
        {


            //Step 1: Create a Docuement-Object
            Document document = new Document();
            try
            {
                //Step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationfile, FileMode.Create));
                //Step 3: Open the document
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int n = 0;
                int rotation = 0;
                //Loops for each file that has been listed
                foreach (string filename in fileList)
                {
                    //The current file path
                    //string filePath = sourcefolder + filename;
                    // we create a reader for the document
                    PdfReader reader = new PdfReader(filename);
                    //Gets the number of pages to process
                    n = reader.NumberOfPages;
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        document.NewPage();


                        //Insert to Destination on the first page
                        if (i == 1)
                        {
                            Chunk fileRef = new Chunk(" ");
                            fileRef.SetLocalDestination(filename);
                            document.Add(fileRef);
                        }


                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0,


                          reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            finally { document.Close(); }
        }
        #endregion


        ///
        /// Gets or Sets the DestinationFile
        ///
        public string DestinationFile
        {
            get { return destinationfile; }
            set { destinationfile = value; }
        }
    }
}