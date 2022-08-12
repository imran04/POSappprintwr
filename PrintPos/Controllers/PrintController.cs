using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintPos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            var s = new PrintingExample(new PrintModel());
            return Ok(s);
        }
    }
    public class PrintModel
    {

        public string Heading { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone { get; set; }
    }

    public class PrintingExample
    {
        PrintModel printModel;

        public PrintingExample(PrintModel data)
        {
            printModel = data;
            Printing();
        }
        public void Printing()
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                //  pd.ShowDialog();
                var pdoc = new PrintDocument();
                // PrinterSettings ps = new PrinterSettings();
                //Font font = new Font("calibri", 15);
                PaperSize psize = new PaperSize("Custom", 100, 30000);
                pd.Document = pdoc;
                pd.Document.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 30000;
                pdoc.DefaultPageSettings.PaperSize.Width = 520;
                // pdoc.PrinterSettings.PrinterName = "POS";
                pdoc.PrintPage += new PrintPageEventHandler(PrintPage);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(PrintPage);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }


        private void PrintPage(object sender, PrintPageEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Font font = new Font("calibri", 10);
            float fontHeight = font.GetHeight();

            String underLine = "-----------------------------------------------------------";
            int startX = 10;
            int startY = 20;
            int Offset = 10;
            Offset += 0;
            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), 0, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString($"{printModel.Heading}", new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 15, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), 0, startY + Offset);
            double tAmount = 0;
            double gTotal = 0;
            string xParent = "*";
            for (int i = 0; i < 2; i++)
            {
                string parent = "toLocation".ToString();
                if (parent != xParent)
                {
                    tAmount = 0;
                    Offset = Offset + 15;
                    graphics.DrawString(parent.ToUpper() + " SECTION", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString("Item                                                   Quantity", new Font("Calibri", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;
                    xParent = parent;
                }
                string item = (i + 1) + " - " + "Name".ToString();
                string quantity = "quantity";
                graphics.DrawString(item, new Font("Calibri", 8), new SolidBrush(Color.Black), startX, startY + Offset);
                graphics.DrawString(quantity, new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 180, startY + Offset);
                Offset = Offset + 15;
                string nextParent;

                nextParent = "toLocation".ToString();

            }
            graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), 0, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Print Time :  " + DateTime.Now.ToString("d, MMMM, yyyy. hh:mm - tt "), new Font("Calibri", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            //graphics.DrawIcon(new Icon())
            graphics.DrawString("By : www.codemodes.com", new Font("Calibri", 10), new SolidBrush(Color.Black), startX, startY + Offset);
        }





    }
}
