using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Layout.Borders;
using appAPI.Repository;
using appAPI.Models;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly IRepository<Orders> _orderRepository;
        private readonly IRepository<Order_details> _orderDetailsRepository;
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<Product_Attributes> _productAttributesRepository;

        public PDFController(
            IRepository<Orders> orderRepository,
            IRepository<Order_details> orderDetailsRepository,
            IRepository<Users> userRepository,
            IRepository<Product_Attributes> productAttributesRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _userRepository = userRepository;
            _productAttributesRepository = productAttributesRepository;
        }


        [HttpGet("generate")]
        public IActionResult GenerateInvoice([FromQuery] int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null) return NotFound("Không tìm thấy đơn hàng.");

            var user = _userRepository.GetById(order.User_id ?? 0);

            var orderDetails = _orderDetailsRepository.Find(od => od.OrderId == orderId).ToList();

            using (var stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4);
                document.SetMargins(30, 25, 30, 25);

                // Load font
                string fontPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "Roboto-Regular.ttf");
                if (!System.IO.File.Exists(fontPath)) return BadRequest("Font không tồn tại tại đường dẫn: " + fontPath);

                PdfFont fontNormal = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
                PdfFont fontBold = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                // Thông tin Header
                Table headerTable = new Table(3).UseAllAvailableWidth();
                headerTable.AddCell(new Cell().Add(new Paragraph("HangKenh").SetFont(fontBold).SetFontSize(12)).SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().Add(new Paragraph("HÓA ĐƠN GIÁ TRỊ GIA TĂNG").SetFont(fontBold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().Add(new Paragraph($"Số (No.): {order.Id}").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                document.Add(headerTable);

                document.Add(new Paragraph("VAT INVOICE").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.CENTER));
                document.Add(new Paragraph($"Ngày (Date): {DateTime.Now:dd/MM/yyyy}\n\n").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.CENTER));

                // Thông tin Người bán
                document.Add(new Paragraph("Tên đơn vị bán hàng (Seller): TRƯỜNG CAO ĐẲNG FPT POLYTECHNIC").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("Mã số thuế (Tax code): 0102635866").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("Địa chỉ (Address): Km12, đường Cầu Diễn, P.Phúc Diễn, Q.Bắc Từ Liêm, Hà Nội").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("Số tài khoản (A/C No.): 13136969301 tại Ngân hàng TMCP Tiên Phong - CN Hoàn Kiếm").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Thông tin Người mua
                if (user != null)
                {
                    document.Add(new Paragraph($"Họ tên người mua hàng (Buyer): {user.Name}").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph($"Địa chỉ (Address): {user.Address}").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph($"Số điện thoại (Phone): {user.Phone}").SetFont(fontNormal).SetFontSize(9));
                }
                else
                {
                    document.Add(new Paragraph("Họ tên người mua hàng (Buyer): ").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph("Địa chỉ (Address): ").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph("Số điện thoại (Phone): ").SetFont(fontNormal).SetFontSize(9));
                }

                // Bảng chi tiết thanh toán
                Table paymentTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                paymentTable.AddCell(new Cell().Add(new Paragraph("Hình thức thanh toán (Payment method): Chuyển khoản").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                paymentTable.AddCell(new Cell().Add(new Paragraph("Số tài khoản (A/C No.): 13136969301").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                document.Add(paymentTable);

                document.Add(new Paragraph("Ngân hàng (Bank): [Tên ngân hàng của bạn]").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Bảng chi tiết sản phẩm
                Table itemTable = new Table(new float[] { 1, 3, 2, 2, 1.5f, 2 }).UseAllAvailableWidth();
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("STT (No.)").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Mã sản phẩm SKU").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Đơn giá").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Giá Giảm (Nếu có)").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Số lượng").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Thành tiền").SetFont(fontBold).SetFontSize(9)));

                int index = 1;
                decimal totalAmount = 0;
                foreach (var detail in orderDetails)
                {
                    var product = _productAttributesRepository.GetById(detail.Product_Attribute_Id);
                    if (product != null)
                    {
                        decimal unitPrice = product.Regular_price ?? 0;
                        decimal discountPrice = product.Sale_price ?? 0;
                        decimal amount = detail.Quantity * discountPrice;

                        itemTable.AddCell(new Cell().Add(new Paragraph(index.ToString()).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(product.SKU).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph($"{unitPrice:0,0}").SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph($"{discountPrice:0,0}").SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(detail.Quantity.ToString()).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph($"{amount:0,0}").SetFont(fontNormal).SetFontSize(9)));

                        totalAmount += amount;
                        index++;
                    }
                }
                document.Add(itemTable);

                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Tổng tiền
                decimal totalPayment = totalAmount * 1.1m;
                string amountInWords = ConvertNumberToWords(totalPayment);

                Table totalTable = new Table(1).UseAllAvailableWidth();
                totalTable.AddCell(new Cell().Add(new Paragraph($"Cộng tiền hàng (Total amount): {totalAmount:0,0} VND").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Tiền thuế GTGT (VAT amount): {(totalAmount * 0.1m):0,0} VND").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Tổng cộng tiền thanh toán (Total payment): {totalPayment:0,0} VND").SetFont(fontBold).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                document.Add(totalTable);

                document.Add(new Paragraph($"Số tiền viết bằng chữ (Amount in words): {amountInWords}").SetFont(fontNormal).SetFontSize(9));

                // Ký tên
                Table signatureTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                signatureTable.AddCell(new Cell().Add(new Paragraph("NGƯỜI MUA HÀNG (Buyer)\n(Ký, ghi rõ họ, tên)").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                signatureTable.AddCell(new Cell().Add(new Paragraph("NGƯỜI BÁN HÀNG (Seller)\n(Ký, đóng dấu, ghi rõ họ, tên)").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                document.Add(signatureTable);

                document.Close();

                byte[] pdfBytes = stream.ToArray();
                return File(pdfBytes, "application/pdf", $"hoa_don_{orderId}.pdf");
            }
        }



        private static string ConvertNumberToWords(decimal number)
        {
            string[] units = { "", "nghìn", "triệu", "tỷ" };
            string[] numbers = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            if (number == 0) return "không đồng";

            string words = "";
            int unitIndex = 0;

            while (number > 0)
            {
                int part = (int)(number % 1000);
                if (part > 0)
                {
                    string partWords = "";
                    int hundreds = part / 100;
                    int tens = (part % 100) / 10;
                    int unitsPart = part % 10;

                    if (hundreds > 0)
                    {
                        partWords += numbers[hundreds] + " trăm ";
                        if (tens == 0 && unitsPart > 0) partWords += "lẻ ";
                    }

                    if (tens > 1)
                    {
                        partWords += numbers[tens] + " mươi ";
                        if (unitsPart > 0) partWords += numbers[unitsPart] + " ";
                    }
                    else if (tens == 1)
                    {
                        partWords += "mười ";
                        if (unitsPart > 0) partWords += numbers[unitsPart] + " ";
                    }
                    else if (unitsPart > 0)
                    {
                        partWords += numbers[unitsPart] + " ";
                    }

                    partWords += units[unitIndex] + " ";
                    words = partWords + words;
                }

                number /= 1000;
                unitIndex++;
            }

            return words.Trim() + " đồng";
        }
    }
}
