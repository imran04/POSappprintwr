using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PrintPos.Models;
using System.Drawing;
using System.Drawing.Printing;
using static System.Windows.Forms.AxHost;

namespace PrintPos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Invoice data)
        {
            var s = new PrintingExample(data);
            return Ok(new { message = "Print SuccessFull", status = true });
        }
    }
    public class PrintHub : Hub
    {
        public async Task Print(Invoice Data)
        {
            var s = new PrintingExample(Data);
        }
    }
   

    public class PrintingExample
    {
        Invoice printModel;

        public PrintingExample(Invoice data)
        {
            printModel = data;
            Console.WriteLine("Print Job");
            Printing();
        }
        public void Printing()
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                var pdoc = new PrintDocument();
                PaperSize psize = new PaperSize("Custom", 300, 500);
                pd.Document = pdoc;
                pd.Document.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 500;
                pdoc.DefaultPageSettings.PaperSize.Width = 300;
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

            
            Graphics graphics = e.Graphics!;
            string underLine = "";
            int startX = 10;
            int startY = 20;
            int Offset = 10;
            Offset += 0;
            Offset = Offset + 15;
            PrintHeader(graphics, startX + 10, startY += Offset);
            PrintCustomer(graphics,startX + 10, startY + Offset);
           
            
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
                e.PageSettings.PaperSize.Height += 22;
            }
            graphics.DrawString(underLine, new Font("calibri", 10), new SolidBrush(Color.Black), 0, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Print Time :  " + DateTime.Now.ToString("d, MMMM, yyyy. hh:mm - tt "), new Font("Calibri", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            
        }


        private void PrintHeader(Graphics graphics,int StartX,int StartY)
        {
            graphics.DrawString("JRS", new Font("calibri", 10), new SolidBrush(Color.Black), StartX, StartY);
        }

        private void PrintCustomer(Graphics graphics, int StartX, int StartY)
        {
            StartY += 15;
            graphics.DrawLine(new Pen(Brushes.Black),new Point(StartX,StartY ),new Point(300-StartX,StartY));
            StartY += 15;
            graphics.DrawString($"Customer {printModel?.Customer?.Fullname}", new Font("calibri", 10), new SolidBrush(Color.Black), StartX, StartY);
            StartY += 15;
            graphics.DrawLine(new Pen(Brushes.Black), new Point(StartX, StartY), new Point(300 - StartX, StartY));
           
        }


    }
}
